using System;
using System.Collections;
using UnityEngine;

namespace OaksMayFall
{
    public class ThirdPersonLocomotion
    {
	    // 常量
	    
	    /// <summary>
	    /// 微量
	    /// </summary>
	    private const float Threshold = 0.01f;
	    
	    // 移动相关
	    
	    /// <summary>
	    /// 移动速度
	    /// </summary>
	    private float _walkSpeed = 7f;
	    /// <summary>
	    /// 玩家旋转的过渡时间
	    /// </summary>
	    private float _rotationSmoothTime = 0.2f;
	    /// <summary>
	    /// 水平速度
	    /// </summary>
	    /// <returns></returns>
	    private Vector3 _horizontalVelocity;
	    /// <summary>
	    /// 竖向速度
	    /// </summary>
	    private Vector3 _verticalVelocity;
	    /// <summary>
	    /// 旋转角的过渡速度
	    /// </summary>
	    private float _rotationSmoothVelocity;
	    /// <summary>
	    /// 行走速度的过渡速度
	    /// </summary>
	    private Vector3 _walkSmoothVelocity;
	    /// <summary>
	    /// 玩家行走的过渡时间
	    /// </summary>
	    private float _walkSmoothTime = 0.2f;

	    // 冲刺相关
	    
	    /// <summary>
	    /// 冲刺速度
	    /// </summary>
	    private float _sprintSpeed = 20f;
	    // 冲刺计时器
	    private float _sprintTimeoutDelta;
	    // 冲刺的冷却时间
	    private float _sprintTimeout = 0.25f;
	    /// <summary>
	    /// 冲刺速度的过渡速度
	    /// </summary>
	    private Vector3 _sprintSmoothVelocity;
	    /// <summary>
	    /// 玩家冲刺速度的过渡时间
	    /// </summary>
	    private float _sprintSmoothTime = 0.2f;
	    
	    // 物理相关
	    
	    /// <summary>
	    /// 重力系数
	    /// </summary>
	    private float _gravity = -9.8f;
	    /// <summary>
	    /// 最大下落速度
	    /// </summary>
	    private float _terminalVelocity = 53f;
	    
	    // 落地相关
	    
	    /// <summary>
	    /// 是否落地
	    /// </summary>
	    private bool _isGrounded = true;
	    /// <summary>
	    /// 是否落地
	    /// </summary>
	    public bool IsGrounded => _isGrounded;
	    /// <summary>
	    /// 落地球形碰撞检测中心点的竖向偏移量
	    /// </summary>
	    private float _groundedOffset = -0.14f;
	    /// <summary>
	    /// 落地球形碰撞检测的半径
	    /// </summary>
	    private float _groundedRadius = 0.28f;
	    /// <summary>
	    /// 落地球形碰撞检测的层级
	    /// </summary>
	    private int _groundLayers = 1;

	    // 攻击相关
	    
	    /// <summary>
	    /// 攻击计时器
	    /// </summary>
	    private bool _isAttacking = false;
		/// <summary>
		/// 攻击的冷却时间
		/// </summary>
	    private float _attackTimeout = 1f;
	    /// <summary>
	    /// 是否能够攻击
	    /// </summary>
	    private bool ShouldAttack
	    {
		    get
		    {
			    if (_userInput.Attack && !_isAttacking)
			    {
				    _isAttacking = true;
				    var timer = GameEntry.Timer.CreateTimer(_attackTimeout, false,
					    delegate(object[] args) { _isAttacking = false; Debug.Log("_isAttacking = false");});
				    timer.Start();
				    return true;
			    }
			    else
			    {
				    return false;
			    }
		    }
	    }

	    // 摄像机相关
		
