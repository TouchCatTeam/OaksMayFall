// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 16/03/2022 16:53
// 最后一次修改于: 29/03/2022 9:58
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using UnityEngine;

namespace MeowACT
{
	/// <summary>
	/// 第三人称运动控制器
	/// </summary>
    public class ThirdPersonLocomotionController : MonoBehaviour
    {
	    // 组件

	    /// <summary>
	    /// 角色控制器
	    /// </summary>
	    public CharacterController CharacterCtr;

	    /// <summary>
	    /// 世界主摄像机
	    /// </summary>
	    public ScriptableGameObject MainCamera;

	    /// <summary>
	    /// 摄像机跟随点
	    /// </summary>
	    public ScriptableGameObject CMCameraFollowTarget;
	   
	    /// <summary>
	    /// 玩家输入
	    /// </summary>
	    public MeowACTInputController Input;
	    
	    // 常量
	    
	    /// <summary>
	    /// 微量
	    /// </summary>
	    private const float Threshold = 0.01f;
	    
	    
	    // 运动相关

	    /// <summary>
	    /// 水平速度
	    /// </summary>
	    public ScriptableVector3 HorizontalVelocity;
	    
	    /// <summary>
	    /// 竖直速度
	    /// </summary>
	    public ScriptableVector3 VerticalVelocity;

	    /// <summary>
	    /// 玩家是否被冻结
	    /// </summary>
	    public ScriptableBoolean IsFreezingMove;
	    
	    // 行走相关
	    
	    /// <summary>
	    /// 移动速度
	    /// </summary>
	    public float WalkSpeed = 7f;
	    
	    /// <summary>
	    /// 玩家旋转的过渡时间
	    /// </summary>
	    public float RotationSmoothTime = 0.2f;
	    
	    /// <summary>
	    /// 旋转角的过渡速度
	    /// </summary>
	    private float rotationSmoothVelocity;
	    
	    /// <summary>
	    /// 行走速度的过渡速度
	    /// </summary>
	    private Vector3 walkSmoothVelocity;
	    
	    /// <summary>
	    /// 玩家行走的过渡时间
	    /// </summary>
	    public float WalkSmoothTime = 0.2f;

	    // 冲刺相关
	    
	    /// <summary>
	    /// 冲刺速度
	    /// </summary>
	    public float SprintSpeed = 15f;

	    /// <summary>
	    /// 是否正在冲刺
	    /// </summary>
	    public ScriptableBoolean IsSprinting;

	    /// <summary>
	    /// 是否刚开始冲刺
	    /// </summary>
	    public ScriptableBoolean IsSprintBegin;
	    
	    /// <summary>
	    /// 冲刺速度的过渡速度
	    /// </summary>
	    private Vector3 sprintSmoothVelocity;
	    
	    /// <summary>
	    /// 玩家冲刺速度的过渡时间
	    /// </summary>
	    public float SprintSmoothTime = 0.5f;
	    
	    // 物理相关
	    
	    /// <summary>
	    /// 重力系数
	    /// </summary>
	    public float Gravity = -9.8f;
	    
	    /// <summary>
	    /// 最大下落速度
	    /// </summary>
	    public float TerminalVelocity = 53f;
	    
	    // 落地相关

	    /// <summary>
	    /// 是否落地
	    /// </summary>
	    public ScriptableBoolean IsGrounded;
	    
	    /// <summary>
	    /// 落地球形碰撞检测中心点的竖向偏移量
	    /// </summary>
	    private float groundedOffset = -0.14f;
	    
	    /// <summary>
	    /// 落地球形碰撞检测的半径
	    /// </summary>
	    private float groundedRadius = 0.28f;
	    
	    /// <summary>
	    /// 落地球形碰撞检测的层级
	    /// </summary>
	    public int GroundLayers = 1;

	    // 摄像机相关
		
	    /// <summary>
	    /// 摄像机角度增量
	    /// </summary>
		public float CameraAngleOverride = 0f;
	    
	    /// <summary>
	    /// 摄像机转动的速度
	    /// </summary>
	    public float CameraRotSpeed = 25f;
	    
	    /// <summary>
	    /// 摄像机最大俯仰角
	    /// </summary>
	    public float TopClamp = 70f; 
	    
	    /// <summary>
	    /// 摄像机最小俯仰角
	    /// </summary>
	    public float BottomClamp = -30f;
	    
	    /// <summary>
	    /// 摄像机跟随点的当前俯仰角的过渡时间
	    /// </summary>
	    public float CinemachinePitchSmoothTime = 0.1f;
	    
	    /// <summary>
	    /// 摄像机跟随点的当前偏航角的过渡时间
	    /// </summary>
	    public float CinemachineYawSmoothTime = 0.1f;
	    
	    /// <summary>
	    /// 摄像机是否固定
	    /// </summary>
	    public bool IsCameraFixed = false;
	    
	    /// <summary>
	    /// 摄像机跟随点的期望俯仰角
	    /// </summary>
	    private float cinemachineTargetPitch;
	    
	    /// <summary>
	    /// 摄像机跟随点的期望偏航角
	    /// </summary>
	    private float cinemachineTargetYaw;
	    
	    /// <summary>
	    /// 摄像机跟随点的当前俯仰角和摄像机跟随点的当前偏航角组成的向量
	    /// </summary>
	    private Vector2 cinemachineCurrPy;
	    
	    /// <summary>
	    /// 摄像机跟随点的当前俯仰角的过渡速度
	    /// </summary>
	    private float cinemachinePitchSmoothVelocity;
	    
	    /// <summary>
	    /// 摄像机跟随点的当前俯仰角的过渡速度
	    /// </summary>
	    private float cinemachineYawSmoothVelocity;

