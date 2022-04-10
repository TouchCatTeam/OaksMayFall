// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 07/04/2022 10:23
// 最后一次修改于: 10/04/2022 10:25
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections;
using Cinemachine;
using MeowFramework.TPSCharacter;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace FlowCanvas.Nodes
{
    [Category("MeowFramework/Ability")]
    [Description("玩家摄像机平滑移动")]
    public class PlayerCameraSmoothMove : LatentActionNode<TPSCharacterLocomotionController>
    {
        /// <summary>
        /// 时间比尺
        /// </summary>
        [Description("时间比尺")]
        public BBParameter<float> TimeScale = 1f;

        /// <summary>
        /// 摄像机移动的过渡时间
        /// </summary>
        [Description("摄像机移动的过渡时间")]
        public BBParameter<float> CameraMoveTransitionTime = 1f;

        /// <summary>
        /// 摄像机的目标 FOV
        /// </summary>
        [Description("摄像机的目标 FOV")]
        public BBParameter<float> TargetFOV = 30f;
        
        /// <summary>
        /// 摄像机的目标 FOV 平滑速度
        /// </summary>
        private float FOVSmoothVelocity;
        
        /// <summary>
        /// 摄像机的目标 FOV 的平滑时间
        /// </summary>
        [Description("摄像机的目标 FOV 的平滑时间")]
        public BBParameter<float> FOVSmoothTime = 0.2f;
        
        /// <summary>
        /// 摄像机的目标侧向位置
        /// </summary>
        [Description("摄像机的目标侧向位置")]
        public BBParameter<float> TargetSide = 0.5f;

        /// <summary>
        /// 摄像机侧向位置的平滑速度
        /// </summary>
        private float cameraSideSmoothVelocity;
        
        /// <summary>
        /// 摄像机侧向位置的平滑时间
        /// </summary>
        [Description("摄像机侧向位置的平滑时间")]
        public BBParameter<float> CameraSideSmoothTime = 0.2f;
        
        /// <summary>
        /// 剩余时间
        /// </summary>
        private float timeLeft { get; set; }

        public override IEnumerator Invoke(TPSCharacterLocomotionController locomotionController)
        {
            CinemachineVirtualCamera playerCamera = locomotionController.PlayerFollowCamera;
            
            // 摄像机第三人称跟随组件
            var camera3rdPersonFollow =
                playerCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            
            // 初始化计时器
            timeLeft = CameraMoveTransitionTime.value;
            
            // 在给定时间内平滑
            // 平滑时间结束时，被平滑项接近终点值但不是终点值
            // 因此最后需要给被平滑项赋终点值，这可能产生一个抖动
            // 因此平滑时间需要在保证效果的同时尽可能小，才能让最后的抖动变小
            while ( timeLeft > 0 ) {
                timeLeft -= Time.deltaTime * TimeScale.value;
                playerCamera.m_Lens.FieldOfView = Mathf.SmoothDamp(playerCamera.m_Lens.FieldOfView, TargetFOV.value,
                    ref FOVSmoothVelocity, FOVSmoothTime.value);
                camera3rdPersonFollow.CameraSide = Mathf.SmoothDamp(camera3rdPersonFollow.CameraSide, TargetSide.value,
                    ref cameraSideSmoothVelocity, CameraSideSmoothTime.value);
                yield return null;
            }

            // 摄像机焦距设置赋终点值
            playerCamera.m_Lens.FieldOfView = TargetFOV.value;
            // 摄像机侧向位置赋终点值
            camera3rdPersonFollow.CameraSide = TargetSide.value;
            
            yield return null;
        }
    }
}