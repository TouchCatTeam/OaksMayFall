// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 25/03/2022 23:35
// 最后一次修改于: 26/03/2022 22:52
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
    public class ThirdPerson : MonoBehaviour
    {
        // Perfab 自带组件

        public CharacterController CharacterCtr;
        public Animator Anim;
        public MeowACTInputController Input;
        public GameObject CMCameraFollowTarget;
        public GameObject PlayerFollowCamera;
        public GameObject MainCamera;

        // 控制组件

        public ThirdPersonAttributeManager AttributeManager;
        public ThirdPersonEventManager EventManager;
        private ThirdPersonActionController actionController;
        private ThirdPersonLocomotionController locomotionController;
        private ThirdPersonAnimationController animationController;
        private ThirdPersonBattleController battleController;
        private ThirdPersonUIController uiController;

        private void Awake()
        {
            // 初始化 Perfab 自带组件

            CharacterCtr = GetComponent<CharacterController>();
            Anim = GetComponent<Animator>();
            Input = gameObject.AddComponent<MeowACTInputController>();
            CMCameraFollowTarget = transform.Find("PlayerCameraRoot").gameObject;
            PlayerFollowCamera = GameObject.Find("PlayerFollowCamera");
            PlayerFollowCamera.GetComponent<CinemachineVirtualCamera>().Follow = CMCameraFollowTarget.transform;
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");

            // 初始化控制组件

            AttributeManager = new ThirdPersonAttributeManager(this);
            EventManager = new ThirdPersonEventManager(this);
            actionController = new ThirdPersonActionController(this);
            locomotionController = new ThirdPersonLocomotionController(this);
            animationController = new ThirdPersonAnimationController(this);
            battleController = new ThirdPersonBattleController(this);
            uiController = new ThirdPersonUIController(this);
            
            // 初始化动画状态机参数
            
            animationController.AssignAnimationIDs();

            // 初始化属性

            AttributeManager.Init();
        }

        protected void Update()
        {
            // 行为

            actionController.TryAction();

            // 运动

            locomotionController.ApplyGravity();
            locomotionController.GroundedCheck();
            locomotionController.Move();
            locomotionController.RotateToMoveDir();

            // 动画

            animationController.SetAnimatorValue();
        }

        protected void LateUpdate()
        {
            // 运动

            locomotionController.CameraRotate();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // 运动

            if (AttributeManager.canPush) PhysicsUtility.PushRigidBodies(hit, AttributeManager.pushLayers, AttributeManager.strength);
        }

        /// <summary>
        /// 由动画触发的动画事件，还需要在 mgr 中触发事件：尝试造成伤害
        /// </summary>
        public void OnTryDoDamageAnimEvent()
        {
            EventManager.Fire("TryDoDamageEvent", null);
        }
        
        /// <summary>
        /// 由动画触发的动画事件，还需要在行为控制器中触发事件：结束近战攻击
        /// </summary>
        public void OnEndMeleeAttackAnimEvent()
        {
            actionController.FireEndMeleeAttackEvent();
        }
    }
}