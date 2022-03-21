using Cinemachine;
using UnityEngine;
using UnityGameFramework.Runtime;
using System.Collections;

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
        
        private ThirdPersonLocomotion _thirdPersonLocomotion;

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
            
            _thirdPersonLocomotion = new ThirdPersonLocomotion(transform, _controller, _animator, _input,
                _cinemachineCameraTarget, _mainCamera);

            _thirdPersonLocomotion.AssignAnimationIDs();

            StartCoroutine(HPCo());
        }

        private IEnumerator HPCo()
        {
            yield return new WaitForSeconds(5);
            Debug.Log(11111111111111);
            GameEntry.HPBar.ShowHPBar(this, 1.0f,0.5f);
            yield return null;
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            
            _thirdPersonLocomotion.ApplyGravity();
            _thirdPersonLocomotion.GroundedCheck();
            _thirdPersonLocomotion.Move();
            _thirdPersonLocomotion.RotateToMoveDir();
            _thirdPersonLocomotion.SetAnimatorValue();
        }

        protected void LateUpdate()
        {
            _thirdPersonLocomotion.CameraRotate();
        }
    }
}

