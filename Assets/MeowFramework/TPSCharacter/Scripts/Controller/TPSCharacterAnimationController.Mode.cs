// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/04/2022 15:55
// 最后一次修改于: 26/04/2022 9:42
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using MeowFramework.Core.Switchable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.TPSCharacter
{
	/// <summary>
    /// 第三人称动画状态机控制器
    /// </summary>
    public partial class TPSCharacterAnimationController
    {
	    /// <summary>
	    /// 行动模式
	    /// </summary>
	    [BoxGroup("Mode")]
	    [Tooltip("行动模式")]
	    public SwitcherEnum<TPSCharacterBehaviourMode> SwitcherMode = new SwitcherEnum<TPSCharacterBehaviourMode>();

	    /// <summary>
	    /// 动画状态机第 0 层的权重
	    /// </summary>
	    [BoxGroup("Mode")]
	    [Tooltip("动画状态机第 0 层的权重")]
	    public SwitchableFloat AnimLayer0Weight = new SwitchableFloat();
	    
	    /// <summary>
	    /// 动画状态机第 1 层的权重
	    /// </summary>
	    [BoxGroup("Mode")]
	    [Tooltip("动画状态机第 1 层的权重")]
	    public SwitchableFloat AnimLayer1Weight = new SwitchableFloat();
	    
	    /// <summary>
	    /// 动画状态机第 2 层的权重
	    /// </summary>
	    [BoxGroup("Mode")]
	    [Tooltip("动画状态机第 2 层的权重")]
	    public SwitchableFloat AnimLayer2Weight = new SwitchableFloat();

	    /// <summary>
	    /// 步枪待机姿态所用到的骨骼绑定的权重
	    /// </summary>
	    [BoxGroup("Mode")]
	    [Tooltip("步枪待机姿态所用到的骨骼绑定的权重")]
	    public SwitchableFloat RifleIdleRigWeight = new SwitchableFloat();
	    
	    /// <summary>
	    /// 步枪瞄准姿态所用到的骨骼绑定的权重
	    /// </summary>
	    [BoxGroup("Mode")]
	    [Tooltip("步枪瞄准姿态所用到的骨骼绑定的权重")]
	    public SwitchableFloat RifleAimingRigWeight = new SwitchableFloat();

	    /// <summary>
        /// 初始化可切换变量列表
        /// </summary>
        private void InitSwitchableList()
	    {
		    // Switcher 注册
		    SwitcherMode.Owner = this;
		    // Switchable 注册
		    SwitcherMode.SwitchableList.AddRange(new List<ISwitchable>
		    {
			    AnimLayer0Weight,
			    AnimLayer1Weight,
			    AnimLayer2Weight,
			    RifleIdleRigWeight,
			    RifleAimingRigWeight
		    });
		    // Switchable 数值绑定
	        AnimLayer0Weight.AfterValueChangeAction += (oldValue, newValue) => { Anim.SetLayerWeight(0, newValue); };
	        AnimLayer1Weight.AfterValueChangeAction += (oldValue, newValue) => { Anim.SetLayerWeight(1, newValue); };
	        AnimLayer2Weight.AfterValueChangeAction += (oldValue, newValue) => { Anim.SetLayerWeight(2, newValue); };
	        RifleIdleRigWeight.AfterValueChangeAction += (oldValue, newValue) => { rifleIdleRig.weight = newValue; };
	        RifleAimingRigWeight.AfterValueChangeAction += (oldValue, newValue) => { rifleAimingRig.weight = newValue; };
        }
    }
}