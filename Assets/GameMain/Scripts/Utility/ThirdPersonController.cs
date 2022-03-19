using UnityEngine;

namespace OaksMayFall
{
    public class ThirdPersonController
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
	    private float _walkSpeed = 5f;
	    /// <summary>
	    /// 冲刺速度
	    /// </summary>
	    private float _sprintSpeed = 30f;
	    /// <summary>
	    /// 玩家旋转的的过渡时间
	    /// </summary>
	    private float _rotationSmoothTime = 0.2f;
	    /// <summary>
	    /// 移动速度的变化速率
	    /// </summary>
	    private float _moveSpeedChangeRate = 10f;
	    
	    // 冲刺相关
	    
	    // 冲刺计时器
	    private float _sprintTimeoutDelta;
	    // 冲刺速度的过渡时间
	    private float _sprintTimeout = 0.25f;
	    
	    // 物理相关
	    
	    /// <summary>
	    /// 重力系数
	    /// </summary>
	    private float _gravity = -15f;
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

	    // 摄像机相关
		
		private float _cameraAngleOverride = 0f;
	    /// <summary>
	    /// 摄像机转动的速度
	    /// </summary>
	    private float _cameraRotSpeed = 30f;
	    /// <summary>
	    /// 摄像机最大俯仰角
	    /// </summary>
	    private float _topClamp = 70f; 
	    /// <summary>
	    /// 摄像机最小俯仰角
	    /// </summary>
	    private float _bottomClamp = -30f;
	    
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
	    
	    // 控制缓存

	    /// <summary>
	    /// 第三人称控制器的使用者的变换
	    /// </summary>
	    private Transform _userTransform;
		/// <summary>
		/// 第三人称控制器的使用者的角色控制器
		/// </summary>
	    private CharacterController _userCharacterController;
		/// <summary>
		/// 第三人称控制器的使用者的动画控制器
		/// </summary>
		private Animator _userAnimator;
		/// <summary>
		/// 第三人称控制器的使用者的输入控制器
		/// </summary>
		private OaksMayFallInputController _userInput;
		/// <summary>
		/// 第三人称控制器的使用者的摄像机跟随点
		/// </summary>
		private GameObject _cinemachineCameraFollowTarget;
		/// <summary>
		/// 主摄像机
		/// </summary>
		private GameObject _mainCamera;
		/// <summary>
		/// 角色当前速度
		/// </summary>
		private float _currSpeed;
		/// <summary>
		/// 行走动画的混合值
		/// </summary>
		private float _animationBlend;
		/// <summary>
	    /// 速度容差
	    /// </summary>
	    private float _speedOffset = 0.1f;
		/// <summary>
		/// 冲刺速度的过渡速度
		/// </summary>
		private Vector3 _sprintSmoothVelocity;
	    /// <summary>
	    /// 旋转角的过渡速度
	    /// </summary>
	    private float _rotationSmoothVelocity;
	    /// <summary>
	    /// 竖向速度
	    /// </summary>
	    private float _verticalVelocity;
		/// <summary>
		/// 冲刺速度
		/// </summary>
	    private Vector3 _sprintVelocity = Vector3.zero;
	    /// <summary>
	    /// 附加速度
	    /// </summary>
	    private Vector3 _additionalVelocity = Vector3.zero;
	    /// <summary>
	    /// 摄像机是否固定
	    /// </summary>
	    private bool _isCameraFixed = false;
	    /// <summary>
	    /// 摄像机偏航角
	    /// </summary>
	    private float _cinemachineTargetYaw;
	    /// <summary>
	    /// 摄像机俯仰角
	    /// </summary>
	    private float _cinemachineTargetPitch;

