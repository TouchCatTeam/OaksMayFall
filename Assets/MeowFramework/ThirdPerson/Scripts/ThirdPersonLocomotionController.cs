// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 16/03/2022 16:53
// 最后一次修改于: 06/04/2022 17:31
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using System.ComponentModel;
using Cinemachine;
using MeowFramework.Core;
using MeowFramework.MeowACT;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;

namespace MeowFramework
{
	/// <summary>
	/// 第三人称运动控制器
	/// </summary>
    public class ThirdPersonLocomotionController : SerializedMonoBehaviour
    {
	    // 常量
	    
	    /// <summary>
	    /// 微量
	    /// </summary>
	    private const float Threshold = 0.01f;
	    
	    // 组件相关

	    /// <summary>
	    /// 角色控制器
	    /// </summary>
	    [BoxGroup("Component")]
	    [Required]
	    [Tooltip("角色控制器")]
	    public CharacterController CharacterCtr;

	    /// <summary>
	    /// ACT 输入控制器
	    /// </summary>
	    [BoxGroup("Component")]
	    [Required]
	    [Tooltip("ACT 输入控制器")]
	    public MeowACTInputController ACTInput;
	    
	    /// <summary>
	    /// 摄像机跟随点
	    /// </summary>
	    [BoxGroup("Component")]
	    [Required]
	    [Tooltip("摄像机跟随点")]
	    public GameObject CMCameraFollowTarget;

	    /// <summary>
	    /// 主摄像机
	    /// </summary>
	    [BoxGroup("Component")]
	    [Sirenix.OdinInspector.ReadOnly]
	    [Tooltip("主摄像机")]
	    public Camera MainCamera;
	    
	    /// <summary>
	    /// 跟随主角的摄像机
	    /// </summary>
	    [BoxGroup("Component")]
	    [Sirenix.OdinInspector.ReadOnly]
	    [Tooltip("跟随主角的摄像机")]
	    public CinemachineVirtualCamera PlayerFollowCamera;
	    
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
	    /// 水平速度覆盖值
	    /// </summary>
	    [BoxGroup("Velocity")]
	    [ShowInInspector]
	    [Sirenix.OdinInspector.ReadOnly]
	    [Tooltip("水平速度覆盖值")]
	    private float HorizontalVelocityOverride;

	    /// <summary>
	    /// 水平速度方向覆盖值
	    /// </summary>
	    private Vector3 HorizontalVelocityDirectionOverride;
	    
	    // 行走相关

	    /// <summary>
	    /// 移动速度
	    /// </summary>
	    [BoxGroup("Walk")]
	    [Tooltip("移动速度")]
	    public float walkSpeed = 7f;

	    /// <summary>
	    /// 行走速度的过渡速度
	    /// </summary>
	    private Vector3 walkSmoothVelocity;
	    
	    /// <summary>
	    /// 玩家行走的过渡时间
	    /// </summary>
	    [BoxGroup("Walk")]
	    [Tooltip("玩家行走的过渡时间")]
	    public float walkSmoothTime = 0.2f;
	    
	    /// <summary>
	    /// 旋转角的过渡速度
	    /// </summary>
	    private float rotationSmoothVelocity;
	    
	    /// <summary>
	    /// 玩家旋转的过渡时间
	    /// 如果这个值过大，由于使用了 SmoothDamp，会让镜头移动出现明显的粘滞感
	    /// </summary>
	    [BoxGroup("Walk")]
	    [Tooltip("玩家旋转的过渡时间")]
	    public float rotationSmoothTime = 0.1f;

	    // 物理相关
	    
	    /// <summary>
	    /// 重力系数
	    /// </summary>
	    [BoxGroup("Gravity")]
	    [Tooltip("重力系数")]
	    public float gravity = -9.8f;
	    
	    /// <summary>
	    /// 最大下落速度
	    /// </summary>
	    [BoxGroup("Gravity")]
	    [Tooltip("最大下落速度")]
	    public float terminalVelocity = 53f;
	    
	    // 落地相关

	    /// <summary>
	    /// 是否落地
	    /// </summary>
	    [BoxGroup("GroundCheck")]
	    [Tooltip("是否落地")]
	    public bool IsGrounded;
	    
	    /// <summary>
	    /// 落地球形碰撞检测中心点的竖向偏移量
	    /// </summary>
	    [BoxGroup("GroundCheck")]
	    [Tooltip("落地球形碰撞检测中心点的竖向偏移量")]
	    public float groundedOffset = -0.14f;
	    
