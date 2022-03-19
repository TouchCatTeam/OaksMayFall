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
        [SerializeField] private PlayerArmatureData playerArmatureData;

        private CharacterController _controller;
        private Animator _animator;
        private OaksMayFallInputController _input;
        private GameObject _cinemachineCameraTarget;
        private GameObject _playerFollowCamera;
        private GameObject _mainCamera;
        private SimRigidBodyPush _simRigidBodyPush;
        
        private ThirdPersonController _thirdPersonController;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            playerArmatureData = userData as PlayerArmatureData;
            if (playerArmatureData == null)
            {
                Log.Error("PlayerArmatureData is invalid.");
                return;
            }
            
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _input = gameObject.AddComponent<OaksMayFallInputController>();
            _cinemachineCameraTarget = transform.Find("PlayerCameraRoot").gameObject;
            _playerFollowCamera = GameObject.Find("PlayerFollowCamera");
            _playerFollowCamera.GetComponent<CinemachineVirtualCamera>().Follow = _cinemachineCameraTarget.transform;
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            _simRigidBodyPush = gameObject.AddComponent<SimRigidBodyPush>();
            
            _thirdPersonController = new ThirdPersonController(transform, _controller, _animator, _input,
                _cinemachineCameraTarget, _mainCamera);

            _thirdPersonController.AssignAnimationIDs();
            
        }
        
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            
            _thirdPersonController.ApplyGravity();
            _thirdPersonController.GroundedCheck();
            _thirdPersonController.Move();
            _thirdPersonController.RotateToMoveDir();
            _thirdPersonController.SetAnimatorValue();
        }

        protected void LateUpdate()
        {
            _thirdPersonController.CameraRotate();
        }
    }
}

