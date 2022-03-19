using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace OaksMayFall
{
	public class OaksMayFallInputController : MonoBehaviour
	{
		private Vector2 move;
		private Vector2 look;
		private bool jump;
		private bool sprint;
		
		public Vector2 Move => move;
		public Vector2 Look => look;
		public bool Sprint => sprint;
		
#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		private void OnMove(InputValue value)
		{
			move = value.Get<Vector2>();
		}

		private void OnLook(InputValue value)
		{
			if (cursorInputForLook)
				look = value.Get<Vector2>();
		}

		private void OnSprint(InputValue value)
		{
			sprint = value.isPressed;
		}
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}