	    /// <summary>
	    /// 落地检查
	    /// </summary>
	    public void GroundedCheck()
        {
	        var spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            IsGrounded.Value = Physics.CheckSphere(spherePosition, groundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        }
	    
	    /// <summary>
	    /// 应用重力
	    /// </summary>
        public void ApplyGravity()
        {
	        if (IsGrounded.Value && VerticalVelocity.Value.y < 0.0f)
		        VerticalVelocity.Value.y = -2f;
	        else if (VerticalVelocity.Value.y < TerminalVelocity)
		        VerticalVelocity.Value.y += Gravity * Time.deltaTime;
        }

        /// <summary>
        /// 移动
        /// </summary>
        public void Move()
        {
	        if (IsFreezingMove.Value)
	        {
		        HorizontalVelocity.Value = Vector3.zero;
		        return;
	        }

	        HorizontalVelocity.Value = IsSprinting.Value ? GetSprintSpeed() : GetNormalSpeed();

	        CharacterCtr.Move((HorizontalVelocity.Value + VerticalVelocity.Value) * Time.deltaTime);
        }

        private Vector3 GetSprintSpeed()
        {
	        // 输入移动方向
	        Vector3 inputDirection = new Vector3(Input.Move.x, 0.0f, Input.Move.y).normalized;
	        // 期望旋转
	        // 因为摄像机呼吸，MainCamera.transform.eulerAngles.y 会发生抖动，进而导致玩家在起步的时候有一个微小抖动
	        // 而 cinemachineTargetYaw 不会抖动，因此采用 cinemachineTargetYaw
	        float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cinemachineTargetYaw;
	        // 期望移动方向
	        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
	        
	        // 如果按下冲刺，那么初始化冲刺
	        if (IsSprintBegin.Value)
	        {
		        // 当前附加速度初始化为冲刺速度
		        // 不需要 SmoothDamp，这是突变的
		        // 如果没有运动输入的话，那么冲刺方向为角色当前朝向
		        if (Input.Move == Vector2.zero)
			        return transform.forward * SprintSpeed;
		        // 有运动的输入的话，那么冲刺方向为运动输入的方向
		        else
			        return targetDirection.normalized * SprintSpeed;
	        }
	        // 否则冲刺速度趋向 0
	        else
				return Vector3.SmoothDamp(HorizontalVelocity.Value, Vector3.zero, ref sprintSmoothVelocity, SprintSmoothTime);
        }

        private Vector3 GetNormalSpeed()
        {
	        // 输入移动方向
	        Vector3 inputDirection = new Vector3(Input.Move.x, 0.0f, Input.Move.y).normalized;
	        
	        // 期望旋转
	        // 因为摄像机呼吸，MainCamera.transform.eulerAngles.y 会发生抖动，进而导致玩家在起步的时候有一个微小抖动
	        // 而 cinemachineTargetYaw 不会抖动，因此采用 cinemachineTargetYaw
	        float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cinemachineTargetYaw;
	        // 期望移动方向
	        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

	        Vector3 targetVelocity = (Input.Move == Vector2.zero) ? Vector3.zero : targetDirection * WalkSpeed;
	    
	        return Vector3.SmoothDamp(HorizontalVelocity.Value, targetVelocity, ref walkSmoothVelocity, WalkSmoothTime);
        }

        /// <summary>
        /// 向移动方向旋转
        /// </summary>
        public void RotateToMoveDir()
        {
	        // 移动方向
	        Vector3 inputDirection = new Vector3(Input.Move.x, 0.0f, Input.Move.y).normalized;
	        
	        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
	        // if there is a move input rotate player when the player is moving
	        if (Input.Move != Vector2.zero)
	        {
		        float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + MainCamera.Value.transform.eulerAngles.y;
		        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationSmoothVelocity, RotationSmoothTime);

		        // 玩家旋转
		        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
	        }
        }

        /// <summary>
        /// 摄像机旋转
        /// </summary>
        public void CameraRotate()
        {
	        // if there is an input and camera position is not fixed
	        if (Input.Look.sqrMagnitude >= Threshold && !IsCameraFixed)
	        {
		        cinemachineTargetPitch += Input.Look.y * Time.deltaTime * CameraRotSpeed / 100.0f;
		        cinemachineTargetYaw += Input.Look.x * Time.deltaTime * CameraRotSpeed / 100.0f;
	        }

	        // clamp our rotations so our values are limited 360 degrees
	        cinemachineTargetPitch = MathUtility.ClampAngle(cinemachineTargetPitch, BottomClamp, TopClamp);
	        cinemachineTargetYaw = MathUtility.ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);

	        // 平滑
	        cinemachineCurrPy.x = Mathf.SmoothDampAngle(cinemachineCurrPy.x, cinemachineTargetPitch,
		        ref cinemachinePitchSmoothVelocity, CinemachinePitchSmoothTime);
	        cinemachineCurrPy.y = Mathf.SmoothDampAngle(cinemachineCurrPy.y, cinemachineTargetYaw,
		        ref cinemachineYawSmoothVelocity, CinemachineYawSmoothTime);
	  
	        // Cinemachine will follow this target
	        CMCameraFollowTarget.Value.transform.rotation = Quaternion.Euler(cinemachineCurrPy.x + CameraAngleOverride, cinemachineCurrPy.y, 0.0f);
        }

        public void Update()
        {
	        ApplyGravity();
	        GroundedCheck();
	        Move();
	        RotateToMoveDir();
        }

        public void LateUpdate()
        {
	        CameraRotate();
        }
    }
}
