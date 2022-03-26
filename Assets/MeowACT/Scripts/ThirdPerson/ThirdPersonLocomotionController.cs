// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 16/03/2022 16:53
// 最后一次修改于: 26/03/2022 22:54
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using UnityEngine;

namespace MeowACT
{
	/// <summary>
	/// 第三人称运动控制器
	/// </summary>
    public class ThirdPersonLocomotionController
    {
	    // 常量
	    
	    /// <summary>
	    /// 微量
	    /// </summary>
	    private const float Threshold = 0.01f;
	    
	    
	    // 运动相关

	    // 行走相关
	    
	    /// <summary>
	    /// 移动速度
	    /// </summary>
	    private float walkSpeed = 7f;
	    /// <summary>
	    /// 玩家旋转的过渡时间
	    /// </summary>
	    private float rotationSmoothTime = 0.2f;
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
	    private float walkSmoothTime = 0.2f;

	    // 冲刺相关
	    
	    /// <summary>
	    /// 冲刺速度
	    /// </summary>
	    private float sprintSpeed = 15f;
	    /// <summary>
	    /// 冲刺速度的过渡速度
	    /// </summary>
	    private Vector3 sprintSmoothVelocity;
	    /// <summary>
	    /// 玩家冲刺速度的过渡时间
	    /// </summary>
	    private float sprintSmoothTime = 0.5f;
	    
	    // 物理相关
	    
	    /// <summary>
	    /// 重力系数
	    /// </summary>
	    private float gravity = -9.8f;
	    /// <summary>
	    /// 最大下落速度
	    /// </summary>
	    private float terminalVelocity = 53f;
	    
	    // 落地相关
	    
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
	    private int groundLayers = 1;

	    // 摄像机相关
		
		private float cameraAngleOverride = 0f;
	    /// <summary>
	    /// 摄像机转动的速度
	    /// </summary>
	    private float cameraRotSpeed = 25f;
	    /// <summary>
	    /// 摄像机最大俯仰角
	    /// </summary>
	    private float topClamp = 70f; 
	    /// <summary>
	    /// 摄像机最小俯仰角
	    /// </summary>
	    private float bottomClamp = -30f;
	    /// <summary>
	    /// 摄像机跟随点的当前俯仰角的过渡时间
	    /// </summary>
	    private float cinemachinePitchSmoothTime = 0.1f;
	    /// <summary>
	    /// 摄像机跟随点的当前偏航角的过渡时间
	    /// </summary>
	    private float cinemachineYawSmoothTime = 0.1f;
	    /// <summary>
	    /// 摄像机是否固定
	    /// </summary>
	    private bool isCameraFixed = false;
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
	    private Vector2 cinemachineCurrPY;
	    /// <summary>
	    /// 摄像机跟随点的当前俯仰角的过渡速度
	    /// </summary>
	    private float cinemachinePitchSmoothVelocity;
	    /// <summary>
	    /// 摄像机跟随点的当前俯仰角的过渡速度
	    /// </summary>
	    private float cinemachineYawSmoothVelocity;

	    /// <summary>
		/// 第三人称运动控制器的主人
		/// </summary>
		public ThirdPerson Owner;
		
		/// <summary>
		/// 第三人称运动控制器的构造函数
		/// </summary>
		/// <param name="owner">第三人称运动控制器的主人</param>
		public ThirdPersonLocomotionController(ThirdPerson owner)
	    {
		    Owner = owner;
	    }

		/// <summary>
	    /// 落地检查
	    /// </summary>
	    public void GroundedCheck()
        {
	        var spherePosition = new Vector3(Owner.transform.position.x, Owner.transform.position.y - groundedOffset, Owner.transform.position.z);
            Owner.AttributeManager.IsGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
        }
	    
	    /// <summary>
	    /// 应用重力
	    /// </summary>
        public void ApplyGravity()
        {
	        if (Owner.AttributeManager.IsGrounded && Owner.AttributeManager.VerticalVelocity.y < 0.0f)
		        Owner.AttributeManager.VerticalVelocity.y = -2f;
	        else if (Owner.AttributeManager.VerticalVelocity.y < terminalVelocity)
		        Owner.AttributeManager.VerticalVelocity.y += gravity * Time.deltaTime;
        }

        /// <summary>
        /// 移动
        /// </summary>
        public void Move()
        {
	        if (Owner.AttributeManager.IsFreezingMove)
	        {
		        Owner.AttributeManager.HorizontalVelocity = Vector3.zero;
		        return;
	        }

	        Owner.AttributeManager.HorizontalVelocity = Owner.AttributeManager.IsSprinting ? GetSprintSpeed() : GetNormalSpeed();
	        Owner.CharacterCtr.Move((Owner.AttributeManager.HorizontalVelocity + Owner.AttributeManager.VerticalVelocity) * Time.deltaTime);
        }