		private float _cameraAngleOverride = 0f;
	    /// <summary>
	    /// 摄像机转动的速度
	    /// </summary>
	    private float _cameraRotSpeed = 25f;
	    /// <summary>
	    /// 摄像机最大俯仰角
	    /// </summary>
	    private float _topClamp = 70f; 
	    /// <summary>
	    /// 摄像机最小俯仰角
	    /// </summary>
	    private float _bottomClamp = -30f;
	    /// <summary>
	    /// 摄像机跟随点的当前俯仰角的过渡时间
	    /// </summary>
	    private float _cinemachinePitchSmoothTime = 0.1f;
	    /// <summary>
	    /// 摄像机跟随点的当前偏航角的过渡时间
	    /// </summary>
	    private float _cinemachineYawSmoothTime = 0.1f;
	    /// <summary>
	    /// 摄像机是否固定
	    /// </summary>
	    private bool _isCameraFixed = false;
	    /// <summary>
	    /// 摄像机跟随点的期望俯仰角
	    /// </summary>
	    private float _cinemachineTargetPitch;
	    /// <summary>
	    /// 摄像机跟随点的期望偏航角
	    /// </summary>
	    private float _cinemachineTargetYaw;
	    /// <summary>
	    /// 摄像机跟随点的当前俯仰角和摄像机跟随点的当前偏航角组成的向量
	    /// </summary>
	    private Vector2 _cinemachineCurrPY;
	    /// <summary>
	    /// 摄像机跟随点的当前俯仰角的过渡速度
	    /// </summary>
	    private float _cinemachinePitchSmoothVelocity;
	    /// <summary>
	    /// 摄像机跟随点的当前俯仰角的过渡速度
	    /// </summary>
	    private float _cinemachineYawSmoothVelocity;
	    
	    // 动画相关
	    
	    /// <summary>
	    /// 动画状态机的速度参数的 id
	    /// </summary>
	    private int _animIDSpeed;
	    /// <summary>
	    /// 动画状态机的落地参数的 id
	    /// </summary>
	    private int _animIDGrounded;
	    /// <summary>
	    /// 动画状态机的自由落体参数的 id
	    /// </summary>
	    private int _animIDFreeFall;
		/// <summary>
		/// 动画状态机的攻击参数的 id
		/// </summary>
	    private int _animIDAttack;
		/// <summary>
		/// 跑步动画混合值
		/// </summary>
	    private float _moveAnimBlend;
		/// <summary>
		/// 跑步动画混合值的过渡速度
		/// </summary>
		private float _moveAnimBlendSmoothVelocity;
		/// <summary>
		/// 跑步动画混合值的过渡时间
		/// </summary>
		private float _moveAnimBlendSmoothTime = 0.2f;
		
	    // 组件

	    /// <summary>
	    /// 第三人称运动控制器的使用者的变换
	    /// </summary>
	    private Transform _userTransform;
		/// <summary>
		/// 第三人称运动控制器的使用者的角色控制器
		/// </summary>
	    private CharacterController _userCharacterController;
		/// <summary>
		/// 第三人称运动控制器的使用者的动画控制器
		/// </summary>
		private Animator _userAnimator;
		/// <summary>
		/// 第三人称运动控制器的使用者的输入控制器
		/// </summary>
		private OaksMayFallInputController _userInput;
		/// <summary>
		/// 第三人称运动控制器的使用者的摄像机跟随点
		/// </summary>
		private GameObject _cinemachineCameraFollowTarget;
		/// <summary>
		/// 主摄像机
		/// </summary>
		private GameObject _mainCamera;

		/// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="userTransform">第三人称运动控制器的使用者的变换</param>
	    /// <param name="userCharacterController">第三人称运动控制器的使用者的角色控制器</param>
	    /// <param name="userAnimator">第三人称运动控制器的使用者的动画控制器</param>
	    /// <param name="userInput">第三人称运动控制器的使用者的输入控制器</param>
	    /// <param name="cinemachineCameraFollowTarget">第三人称运动控制器的使用者的摄像机跟随点</param>
	    /// <param name="mainCamera">主摄像机</param>
	    public ThirdPersonLocomotion(Transform userTransform, CharacterController userCharacterController,
		    Animator userAnimator, OaksMayFallInputController userInput, GameObject cinemachineCameraFollowTarget,
		    GameObject mainCamera)
	    {
		    _userTransform = userTransform;
		    _userCharacterController = userCharacterController;
		    _userAnimator = userAnimator;
		    _userInput = userInput;
		    _cinemachineCameraFollowTarget = cinemachineCameraFollowTarget;
		    _mainCamera = mainCamera;
	    }

