// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/04/2022 15:46
// 最后一次修改于: 18/04/2022 15:34
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using MeowFramework.Core.Scriptable;
using MeowFramework.Core.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.TPSCharacter
{
	/// <summary>
	/// 第三人称运动控制器
	/// </summary>
    public partial class TPSCharacterLocomotionController
	{

	    // 运动相关
	    
	    /// <summary>
	    /// 可资产化水平速度
	    /// </summary>
	    [BoxGroup("Velocity")]
	    [Required]
	    [Tooltip("可资产化水平速度")]
	    public ScriptableVector3Variable HorizontalVelocity;

	    /// <summary>
	    /// 可资产化竖直速度
	    /// </summary>
	    [BoxGroup("Velocity")]
	    [Required]
	    [Tooltip("可资产化竖直速度")]
	    public ScriptableFloatVariable VerticalVelocity;

	    /// <summary>
	    /// 水平速度是否被覆盖了
	    /// </summary>
	    [BoxGroup("Velocity")]
	    [HorizontalGroup("Velocity/HorizontalVelocityOverride")]
	    [ShowInInspector]
	    [Sirenix.OdinInspector.ReadOnly]
	    [Tooltip("水平速度覆盖值")]
	    private bool isHorizontalVelocityOverrided;
	    
	    /// <summary>
	    /// 水平速度覆盖值
	    /// </summary>
	    [BoxGroup("Velocity")]
	    [HorizontalGroup("Velocity/HorizontalVelocityOverride")]
	    [ShowInInspector]
	    [Sirenix.OdinInspector.ReadOnly]
	    [Tooltip("水平速度覆盖值")]
	    private float horizontalVelocityOverride;

	    // 行走相关

	    /// <summary>
	    /// 移动速度
	    /// </summary>
	    [BoxGroup("Walk")]
	    [ShowInInspector]
	    [Tooltip("移动速度")]
	    private float walkSpeed = 4f;

	    /// <summary>
	    /// 玩家行走的过渡时间
	    /// </summary>
	    [BoxGroup("Walk")]
	    [ShowInInspector]
	    [Tooltip("玩家行走的过渡时间")]
	    private float walkSmoothTime = 0.2f;

	    /// <summary>
	    /// 玩家旋转的过渡时间
	    /// 如果这个值过大，由于使用了 SmoothDamp，会让镜头移动出现明显的粘滞感
	    /// </summary>
	    [BoxGroup("Walk")]
	    [ShowInInspector]
	    [Tooltip("玩家旋转的过渡时间")]
	    private float rotationSmoothTime = 0.1f;

	    // 物理相关
	    
	    /// <summary>
	    /// 重力系数
	    /// </summary>
	    [BoxGroup("Gravity")]
	    [ShowInInspector]
	    [Tooltip("重力系数")]
	    private float gravity = -9.8f;
	    
	    /// <summary>
	    /// 最大下落速度
	    /// </summary>
	    [BoxGroup("Gravity")]
	    [ShowInInspector]
	    [Tooltip("最大下落速度")]
	    private float terminalVelocity = 53f;
	    
	    // 落地相关

	    /// <summary>
	    /// 是否落地
	    /// </summary>
	    [BoxGroup("GroundCheck")]
	    [ShowInInspector]
	    [Sirenix.OdinInspector.ReadOnly]
	    [Tooltip("是否落地")]
	    private bool isGrounded;

	    /// <summary>
	    /// 是否落地
	    /// </summary>
	    public bool IsGrounded => isGrounded;
	    
	    /// <summary>
	    /// 落地球形碰撞检测中心点的竖向偏移量
	    /// </summary>
	    [BoxGroup("GroundCheck")]
	    [ShowInInspector]
	    [Tooltip("落地球形碰撞检测中心点的竖向偏移量")]
	    private float groundedOffset = -0.14f;
	    
	    /// <summary>
	    /// 落地球形碰撞检测的半径
	    /// </summary>
	    [BoxGroup("GroundCheck")]
	    [ShowInInspector]
	    [Tooltip("落地球形碰撞检测的半径")]
	    private float groundedRadius = 0.28f;
	    
	    /// <summary>
	    /// 落地球形碰撞检测的层级
	    /// </summary>
	    [BoxGroup("GroundCheck")]
	    [ShowInInspector]
	    [Tooltip("落地球形碰撞检测的层级")]
	    private int groundLayers = 1;

	    // 摄像机相关
		
	    /// <summary>
	    /// 摄像机是否固定
	    /// </summary>
	    [BoxGroup("Camera")]
	    [ShowInInspector]
	    [Sirenix.OdinInspector.ReadOnly]
	    [Tooltip("摄像机是否固定")]
	    private bool IsCameraFixed = false;
	    
	    /// <summary>
	    /// 摄像机俯仰角覆盖值
	    /// </summary>
		[BoxGroup("Camera")]
		[ShowInInspector]
	    [Tooltip("摄像机俯仰角覆盖值")]
	    private float cameraPitchOverride = 0f;
		
	    /// <summary>
	    /// 摄像机转动速度
	    /// </summary>
	    [BoxGroup("Camera")]
	    [ShowInInspector]
	    [Tooltip("摄像机转动速度")]
	    private float cameraRotSpeed = 25f;
	    
	    /// <summary>
	    /// 摄像机最大俯仰角
	    /// </summary>
	    [BoxGroup("Camera")]
	    [PropertyRange(0,90)]
	    [ShowInInspector]
	    [Tooltip("摄像机最大俯仰角")]
	    private float topClamp = 70f; 
	    
	    /// <summary>
	    /// 摄像机最小俯仰角
	    /// </summary>
	    [BoxGroup("Camera")]
	    [PropertyRange(-90,0)]
	    [ShowInInspector]
	    [Tooltip("摄像机最小俯仰角")]
	    private float bottomClamp = -30f;
	    
	    /// <summary>
	    /// 摄像机跟随点的当前俯仰角的过渡时间
	    /// </summary>
	    [BoxGroup("Camera")]
	    [ShowInInspector]
	    [Tooltip("摄像机跟随点的当前俯仰角的过渡时间")]
	    private float cinemachinePitchSmoothTime = 0.1f;
	    
	    /// <summary>
	    /// 摄像机跟随点的当前偏航角的过渡时间
	    /// </summary>
	    [BoxGroup("Camera")]
	    [ShowInInspector]
	    [Tooltip("摄像机跟随点的当前偏航角的过渡时间")]
	    private float cinemachineYawSmoothTime = 0.1f;

	    // 缓存
	    
	    // 缓存 - 速度覆盖
	    
	    /// <summary>
	    /// 水平速度方向覆盖值
	    /// </summary>
	    private Vector3 horizontalVelocityDirectionOverride;
	    
	    // 缓存 - 玩家行走
	    
	    /// <summary>
	    /// 行走速度的过渡速度
	    /// </summary>
	    private Vector3 walkSmoothVelocity;
	    
	    /// <summary>
	    /// 旋转角的过渡速度
	    /// </summary>
	    private float rotationSmoothVelocity;
	    
	    // 缓存 - 摄像机旋转
	    
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
	    /// 落地检查
	    /// </summary>
	    private void GroundedCheck()
        {
	        var spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
        }
	    
	    /// <summary>
	    /// 应用重力
	    /// </summary>
        private void ApplyGravity()
        {
	        if (isGrounded && VerticalVelocity.Value < 0.0f)
		        VerticalVelocity.Value = -2f;
	        else if (VerticalVelocity.Value < terminalVelocity)
		        VerticalVelocity.Value += gravity * Time.deltaTime;
        }

        /// <summary>
        /// 移动
        /// </summary>
        private void Move()
        {
	        HorizontalVelocity.Value = GetSpeed();

	        CharacterCtr.Move((HorizontalVelocity.Value + VerticalVelocity.Value * new Vector3(0,1,0)) * Time.deltaTime);
        }

        private Vector3 GetSpeed()
        {
	        // 基于摄像机方向，向键盘输入方向移动的方向
	        Vector3	targetMoveDirection = GetInputDirectionBaseOnCamera();

	        // 期望旋转方向
	        Vector3 targetRotateDirection;
	        // 摄像机的前方向
	        if (mode == TPSCharacterBehaviourMode.Rifle)
		        targetRotateDirection = Camera.main.transform.forward;
	        else
		        targetRotateDirection = targetMoveDirection;

	        // 向期望移动方向旋转
	        RotateTo(targetRotateDirection);

			// 如果有速度覆盖，则直接返回速度覆盖的结果
	        float targetSpeed = isHorizontalVelocityOverrided ? horizontalVelocityOverride : walkSpeed;
	        
	        // 如果没有速度覆盖，则返回 Smooth 的结果
	        Vector3 targetVelocity = (ACTInput.Move == Vector2.zero && isHorizontalVelocityOverrided == false) ? Vector3.zero : targetMoveDirection * targetSpeed;
	        
	        return Vector3.SmoothDamp(HorizontalVelocity.Value, targetVelocity, ref walkSmoothVelocity, walkSmoothTime);
        }

        /// <summary>
        /// 向移动方向旋转
        /// </summary>
        private void RotateTo(Vector3 inputDirection)
        {
	        // 有键盘输入
	        // 或者没有键盘输入但是有速度覆盖时
	        // 才会启用旋转
	        if (ACTInput.Move != Vector2.zero || isHorizontalVelocityOverrided == true)
	        {
		        float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
		        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);

		        // 玩家旋转
		        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
	        }
        }

        /// <summary>
        /// 摄像机旋转
        /// </summary>
        private void CameraRotate()
        {
	        // if there is an input and camera position is not fixed
	        if (ACTInput.Look.sqrMagnitude >= Threshold && !IsCameraFixed)
	        {
		        cinemachineTargetPitch += ACTInput.Look.y * Time.deltaTime * cameraRotSpeed / 100.0f;
		        cinemachineTargetYaw += ACTInput.Look.x * Time.deltaTime * cameraRotSpeed / 100.0f;
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
	        CMCameraFollowTarget.transform.rotation = Quaternion.Euler(cinemachineCurrPY.x + cameraPitchOverride, cinemachineCurrPY.y, 0.0f);
        }

        /// <summary>
        /// 基于摄像机方向，向键盘输入方向移动的方向
        /// </summary>
        /// <returns></returns>
        private Vector3 GetInputDirectionBaseOnCamera()
        {
	        // 输入移动方向
	        Vector3 inputDirection = new Vector3(ACTInput.Move.x, 0.0f, ACTInput.Move.y).normalized;

	        // 期望旋转
	        // 因为摄像机呼吸，MainCamera.transform.eulerAngles.y 会发生抖动，进而导致玩家在起步的时候有一个微小抖动
	        // 而 cinemachineTargetYaw 不会抖动，因此采用 cinemachineTargetYaw
	        float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cinemachineTargetYaw;
	        // 基于摄像机方向，向键盘输入方向移动的方向
	        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

	        return targetDirection;
        }
        
        /// <summary>
        /// 取消速度覆盖
        /// </summary>
        public void CancelHorizontalVelocityOverride()
        {
	        isHorizontalVelocityOverrided = false;
        }
        
        /// <summary>
        /// 默认速度覆盖方向为当前摄像机方向+键盘输入方向
        /// </summary>
        /// <param name="speed">速度</param>
        public void SetHorizontalVelocityOverride(float speed)
        {
	        isHorizontalVelocityOverrided = true;
	        horizontalVelocityOverride = speed;
        }
        
        // 后面这个可能会有强制初始方向和强制始终一个方向
        // 现在是懒得做了
        
        // /// <summary>
        // /// 允许设置速度覆盖方向
        // /// </summary>
        // /// <param name="speed">速度</param>
        // /// <param name="dir">速度方向</param>
        // public void SetHorizontalVelocityOverride(float speed, Vector3 dir)
        // {
	       //  isHorizontalVelocityOverrided = true;
	       //  HorizontalVelocityOverride = speed;
	       //  HorizontalVelocityDirectionOverride = dir;
        // }
    }
}