	    /// <summary>
	    /// 构造函数
	    /// </summary>
	    /// <param name="userTransform">第三人称控制器的使用者的变换</param>
	    /// <param name="userCharacterController">第三人称控制器的使用者的角色控制器</param>
	    /// <param name="userAnimator">第三人称控制器的使用者的动画控制器</param>
	    /// <param name="userInput">第三人称控制器的使用者的输入控制器</param>
	    /// <param name="cinemachineCameraFollowTarget">第三人称控制器的使用者的摄像机跟随点</param>
	    /// <param name="mainCamera">主摄像机</param>
	    public ThirdPersonController(Transform userTransform, CharacterController userCharacterController,
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
	        if (_isGrounded && _verticalVelocity < 0.0f)
		        _verticalVelocity = -2f;
	        else if (_verticalVelocity < _terminalVelocity)
		        _verticalVelocity += _gravity * Time.deltaTime;
        }

        /// <summary>
        /// 移动
        /// </summary>
        public void Move()
        {
	        // 期望速度默认为行走速度
	        float targetSpeed = _walkSpeed;
	        // 输入移动方向
	        Vector3 inputDirection = new Vector3(_userInput.Move.x, 0.0f, _userInput.Move.y).normalized;
	        // 期望旋转
	        float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
	        // 期望移动方向
	        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
	        
	        // 如果按下冲刺，并且不在冲刺，那么就重置冲刺计时器
	        if (_userInput.Sprint && _sprintTimeoutDelta <= 0f)
	        {
		        _sprintTimeoutDelta = _sprintTimeout;
		        // 冲刺速度
		        _sprintVelocity = targetDirection.normalized * _sprintSpeed;
		        _additionalVelocity = _sprintVelocity;
	        }
	        
	        // 如果冲刺计时器大于 0，说明当前正在冲刺
	        if (_sprintTimeoutDelta > 0f)
	        {
		        // 冲刺计时器工作
		        _sprintTimeoutDelta -= Time.deltaTime;
	        }
	        // 如果不在冲刺，并且移动输入为 0，那么期望速度为 0
			else if (_userInput.Move == Vector2.zero)
		        targetSpeed = 0.0f;
	        
			// 当前速度与目标速度相差较大时，当前速度需要变化
			if (_currSpeed < targetSpeed - _speedOffset || _currSpeed > targetSpeed + _speedOffset)
			{
				// 取当前速度
				_currSpeed = new Vector3(_userCharacterController.velocity.x, 0.0f, _userCharacterController.velocity.z).magnitude;
				// 当前速度向期望速度插值
				_currSpeed = Mathf.Lerp(_currSpeed, targetSpeed, Time.deltaTime * _moveSpeedChangeRate);
				// 取三位小数
				_currSpeed = Mathf.Round(_currSpeed * 1000f) / 1000f;
			}
			// 当前速度与目标速度相差较小时，当前速度就是目标速度
			else
			{
				_currSpeed = targetSpeed;
			}

			_animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * _moveSpeedChangeRate);
			
			// 冲刺速度过渡
			_additionalVelocity = Vector3.SmoothDamp(_additionalVelocity, Vector3.zero, ref _sprintSmoothVelocity, _sprintTimeout);
			Debug.Log(_additionalVelocity);
			
			// 玩家移动
			_userCharacterController.Move(targetDirection.normalized * (_currSpeed * Time.deltaTime) +
			                              new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime +
			                              _additionalVelocity * Time.deltaTime);
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
		        _cinemachineTargetYaw += _userInput.Look.x * Time.deltaTime * _cameraRotSpeed / 100.0f;
		        _cinemachineTargetPitch += _userInput.Look.y * Time.deltaTime * _cameraRotSpeed / 100.0f;
	        }

	        // clamp our rotations so our values are limited 360 degrees
	        _cinemachineTargetYaw = MathUtility.ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
	        _cinemachineTargetPitch = MathUtility.ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

	        // Cinemachine will follow this target
	        _cinemachineCameraFollowTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + _cameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        }
        
        /// <summary>
        /// 初始化动画状态机参数
        /// </summary>
        public void AssignAnimationIDs()
        {
	        _animIDSpeed = Animator.StringToHash("Speed");
	        _animIDGrounded = Animator.StringToHash("Grounded");
	        _animIDFreeFall = Animator.StringToHash("FreeFall");
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
	        _userAnimator.SetFloat(_animIDSpeed, _animationBlend);
        }
    }
}