		/// <summary>
	    /// 落地检查
	    /// </summary>
	    public void GroundedCheck()
        {
	        var spherePosition = new Vector3(_userTransform.position.x, _userTransform.position.y - _groundedOffset, _userTransform.position.z);
            _isGrounded = Physics.CheckSphere(spherePosition, _groundedRadius, _groundLayers, QueryTriggerInteraction.Ignore);
        }
	    
	    /// <summary>
	    /// 应用重力
	    /// </summary>
        public void ApplyGravity()
        {
	        if (_isGrounded && _verticalVelocity.y < 0.0f)
		        _verticalVelocity.y = -2f;
	        else if (_verticalVelocity.y < _terminalVelocity)
		        _verticalVelocity.y += _gravity * Time.deltaTime;
        }

        /// <summary>
        /// 移动
        /// </summary>
        public void Move()
        {
	        // 一开始的想法是，想把冲刺做成一个常规速度之外的附加速度
	        // 之后 debug 的时候发现不好调，还是要做成分离的
	        // 只要正在冲刺，就只使用冲刺速度
	        
	        UpdateSprintTimeout();
	        
	        _horizontalVelocity = _sprintTimeoutDelta > 0f ? GetSprintSpeed() : GetNormalSpeed();
	        _userCharacterController.Move((_horizontalVelocity + _verticalVelocity) * Time.deltaTime);
        }

        private Vector3 GetSprintSpeed()
        {
	        // 输入移动方向
	        Vector3 inputDirection = new Vector3(_userInput.Move.x, 0.0f, _userInput.Move.y).normalized;
	        // 期望旋转
	        // 因为摄像机呼吸，_mainCamera.transform.eulerAngles.y 会发生抖动，进而导致玩家在起步的时候有一个微小抖动
	        // 而 _cinemachineTargetYaw 不会抖动，因此采用 _cinemachineTargetYaw
	        float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _cinemachineTargetYaw;
	        // 期望移动方向
	        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
	        
	        // 如果按下冲刺，那么初始化冲刺
	        if (_userInput.Sprint)
	        {
		        // 当前附加速度初始化为冲刺速度
		        // 不需要 SmoothDamp，这是突变的
		        // 如果没有运动输入的话，那么冲刺方向为角色当前朝向
		        if (_userInput.Move == Vector2.zero)
			        return _userTransform.forward * _sprintSpeed;
		        // 有运动的输入的话，那么冲刺方向为运动输入的方向
		        else
			        return targetDirection.normalized * _sprintSpeed;
	        }
	        // 否则冲刺速度趋向 0
	        else
				return Vector3.SmoothDamp(_horizontalVelocity, Vector3.zero, ref _sprintSmoothVelocity, _sprintSmoothTime);
        }

        private Vector3 GetNormalSpeed()
        {
	        // 输入移动方向
	        Vector3 inputDirection = new Vector3(_userInput.Move.x, 0.0f, _userInput.Move.y).normalized;
	        // 期望旋转
	        // 因为摄像机呼吸，_mainCamera.transform.eulerAngles.y 会发生抖动，进而导致玩家在起步的时候有一个微小抖动
	        // 而 _cinemachineTargetYaw 不会抖动，因此采用 _cinemachineTargetYaw
	        float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _cinemachineTargetYaw;
	        // 期望移动方向
	        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

	        Vector3 targetVelocity = (_userInput.Move == Vector2.zero) ? Vector3.zero : targetDirection * _walkSpeed;
	        
	        return Vector3.SmoothDamp(_horizontalVelocity, targetVelocity, ref _walkSmoothVelocity, _walkSmoothTime);
        }
        private void UpdateSprintTimeout()
        {
	        // 如果正在冲刺，并且计时器小于 0，说明可以开始冲刺
	        if (_userInput.Sprint && _sprintTimeoutDelta <= 0f)
		        _sprintTimeoutDelta = _sprintTimeout;

	        // 如果冲刺计时器大于 0，说明当前正在冲刺
	        if (_sprintTimeoutDelta > 0f)
		        // 冲刺计时器工作
		        _sprintTimeoutDelta -= Time.deltaTime;
        }
        /// <summary>
        /// 向移动方向旋转
        /// </summary>
        public void RotateToMoveDir()
        {
	        // 移动方向
	        Vector3 inputDirection = new Vector3(_userInput.Move.x, 0.0f, _userInput.Move.y).normalized;

	        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
	        // if there is a move input rotate player when the player is moving
	        if (_userInput.Move != Vector2.zero)
	        {
		        float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
		        float rotation = Mathf.SmoothDampAngle(_userTransform.eulerAngles.y, targetRotation, ref _rotationSmoothVelocity, _rotationSmoothTime);

		        // 玩家旋转
		        _userTransform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
	        }
        }

