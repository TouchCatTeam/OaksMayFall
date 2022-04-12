// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 10/04/2022 14:04
// 最后一次修改于: 12/04/2022 0:20
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.ComponentModel;
using MeowFramework.Core.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.TPSCharacter
{
    /// <summary>
    /// 第三人称 UI 控制器
    /// </summary>
    public partial class TPSCharacterUIController : SerializedMonoBehaviour
    {
        // 组件
        
        /// <summary>
        /// 瞄准 UI
        /// </summary>
        [Required]
        [BoxGroup("Component")]
        [Description("瞄准 UI")]
        public GameObject AimUI;
        
        /// <summary>
        /// 血量 UI
        /// </summary>
        [Required]
        [BoxGroup("Component")]
        [Description("血量 UI")]
        public GameObject HPUI;
        
        /// <summary>
        /// 体力条 UI
        /// </summary>
        [Required]
        [BoxGroup("Component")]
        [Description("体力条 UI")]
        public GameObject NRGUI;
        
    }
}