	    /// <summary>
	    /// 落地球形碰撞检测的半径
	    /// </summary>
	    [BoxGroup("GroundCheck")]
	    [Tooltip("落地球形碰撞检测的半径")]
	    public float groundedRadius = 0.28f;
	    
	    /// <summary>
	    /// 落地球形碰撞检测的层级
	    /// </summary>
	    [BoxGroup("GroundCheck")]
	    [Tooltip("落地球形碰撞检测的层级")]
	    public int groundLayers = 1;

	    // 摄像机相关
		
	    /// <summary>
	    /// 摄像机是否固定
	    /// </summary>
	    [BoxGroup("Camera")]
	    [Tooltip("摄像机是否固定")]
	    public bool IsCameraFixed = false;
	    
	    /// <summary>
	    /// 摄像机俯仰角覆盖值
	    /// </summary>
		[BoxGroup("Camera")]
	    [Tooltip("摄像机俯仰角覆盖值")]
	    public float cameraPitchOverride = 0f;
		
	    /// <summary>
	    /// 摄像机转动速度
	    /// </summary>
	    [BoxGroup("Camera")]
	    [Tooltip("摄像机转动速度")]
	    public float cameraRotSpeed = 25f;
	    
	    /// <summary>
	    /// 摄像机最大俯仰角
	    /// </summary>
	    [BoxGroup("Camera")]
	    [PropertyRange(0,90)]
	    [Tooltip("摄像机最大俯仰角")]
	    public float topClamp = 70f; 
	    
	    /// <summary>
	    /// 摄像机最小俯仰角
	    /// </summary>
	    [BoxGroup("Camera")]
	    [PropertyRange(-90,0)]
	    [Tooltip("摄像机最小俯仰角")]
	    public float bottomClamp = -30f;
	    
	    /// <summary>
	    /// 摄像机跟随点的当前俯仰角的过渡时间
	    /// </summary>
	    [BoxGroup("Camera")]
	    [Tooltip("摄像机跟随点的当前俯仰角的过渡时间")]
	    public float cinemachinePitchSmoothTime = 0.1f;
	    
	    /// <summary>
	    /// 摄像机跟随点的当前偏航角的过渡时间
	    /// </summary>
	    [BoxGroup("Camera")]
	    [Tooltip("摄像机跟随点的当前偏航角的过渡时间")]
	    public float cinemachineYawSmoothTime = 0.1f;

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

	    public void Awake()
	    {
		    MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		    PlayerFollowCamera = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();
	    }

	    public void Start()
	    {
		    PlayerFollowCamera.Follow = CMCameraFollowTarget.transform;
		    
		    // 之所以不使用订阅委托的方式调用 Move RotateToMoveDir CameraRotate
		    // 是因为他们有着 Update LateUpdate 的先后顺序要求
		    // 同时平滑功能也要求它们是每帧调用的
	    }

	    protected void Update()
	    {
		    ApplyGravity();
		    GroundedCheck();
		    Move();
	    }

	    protected void LateUpdate()
	    {
		    CameraRotate();
	    }
	    
	    /// <summary>
	    /// 落地检查
	    /// </summary>
	    private void GroundedCheck()
        {
	        var spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            IsGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
        }
	    
	    /// <summary>
	    /// 应用重力
	    /// </summary>
        private void ApplyGravity()
        {
	        if (IsGrounded && VerticalVelocity.Value < 0.0f)
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
	        Vector3 targetDirection = GetInputDirectionBaseOnCamera();

	        RotateToMoveDir(targetDirection);
		        
	        Vector3 targetVelocity;
	        if (HorizontalVelocityOverride != 0)
		        targetVelocity = HorizontalVelocityDirectionOverride * HorizontalVelocityOverride;
	        else
				targetVelocity = (ACTInput.Move == Vector2.zero) ? Vector3.zero : targetDirection * walkSpeed;

	        return Vector3.SmoothDamp(HorizontalVelocity.Value, targetVelocity, ref walkSmoothVelocity, walkSmoothTime);
        }

        /// <summary>
        /// 向移动方向旋转
        /// </summary>
        private void RotateToMoveDir(Vector3 inputDirection)
        {
	        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
	        // if there is a move input rotate player when the player is moving
	        if (ACTInput.Move != Vector2.zero || HorizontalVelocityOverride != 0)
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
        
        public void SetHorizontalVelocityOverride(float speed)
        {
	        HorizontalVelocityOverride = speed;
	        if(speed != 0)
		        HorizontalVelocityDirectionOverride = GetInputDirectionBaseOnCamera();
        }
        
        
    }
}