        public void RotateToCameraDir()
        {
	        float targetRotation = _mainCamera.transform.eulerAngles.y;
	        float rotation = Mathf.SmoothDampAngle(_userTransform.eulerAngles.y, targetRotation, ref _rotationSmoothVelocity, _rotationSmoothTime);

	        // 玩家旋转
	        _userTransform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
        
        /// <summary>
        /// 摄像机旋转
        /// </summary>
        public void CameraRotate()
        {
	        // if there is an input and camera position is not fixed
	        if (_userInput.Look.sqrMagnitude >= Threshold && !_isCameraFixed)
	        {
		        _cinemachineTargetPitch += _userInput.Look.y * Time.deltaTime * _cameraRotSpeed / 100.0f;
		        _cinemachineTargetYaw += _userInput.Look.x * Time.deltaTime * _cameraRotSpeed / 100.0f;
	        }

	        // clamp our rotations so our values are limited 360 degrees
	        _cinemachineTargetPitch = MathUtility.ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);
	        _cinemachineTargetYaw = MathUtility.ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);

	        // 平滑
	        _cinemachineCurrPY.x = Mathf.SmoothDampAngle(_cinemachineCurrPY.x, _cinemachineTargetPitch,
		        ref _cinemachinePitchSmoothVelocity, _cinemachinePitchSmoothTime);
	        _cinemachineCurrPY.y = Mathf.SmoothDampAngle(_cinemachineCurrPY.y, _cinemachineTargetYaw,
		        ref _cinemachineYawSmoothVelocity, _cinemachineYawSmoothTime);
	  
	        // Cinemachine will follow this target
	        _cinemachineCameraFollowTarget.transform.rotation = Quaternion.Euler(_cinemachineCurrPY.x + _cameraAngleOverride, _cinemachineCurrPY.y, 0.0f);
	        
	    }
        
        /// <summary>
        /// 初始化动画状态机参数
        /// </summary>
        public void AssignAnimationIDs()
        {
	        _animIDSpeed = Animator.StringToHash("ForwardSpeed");
	        _animIDGrounded = Animator.StringToHash("Grounded");
	        _animIDFreeFall = Animator.StringToHash("FreeFall");
	        _animIDAttack = Animator.StringToHash("Attack");
        }

        public void Attack()
        {
	        _userAnimator.SetTrigger(_animIDAttack);
        }

        /// <summary>
        /// 设置动画状态机参数
        /// </summary>
        public void SetAnimatorValue()
        {
	        // 着地
	        _userAnimator.SetBool(_animIDGrounded, _isGrounded);
            
	        // 跳跃
	        if (_isGrounded)
		        _userAnimator.SetBool(_animIDFreeFall, false);
	        // 自由下落
	        else
		        _userAnimator.SetBool(_animIDFreeFall, true);
            
	        // 移动
	        _moveAnimBlend = Mathf.SmoothDamp(_moveAnimBlend, _horizontalVelocity.magnitude, ref _moveAnimBlendSmoothVelocity, _moveAnimBlendSmoothTime);
	        _userAnimator.SetFloat(_animIDSpeed, _moveAnimBlend);
	        
	        // 攻击
	        if (ShouldAttack)
	        {
		        Attack();
	        }
        }
    }
}
