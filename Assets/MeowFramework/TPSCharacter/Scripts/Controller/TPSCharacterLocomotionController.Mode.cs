// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/04/2022 15:48
// 最后一次修改于: 26/04/2022 10:38
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using Cinemachine;
using MeowFramework.Core.Switchable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.TPSCharacter
{
	/// <summary>
	/// 第三人称运动控制器
	/// </summary>
    public partial class TPSCharacterLocomotionController
    {
	    /// <summary>
	    /// 行动模式
	    /// </summary>
	    [BoxGroup("Mode")]
	    [Tooltip("行动模式")]
	    public SwitcherEnum<TPSCharacterBehaviourMode> SwitcherMode = new SwitcherEnum<TPSCharacterBehaviourMode>();

	    /// <summary>
	    /// 行走速度
	    /// </summary>
	    [BoxGroup("Mode")]
	    [Tooltip("行走速度")]
	    public SwitchableFloat WalkSpeed = new SwitchableFloat();
	    
	    /// <summary>
	    /// 摄像机 FOV
	    /// </summary>
	    [BoxGroup("Mode")]
	    [Tooltip("摄像机 FOV")]
	    public SwitchableFloat CameraFOV = new SwitchableFloat();
	    
	    /// <summary>
	    /// 摄像机侧向位置
	    /// </summary>
	    [BoxGroup("Mode")]
	    [Tooltip("摄像机侧向位置")]
	    public SwitchableFloat CameraSide = new SwitchableFloat();

	    /// <summary>
	    /// 是否应该向着摄像机的前方向旋转
	    /// </summary>
	    [BoxGroup("Mode")]
	    [Tooltip("是否应该向着摄像机的前方向旋转")]
	    public SwitchableBool ShouldRotateToCameraForward = new SwitchableBool();
	    
	    /// <summary>
        /// 初始化可切换变量列表
        /// </summary>
        private void InitSwitchableList()
        {
	        // 摄像机第三人称跟随组件
	        var camera3rdPersonFollow =
		        PlayerFollowCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

	        // Switcher 注册
	        SwitcherMode.Owner = this;
	        // Switchable 注册
	        SwitcherMode.SwitchableList.AddRange(new List<ISwitchable>{
		        WalkSpeed,
		        CameraFOV,
		        CameraSide,
		        ShouldRotateToCameraForward
	        });
	        // Switchable 数值绑定
	        WalkSpeed.AfterValueChangeAction += (oldValue, newValue) => { walkSpeed = newValue; };
            CameraFOV.AfterValueChangeAction += (oldValue, newValue) => { PlayerFollowCamera.m_Lens.FieldOfView = newValue; };
            CameraSide.AfterValueChangeAction += (oldValue, newValue) => { camera3rdPersonFollow.CameraSide = newValue; };
        }
    }
}

