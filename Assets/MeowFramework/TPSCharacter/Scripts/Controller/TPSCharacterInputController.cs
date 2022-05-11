// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 14/03/2022 9:54
// 最后一次修改于: 20/04/2022 17:57
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MeowFramework.TPSCharacter
{
	public class TPSCharacterInputController : SerializedMonoBehaviour
	{
		/// <summary>
		/// 行动模式
		/// </summary>
		[BoxGroup("Mode")]
		[ShowInInspector]
		[Sirenix.OdinInspector.ReadOnly]
		[Description("行动模式")]
		private TPSCharacterBehaviourMode mode;
		
		/// <summary>
		/// 是否接受运动输入
		/// </summary>
		[BoxGroup("InputValue")]
		[HorizontalGroup("InputValue/MoveInput")]
		[HorizontalGroup("InputValue/MoveInput/Left")]
		[ShowInInspector]
		[Sirenix.OdinInspector.ReadOnly]
		[Tooltip("是否接受运动输入")]
		private bool CanMoveInput = true;
			
		/// <summary>
		/// 移动
		/// </summary>
		[HorizontalGroup("InputValue/MoveInput/Right")]
		[LabelWidth(50)]
		[Tooltip("移动")]
		public Vector2 Move;
		
		/// <summary>
		/// 是否接受摄像机旋转输入
		/// </summary>
		[HorizontalGroup("InputValue/LookInput")]
		[HorizontalGroup("InputValue/LookInput/Left")]
		[ShowInInspector]
		[Sirenix.OdinInspector.ReadOnly]
		[Tooltip("是否接受摄像机旋转输入")]
		private bool CanLookInput = true;
		
		/// <summary>
		/// 鼠标移动
		/// </summary>
		[HorizontalGroup("InputValue/LookInput/Right")]
		[LabelWidth(50)]
		[Tooltip("鼠标移动")]
		public Vector2 Look;
		
		/// <summary>
		/// 是否接受冲刺输入
		/// </summary>
		[HorizontalGroup("InputValue/SprintInput")]
		[HorizontalGroup("InputValue/SprintInput/Left")]
		[ShowInInspector]
		[Sirenix.OdinInspector.ReadOnly]
		[Tooltip("是否接受冲刺输入")]
		private bool CanSprintInput = true;
		
		/// <summary>
		/// 冲刺
		/// </summary>
		[HorizontalGroup("InputValue/SprintInput/Right")]
		[LabelWidth(50)]
		[Tooltip("冲刺")]
		public bool Sprint;
		
		/// <summary>
		/// 是否接受冲刺输入
		/// </summary>
		[HorizontalGroup("InputValue/AttackInput")]
		[HorizontalGroup("InputValue/AttackInput/Left")]
		[ShowInInspector]
		[Sirenix.OdinInspector.ReadOnly]
		[Tooltip("是否接受冲刺输入")]
		private bool CanAttackInput = true;
		
		/// <summary>
		/// 攻击
		/// </summary>
		[HorizontalGroup("InputValue/AttackInput/Right")]
		[LabelWidth(50)]
		[Tooltip("攻击")]
		public bool Attack;
		
		/// <summary>
		/// 是否接受瞄准输入
		/// </summary>
		[HorizontalGroup("InputValue/AimInput")]
		[HorizontalGroup("InputValue/AimInput/Left")]
		[ShowInInspector]
		[Sirenix.OdinInspector.ReadOnly]
		[Tooltip("是否接受瞄准输入")]
		private bool CanAimInput = true;
		
		/// <summary>
		/// 瞄准
		/// </summary>
		[HorizontalGroup("InputValue/AimInput/Right")]
		[LabelWidth(50)]
		[Tooltip("瞄准")]
		public bool Aim;
		
		/// <summary>
		/// 鼠标锁定
		/// </summary>
		[BoxGroup("InputValue")]
		[Tooltip("鼠标锁定")]
		public bool CursorLocked = true;

		/// <summary>
		/// 移动时触发的 Action
		/// </summary>
		[HideInInspector] 
		public Action<Vector2> OnMoveAction;

		/// <summary>
		/// 鼠标移动时，能够输入时触发的 Action
		/// </summary>
		[HideInInspector]
		public Action<Vector2> OnLookAction;

		/// <summary>
		/// 右键冲刺时触发的 Action
		/// </summary>
		[HideInInspector]
		public Action OnSprintAction;

		/// <summary>
		/// 左键攻击时触发的 Action
		/// </summary>
		[HideInInspector]
		public Action OnAttackAction;

		/// <summary>
		/// 右键按下与松开时触发的 Action
		/// </summary>
		[HideInInspector]
		public Action OnAimAction;
		
		/// <summary>
		/// 应用窗口聚焦时触发的 Action
		/// </summary>
		[HideInInspector]
		public Action<bool> OnApplicationFocusAction;
		
		private void OnMove(InputValue value)
		{
			if (CanMoveInput)
			{
				Move = value.Get<Vector2>();
				OnMoveAction?.Invoke(Move);
			}
		}

		private void OnLook(InputValue value)
		{
			if (CanLookInput)
			{
				Look = value.Get<Vector2>();
				OnLookAction?.Invoke(Look);
			}
		}

		private void OnSprint(InputValue value)
		{
			if (CanSprintInput)
			{
				Sprint = value.isPressed;
				OnSprintAction?.Invoke();
			}
		}

		private void OnAttack(InputValue value)
		{
			if (CanAttackInput)
			{
				Attack = value.isPressed;
				OnAttackAction?.Invoke();
			}
		}

		private void OnAim(InputValue value)
		{
			if (CanAttackInput)
			{
				Aim = value.isPressed;
				OnAimAction?.Invoke();
			}
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(hasFocus);
			CursorLocked = hasFocus;
			OnApplicationFocusAction?.Invoke(CursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

		public void EnableMoveInput(bool shouldEnable)
		{
			CanMoveInput = shouldEnable;
			// 注意清零
			Move = Vector2.zero;
		}
		
		public void EnableLookInput(bool shouldEnable)
		{
			CanLookInput = shouldEnable;
			// 注意清零
			Look = Vector2.zero;
		}
		
		public void EnableSprintInput(bool shouldEnable)
		{
			CanSprintInput = shouldEnable;
			// 注意清零
			Sprint = false;
		}
		
		public void EnableAttackInput(bool shouldEnable)
		{
			CanAttackInput = shouldEnable;
			// 注意清零
			Attack = false;
		}
		
		public void EnableAimInput(bool shouldEnable)
		{
			CanAimInput = shouldEnable;
			// 注意清零
			Aim = false;
		}
		
		/// <summary>
		/// 设置输入模式
		/// </summary>
		public void SetInputMode(TPSCharacterBehaviourMode mode)
		{
			this.mode = mode;
			switch (mode)
			{
				case TPSCharacterBehaviourMode.NoWeapon:
				case TPSCharacterBehaviourMode.RifleIdle:
					EnableSprintInput(true);
					break;
				case TPSCharacterBehaviourMode.RifleAiming:
					EnableSprintInput(false);
					break;
			}
		}
	}
	
}