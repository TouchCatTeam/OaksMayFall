// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 11/04/2022 10:59
// 最后一次修改于: 11/04/2022 23:56
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using MeowFramework.Core.Scriptable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core.FrameworkComponent
{
    /// <summary>
    /// 框架中的 Buff 组件
    /// </summary>
    [InlineEditor]
    public class BuffComponent : BaseComponent
    {
        /// <summary>
        /// Buff 对象池的根节点
        /// </summary>
        public static Transform BuffPoolRoot;

        private void Awake()
        {
            // 创建技能对象池的根节点
            GameObject root = new GameObject();
            root.name = "AbilityPoolRoot";
            root.transform.SetParent(transform);
            BuffPoolRoot = root.transform;
        }
        
        /// <summary>
        /// 技能字典
        /// </summary>
        [Sirenix.OdinInspector.ReadOnly]
        [ShowInInspector]
        [Description("Buff 字典")]
        public static Dictionary<int, ScriptableBuff> ScriptableBuffDictionary = new Dictionary<int, ScriptableBuff>();
    }
}