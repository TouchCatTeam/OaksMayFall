// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 25/03/2022 23:35
// 最后一次修改于: 26/03/2022 8:32
// 版权所有: ThinkDifferentStudio
// 描述:
// ----------------------------------------------

using Cinemachine;
using UnityEngine;

namespace OaksMayFall
{
    /// <summary>
    /// 第三人称主角
    /// </summary>
    public class ThirdPerson : MonoBehaviour
    {
        // Perfab 自带组件

        public CharacterController CharacterCtr;
        public Animator Anim;
        public OaksMayFallInputController Input;
        public GameObject CMCameraFollowTarget;
        public GameObject PlayerFollowCamera;
        public GameObject MainCamera;

        // 控制组件

        private ThirdPersonActionController actionController;
        private ThirdPersonLocomotionController locomotionController;
        private ThirdPersonAnimationController animationController;
        private ThirdPersonBattleController battleController;

        // 事件相关
        
        /// <summary>
        /// 事件委托类型
        /// </summary>
        public delegate void ThirdPersonEvent(object[] args);
        
        /// <summary>
        /// 开始冲刺
        /// </summary>
        public event ThirdPersonEvent BeginSprintEvent;
        /// <summary>
        /// 开始冲刺之后一帧
        /// </summary>
        public event ThirdPersonEvent AfterBeginSprintEvent;
        /// <summary>
        /// 结束冲刺
        /// </summary>
        public event ThirdPersonEvent EndSprintEvent;
        /// <summary>
        /// 开始近战攻击
        /// </summary>
        public event ThirdPersonEvent BeginMeleeAttackEvent;
        /// <summary>
        /// 结束近战攻击
        /// </summary>
        public event ThirdPersonEvent EndMeleeAttackEvent;
        
        // 刚体模拟

        public LayerMask pushLayers;
        public bool canPush;
        [Range(0.5f, 5f)] public float strength = 1.1f;

        // 运动参数

        /// <summary>
        /// 水平速度
        /// </summary>
        /// <returns></returns>
        public Vector3 HorizontalVelocity;

        /// <summary>
        /// 竖向速度
        /// </summary>
        public Vector3 VerticalVelocity;

        // 运动状态

        // 运动状态 - 冲刺相关

        /// <summary>
        /// 是否落地
        /// </summary>
        public bool IsGrounded = true;

        /// <summary>
        /// 是否正在静止运动
        /// </summary>
        public bool IsFreezingMove = false;

        /// <summary>
        /// 是否正在冲刺
        /// </summary>
        public bool IsSprinting = false;

        /// <summary>
        /// 是否开始冲刺
        /// </summary>
        public bool IsSprintBegin = false;

        // 运动状态 - 攻击相关

        /// <summary>
        /// 是否正在攻击
        /// </summary>
        public bool IsMeleeAttacking = false;

        private void Awake()
        {
            // 初始化 Perfab 自带组件

            CharacterCtr = GetComponent<CharacterController>();
            Anim = GetComponent<Animator>();
            Input = gameObject.AddComponent<OaksMayFallInputController>();
            CMCameraFollowTarget = transform.Find("PlayerCameraRoot").gameObject;
            PlayerFollowCamera = GameObject.Find("PlayerFollowCamera");
            PlayerFollowCamera.GetComponent<CinemachineVirtualCamera>().Follow = CMCameraFollowTarget.transform;
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");

            // 初始化控制组件

            actionController = new ThirdPersonActionController(this);
            locomotionController = new ThirdPersonLocomotionController(this);
            animationController = new ThirdPersonAnimationController(this);
            battleController = new ThirdPersonBattleController(this);

            // 初始化动画状态机参数
            animationController.AssignAnimationIDs();
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

            if (canPush) PhysicsUtility.PushRigidBodies(hit, pushLayers, strength);
        }
        
        /// <summary>
        /// 触发开始冲刺事件
        /// </summary>
        /// <param name="args"></param>
        public void FireBeginSprintEvent(object[] args)
        {
            BeginSprintEvent?.Invoke(args);
        }

        /// <summary>
        /// 触发开始冲刺之后一帧事件
        /// </summary>
        /// <param name="args"></param>
        public void FireAfterBeginSprintEvent(object[] args)
        {
            AfterBeginSprintEvent?.Invoke(args);
        }
        
        /// <summary>
        /// 触发结束冲刺事件
        /// </summary>
        /// <param name="args"></param>
        public void FireEndSprintEvent(object[] args)
        {
            EndSprintEvent?.Invoke(args);
        }
        
        /// <summary>
        /// 触发开始近战攻击事件
        /// </summary>
        /// <param name="args"></param>
        public void FireBeginMeleeAttackEvent(object[] args)
        {
            BeginMeleeAttackEvent?.Invoke(args);
        }
        
        /// <summary>
        /// 触发结束近战攻击事件
        /// </summary>
        /// <param name="args"></param>
        public void FireEndMeleeAttackEvent(object[] args)
        {
            EndMeleeAttackEvent?.Invoke(args);
        }
        
        /// <summary>
        /// 动画事件：尝试攻击
        /// </summary>
        public void TryDoDamage()
        {
            
        }
    }
}