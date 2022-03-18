using UnityEngine;

namespace OaksMayFall
{
    public static class ThirdPersonControllerUtility
    {
	    public static bool IsGrounded(Transform selfTransform, float groundedOffset, float groundedRadius, int groundLayers)
        {
            Vector3 spherePosition = new Vector3(selfTransform.position.x, selfTransform.position.y - groundedOffset, selfTransform.position.z);
            bool isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

            return isGrounded;
        }
        
        // public static void JumpAndGravity(OaksMayFallInputController input, bool isGrounded, ref float fallTimeoutDelta, ref float jumpTimeoutDelta, float fallTimeout, float jumpTimeout,
	       //  ref float verticalVelocity, float jumpHeight, float gravity, float terminalVelocity)
        // {
	       //  if (isGrounded)
	       //  {
		      //   // reset the fall timeout timer
		      //   fallTimeoutDelta = fallTimeout;
        //
		      //   // stop our velocity dropping infinitely when isGrounded
		      //   if (verticalVelocity < 0.0f)
			     //    verticalVelocity = -2f;
        //
		      //   // Jump
		      //   if (input.Jump && jumpTimeoutDelta <= 0.0f)
			     //    // the square root of H * -2 * G = how much velocity needed to reach desired height
			     //    verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        //
		      //   // jump timeout
		      //   if (jumpTimeoutDelta >= 0.0f)
			     //    jumpTimeoutDelta -= Time.deltaTime;
	       //  }
	       //  else
	       //  {
		      //   // reset the jump timeout timer
		      //   jumpTimeoutDelta = jumpTimeout;
        //
		      //   // fall timeout
		      //   if (fallTimeoutDelta >= 0.0f)
			     //    fallTimeoutDelta -= Time.deltaTime;
        //
		      //   // if we are not isGrounded, do not jump
		      //   input.Jump = false;
	       //  }
        //
	       //  // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
	       //  if (verticalVelocity < terminalVelocity)
		      //   verticalVelocity += gravity * Time.deltaTime;
        // }

        public static void Gravity(bool isGrounded, ref float verticalVelocity, float gravity, float terminalVelocity)
        {
	        if (isGrounded)
	        {
		        // stop our velocity dropping infinitely when isGrounded
		        if (verticalVelocity < 0.0f)
			        verticalVelocity = -2f;
	        }
	        else
	        {
		        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
		        if (verticalVelocity < terminalVelocity)
			        verticalVelocity += gravity * Time.deltaTime;
	        }
        }
        
        public static void Move(OaksMayFallInputController input, CharacterController controller, Transform selfTransform, GameObject mainCamera,
	        float moveSpeed, float speedChangeRate, ref float speed, float verticalVelocity, ref float rotationVelocity, float rotationSmoothTime,
	        ref float animationBlend, ref float inputMagnitude)
        {
	        // set target speed based on move speed, sprint speed and if sprint is pressed
			// float targetSpeed = input.sprint ? SprintSpeed : MoveSpeed;
			float targetSpeed = moveSpeed;

			// 目标旋转
			float targetRotation = 0.0f;
			
			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (input.Move == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			inputMagnitude = input.analogMovement ? input.Move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);

				// round speed to 3 decimal places
				speed = Mathf.Round(speed * 1000f) / 1000f;
			}
			else
			{
				speed = targetSpeed;
			}

			animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);
			
			// normalise input direction
			Vector3 inputDirection = new Vector3(input.Move.x, 0.0f, input.Move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (input.Move != Vector2.zero)
			{
				// 如果出现人物只有上下左右四个朝向的情况，说明 mainCamera.transform.eulerAngles.y == 0
				// 如果摄像机不转动
				targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
				float rotation = Mathf.SmoothDampAngle(selfTransform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);

				// rotate to face input direction relative to camera position
				selfTransform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}
			
			Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

			// move the player
			controller.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
		}
        
        public static void CameraRotation(OaksMayFallInputController input, GameObject cinemachineCameraTarget, float threshold, bool isCameraFixed,
	        ref float cinemachineTargetYaw, ref float cinemachineTargetPitch, float cameraRotSpeed, float bottomClamp, float topClamp, float cameraAngleOverride)
        {
	        // if there is an input and camera position is not fixed
	        if (input.Look.sqrMagnitude >= threshold && !isCameraFixed)
	        {
		        cinemachineTargetYaw += input.Look.x * Time.deltaTime * cameraRotSpeed / 100.0f;
		        cinemachineTargetPitch += input.Look.y * Time.deltaTime * cameraRotSpeed / 100.0f;
	        }

	        // clamp our rotations so our values are limited 360 degrees
	        cinemachineTargetYaw = MathUtility.ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
	        cinemachineTargetPitch = MathUtility.ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);

	        // Cinemachine will follow this target
	        cinemachineCameraTarget.transform.rotation = Quaternion.Euler(cinemachineTargetPitch + cameraAngleOverride, cinemachineTargetYaw, 0.0f);
        }
    }
}
