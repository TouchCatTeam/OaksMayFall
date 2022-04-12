// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 11/04/2022 9:49
// 最后一次修改于: 12/04/2022 19:11
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core.FrameworkComponent
{
    public class FrameworkComponent : SerializedMonoBehaviour
    {
        /// <summary>
        /// 初始化组件
        /// </summary>
        [Required]
        [Tooltip("初始化组件")]
        public InitializationComponent Initialization;

        /// <summary>
        /// Buff 组件
        /// </summary>
        [Required]
        [Tooltip("Buff 组件")]
        public BuffComponent Buff;
    }
}