// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 01/04/2022 22:19
// 最后一次修改于: 02/04/2022 23:05
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core
{
    /// <summary>
    /// 可资产化变量的泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [InlineEditor]
    public class ScriptableGenericVariable<T> : SerializedScriptableObject
    {
        /// <summary>
        /// 开发者注释
        /// </summary>
        [TextArea]
        [Tooltip("开发者注释")]
        public string DeveloperDescription = "";

        /// <summary>
        /// 可资产化变量的值
        /// </summary>
        [Tooltip("可资产化变量的值")]
        public T Value;
    }
}