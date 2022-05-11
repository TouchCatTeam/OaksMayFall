// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/04/2022 15:53
// 最后一次修改于: 26/04/2022 9:42
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using MeowFramework.Core.Switchable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.TPSCharacter
{
    /// <summary>
    /// 第三人称 UI 控制器
    /// </summary>
    public partial class TPSCharacterUIController
    {
        /// <summary>
        /// 行动模式
        /// </summary>
        [BoxGroup("Mode")]
        [Tooltip("行动模式")]
        public SwitcherEnum<TPSCharacterBehaviourMode> SwitcherMode = new SwitcherEnum<TPSCharacterBehaviourMode>();

        /// <summary>
        /// 瞄准 UI 的透明度
        /// </summary>
        [BoxGroup("Mode")]
        [Tooltip("瞄准 UI 的透明度")]
        public SwitchableFloat AimUIAlpha = new SwitchableFloat();
        
        /// <summary>
        /// 初始化可切换变量列表
        /// </summary>
        private void InitSwitchableList()
        {
            // Switcher 注册
            SwitcherMode.Owner = this;
            // Switchable 注册
            SwitcherMode.SwitchableList.AddRange(new List<ISwitchable>{
                AimUIAlpha,
            });
            // Switchable 数值绑定
            AimUIAlpha.AfterValueChangeAction += (oldValue, newValue) => { AimUI.GetComponent<CanvasGroup>().alpha = newValue; };
        }
    }
}