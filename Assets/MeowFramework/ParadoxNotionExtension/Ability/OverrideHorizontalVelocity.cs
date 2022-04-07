// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 07/04/2022 5:45
// 最后一次修改于: 07/04/2022 11:02
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections;
using MeowFramework;
using MeowFramework.MeowACT;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace FlowCanvas.Nodes
{
    [Category("MeowFramework/Ability")]
    [Description("在一段时间内覆盖水平速度")]
    public class OverrideHorizontalVelocity : LatentActionNode<float, float>
    {
        /// <summary>
        /// 输入控制器
        /// </summary>
        [Description("输入控制器")]
        public BBParameter<MeowACTInputController> InputController;

        /// <summary>
        /// 移动控制器
        /// </summary>
        [Description("移动控制器")]
        public BBParameter<ThirdPersonLocomotionController> LocomotionController;

        /// <summary>
        /// 时间比尺
        /// </summary>
        [Description("时间比尺")]
        public BBParameter<float> TimeScale = 1f;
        
        public float timeLeft { get; private set; }
        public float normalized { get; private set; }
        
        /// <summary>
        /// 速度覆盖的协程函数
        /// </summary>
        /// <param name="velocityOverride">速度覆盖值</param>
        /// <param name="time">时长</param>
        /// <param name="timeScale">时间尺度</param>
        /// <returns></returns>
        public override IEnumerator Invoke(float velocityOverride = 0f, float time = 1f)
        {
            // 空值检查
            if (InputController.value == null)
                yield break;
            if (LocomotionController.value == null)
                yield break;
            
            InputController.value.EnableMoveInput(false);
            LocomotionController.value.SetHorizontalVelocityOverride(velocityOverride);
            
            timeLeft = time;
            while ( timeLeft > 0 ) {
                timeLeft -= Time.deltaTime * TimeScale.value;
                timeLeft = Mathf.Max(timeLeft, 0);
                normalized = timeLeft / time;
                yield return null;
            }
            
            LocomotionController.value.CancelHorizontalVelocityOverride();
            InputController.value.EnableMoveInput(true);
            
            // 最后要返回 null，不然协程函数末尾的语句的执行时间会有问题
            yield return null;
        }
    }
}