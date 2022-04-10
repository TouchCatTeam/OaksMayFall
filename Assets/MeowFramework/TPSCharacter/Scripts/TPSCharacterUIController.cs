// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 10/04/2022 14:04
// 最后一次修改于: 10/04/2022 14:23
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.TPSCharacter
{
    /// <summary>
    /// 第三人称 UI 控制器
    /// </summary>
    public class TPSCharacterUIController : SerializedMonoBehaviour
    {
        /// <summary>
        /// 瞄准 UI
        /// </summary>
        [Required]
        [Description("瞄准 UI")]
        public GameObject AimUI;
        
        /// <summary>
        /// 血量 UI
        /// </summary>
        [Required]
        [Description("血量 UI")]
        public GameObject HPUI;
        
        /// <summary>
        /// 体力条 UI
        /// </summary>
        [Required]
        [Description("体力条 UI")]
        public GameObject NRGUI;
        
    }
}