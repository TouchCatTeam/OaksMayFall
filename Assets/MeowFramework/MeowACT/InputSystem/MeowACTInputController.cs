// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 14/03/2022 9:54
// 最后一次修改于: 05/04/2022 0:25
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace MeowFramework.MeowACT
{
	public class MeowACTInputController : SerializedMonoBehaviour
	{
		/// <summary>
		/// 移动
		/// </summary>
		[BoxGroup("Move")]
		[Tooltip("移动")]
		public Vector2 Move;
		
		/// <summary>
		/// 鼠标移动
		/// </summary>
		[BoxGroup("Move")]
		[Tooltip("鼠标移动")]
		public Vector2 Look;
		
		/// <summary>
		/// 冲刺
		/// </summary>
		[BoxGroup("Move")]
		[Tooltip("冲刺")]
		public bool Sprint;
		
		/// <summary>
		/// 攻击
		/// </summary>
		[BoxGroup("Combat")]
		[Tooltip("攻击")]
		public bool Attack;
		
		/// <summary>
		/// 鼠标锁定
		/// </summary>
		[BoxGroup("Cursor")]
		[Tooltip("鼠标锁定")]
		public bool CursorLocked = true;
		
		/// <summary>
		/// 鼠标控制摄像机
		/// </summary>
		[BoxGroup("Cursor")]
		[Tooltip("鼠标控制摄像机")]
		public bool CursorInputForLook = true;

		private void OnMove(InputValue value)
		{
			Move = value.Get<Vector2>();
		}

		private void OnLook(InputValue value)
		{
			if (CursorInputForLook)
				Look = value.Get<Vector2>();
		}

		private void OnSprint(InputValue value)
		{
			Sprint = value.isPressed;
		}

		private void OnAttack(InputValue value)
		{
			Attack = value.isPressed;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(CursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}