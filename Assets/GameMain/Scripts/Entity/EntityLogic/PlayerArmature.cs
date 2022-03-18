using Cinemachine;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace OaksMayFall
{
    /// <summary>
    /// 玩家类。
    /// </summary>
    public class PlayerArmature : UEntity
    {
        [SerializeField] private PlayerArmatureData playerArmatureData = null;
        
        private bool _isGrounded = true;
        
        // cinemachine
        private bool _isCameraFixed = false;
        private float _cameraAngleOverride = 0f;
        private GameObject _cinemachineCameraTarget = null;
        private GameObject _playerFollowCamera = null;
        private float _cinemachineTargetYaw = 0f;
        private float _cinemachineTargetPitch = 0f;
        
        // player
        private float _speed = 0f;
        private float _animationBlend = 0f;
        private float _inputMagnitude = 0f;
        private float _rotationVelocity = 0f;
        private float _verticalVelocity = 0f;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta = 0f;
        private float _fallTimeoutDelta = 0f;

        // animation IDs
        private int _animIDSpeed = 0;
        private int _animIDGrounded = 0;
        private int _animIDJump = 0;
        private int _animIDFreeFall = 0;
        private int _animIDMotionSpeed = 0;
        
        private Animator _animator = null;
        private CharacterController _controller = null;
        private OaksMayFallInputController _input = null;
        private GameObject _mainCamera = null;
        private SimRigidBodyPush _simRigidBodyPush = null;
        
        private const float Threshold = 0.01f;

        private bool _hasAnimator = false;

        protected override void OnInit(object userData)

        {
            base.OnInit(userData);
        }
        
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            playerArmatureData = userData as PlayerArmatureData;
            if (playerArmatureData == null)
            {
                Log.Error("Bullet data is invalid.");
                return;
            }
            
            // 获取主摄像机的引用
            if (_mainCamera == null)
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            
            // 添加输入控制器
            _input = gameObject.AddComponent<OaksMayFallInputController>();
            // 添加刚体推力
            _simRigidBodyPush = gameObject.AddComponent<SimRigidBodyPush>();
            
            _cinemachineCameraTarget = transform.Find("PlayerCameraRoot").gameObject;
            _playerFollowCamera = GameObject.Find("PlayerFollowCamera");
            _playerFollowCamera.GetComponent<CinemachineVirtualCamera>().Follow = _cinemachineCameraTarget.transform;
               
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<OaksMayFallInputController>();
            
            // 初始化动画状态机参数
            AssignAnimationIDs();
            
            // 重置跳跃计时器
            _jumpTimeoutDelta = playerArmatureData.JumpTimeout;
            _fallTimeoutDelta = playerArmatureData.FallTimeout;
        }
        
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            // 设置动画状态机参数
            if (_hasAnimator)
                SetAnimatorValue();
            
            // 跳跃
            ThirdPersonControllerUtility.JumpAndGravity(_input, _isGrounded, ref _fallTimeoutDelta, ref _jumpTimeoutDelta,
                playerArmatureData.FallTimeout, playerArmatureData.JumpTimeout,
                ref _verticalVelocity, playerArmatureData.JumpHeight, playerArmatureData.Gravity, _terminalVelocity);
            
            // 着地
            _isGrounded = ThirdPersonControllerUtility.IsGrounded(transform, playerArmatureData.GroundedOffset,
                playerArmatureData.GroundedRadius, playerArmatureData.GroundLayers);
            
            // 移动
            ThirdPersonControllerUtility.Move(_input, _controller, transform, _mainCamera,
                playerArmatureData.MoveSpeed, playerArmatureData.SpeedChangeRate, ref _speed, _verticalVelocity,
                ref _rotationVelocity, playerArmatureData.RotationSmoothTime, ref _animationBlend, ref _inputMagnitude);
        }

        protected void LateUpdate()
        {
            // 摄像机运动
            ThirdPersonControllerUtility.CameraRotation(_input, _cinemachineCameraTarget, Threshold, _isCameraFixed,
                ref _cinemachineTargetYaw, ref _cinemachineTargetPitch, playerArmatureData.CameraRotSpeed, playerArmatureData.BottomClamp,
                playerArmatureData.TopClamp, _cameraAngleOverride);
        }
        
        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (_isGrounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;
			
            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - playerArmatureData.GroundedOffset, transform.position.z), playerArmatureData.GroundedRadius);
        }
        
        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }
        
        private void SetAnimatorValue()
        {
            // 着地
            _animator.SetBool(_animIDGrounded, _isGrounded);
            
            // 跳跃
            if (_isGrounded)
            {
                _animator.SetBool(_animIDJump, false);
                _animator.SetBool(_animIDFreeFall, false);
                
                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                    _animator.SetBool(_animIDJump, true);
            }
            // 自由下落
            else if(_fallTimeoutDelta < 0.0f)
                _animator.SetBool(_animIDFreeFall, true);
            
            // 移动
            _animator.SetFloat(_animIDSpeed, _animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, _inputMagnitude);
        }
    }
}