        private Vector3 GetSprintSpeed()
        {
	        // 输入移动方向
	        Vector3 inputDirection = new Vector3(Owner.Input.Move.x, 0.0f, Owner.Input.Move.y).normalized;
	        // 期望旋转
	        // 因为摄像机呼吸，Owner.MainCamera.transform.eulerAngles.y 会发生抖动，进而导致玩家在起步的时候有一个微小抖动
	        // 而 cinemachineTargetYaw 不会抖动，因此采用 cinemachineTargetYaw
	        float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cinemachineTargetYaw;
	        // 期望移动方向
	        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
	        
	        // 如果按下冲刺，那么初始化冲刺
	        if (Owner.AttributeManager.IsSprintBegin)
	        {
		        // 当前附加速度初始化为冲刺速度
		        // 不需要 SmoothDamp，这是突变的
		        // 如果没有运动输入的话，那么冲刺方向为角色当前朝向
		        if (Owner.Input.Move == Vector2.zero)
			        return Owner.transform.forward * sprintSpeed;
		        // 有运动的输入的话，那么冲刺方向为运动输入的方向
		        else
			        return targetDirection.normalized * sprintSpeed;
	        }
	        // 否则冲刺速度趋向 0
	        else
				return Vector3.SmoothDamp(Owner.AttributeManager.HorizontalVelocity, Vector3.zero, ref sprintSmoothVelocity, sprintSmoothTime);
        }

        private Vector3 GetNormalSpeed()
        {
	        // 输入移动方向
	        Vector3 inputDirection = new Vector3(Owner.Input.Move.x, 0.0f, Owner.Input.Move.y).normalized;
	        // 期望旋转
	        // 因为摄像机呼吸，Owner.MainCamera.transform.eulerAngles.y 会发生抖动，进而导致玩家在起步的时候有一个微小抖动
	        // 而 cinemachineTargetYaw 不会抖动，因此采用 cinemachineTargetYaw
	        float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cinemachineTargetYaw;
	        // 期望移动方向
	        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

	        Vector3 targetVelocity = (Owner.Input.Move == Vector2.zero) ? Vector3.zero : targetDirection * walkSpeed;
	        
	        return Vector3.SmoothDamp(Owner.AttributeManager.HorizontalVelocity, targetVelocity, ref walkSmoothVelocity, walkSmoothTime);
        }

        /// <summary>
        /// 向移动方向旋转
        /// </summary>
        public void RotateToMoveDir()
        {
	        // 移动方向
	        Vector3 inputDirection = new Vector3(Owner.Input.Move.x, 0.0f, Owner.Input.Move.y).normalized;

	        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
	        // if there is a move input rotate player when the player is moving
	        if (Owner.Input.Move != Vector2.zero)
	        {
		        float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + Owner.MainCamera.transform.eulerAngles.y;
		        float rotation = Mathf.SmoothDampAngle(Owner.transform.eulerAngles.y, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);

		        // 玩家旋转
		        Owner.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
	        }
        }

        /// <summary>
        /// 摄像机旋转
        /// </summary>
        public void CameraRotate()
        {
	        // if there is an input and camera position is not fixed
	        if (Owner.Input.Look.sqrMagnitude >= Threshold && !isCameraFixed)
	        {
		        cinemachineTargetPitch += Owner.Input.Look.y * Time.deltaTime * cameraRotSpeed / 100.0f;
		        cinemachineTargetYaw += Owner.Input.Look.x * Time.deltaTime * cameraRotSpeed / 100.0f;
	        }

	        // clamp our rotations so our values are limited 360 degrees
	        cinemachineTargetPitch = MathUtility.ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);
	        cinemachineTargetYaw = MathUtility.ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);

	        // 平滑
	        cinemachineCurrPY.x = Mathf.SmoothDampAngle(cinemachineCurrPY.x, cinemachineTargetPitch,
		        ref cinemachinePitchSmoothVelocity, cinemachinePitchSmoothTime);
	        cinemachineCurrPY.y = Mathf.SmoothDampAngle(cinemachineCurrPY.y, cinemachineTargetYaw,
		        ref cinemachineYawSmoothVelocity, cinemachineYawSmoothTime);
	  
	        // Cinemachine will follow this target
	        Owner.CMCameraFollowTarget.transform.rotation = Quaternion.Euler(cinemachineCurrPY.x + cameraAngleOverride, cinemachineCurrPY.y, 0.0f);
        }
    }
}
