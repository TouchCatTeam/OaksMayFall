// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 14/03/2022 9:54
// 最后一次修改于: 30/03/2022 15:14
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace MeowFramework.Core
{
	public class MeowFramework.CoreInputController : MonoBehaviour
	{
		private Vector2 move;
		private Vector2 look;
		private bool jump;
		private bool sprint;
		private bool attack;
		
		public Vector2 Move => move;
		public Vector2 Look => look;
		public bool Sprint => sprint;
		public bool Attack => attack;
		
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

		private void OnAttack(InputValue value)
		{
			attack = value.isPressed;
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