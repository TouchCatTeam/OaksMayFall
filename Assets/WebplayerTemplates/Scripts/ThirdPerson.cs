// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 25/03/2022 23:35
// 最后一次修改于: 29/03/2022 23:37
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using Cinemachine;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 第三人称主角
    /// </summary>
    public class ThirdPerson : ActorBase
    {
        /// <summary>
        /// 耐力
        /// </summary>
        [Tooltip("耐力")] 
        public ScriptableFloatReference NRG;
        
        /// <summary>
        /// 最大耐力
        /// </summary>
        [Tooltip("最大耐力")] 
        public ScriptableFloatReference MaxNRG;

        /// <summary>
        /// 战斗 UI
        /// </summary>
        [Header("组件")]
        [Tooltip("战斗 UI")] 
        public Transform BattleGUI;

        /// <summary>
        /// 可资产化摄像机
        /// </summary>
        [Tooltip("可资产化摄像机")] 
        public ScriptableGameObject MainCamera;
        
        /// <summary>
        /// 是否能够推动刚体
        /// </summary>
        [Header("刚体模拟")]
        [Tooltip("是否能够推动刚体")] 
        public bool CanPushRigidBody;

        /// <summary>
        /// 推动物体的层级
        /// </summary>
        [Tooltip("推动物体的层级")]
        public LayerMask PushRigidBodyLayerMask;

        /// <summary>
        /// 推动物体的力度
        /// </summary>
        [Tooltip("推动物体的力度")]
        public float PushRigidBodyStrength = 1f;

        private void Awake()
        {
            // 初始化 Perfab 自带组件
            if (BattleGUI != null)
            {
                BattleGUI.GetComponent<Canvas>().worldCamera = MainCamera.Value.GetComponent<Camera>();
                BattleGUI.GetComponent<Canvas>().planeDistance = 1;
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // 运动

            if (CanPushRigidBody) PhysicsUtility.PushRigidBodies(hit, PushRigidBodyLayerMask, PushRigidBodyStrength);
        }
    }
}