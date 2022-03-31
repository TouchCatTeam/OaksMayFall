// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 29/03/2022 1:20
// 最后一次修改于: 31/03/2022 15:55
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 可资产化字符串值
    /// </summary>
    [CreateAssetMenu(menuName = "MeowACT/Scriptable Variable/Create Scriptable String")]
    public class ScriptableString : SerializedScriptableObject
    {
#if UNITY_EDITOR
        /// <summary>
        /// 开发者注释
        /// </summary>
        [Multiline]
        public string DeveloperDescription = "";
#endif
        /// <summary>
        /// 字符串值
        /// </summary>
        public string Value;

        /// <summary>
        /// 使用字符串值给可资产化字符串值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(string other)
        {
            Value = other;
        }

        /// <summary>
        /// 使用可资产化字符串值给可资产化字符串值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(ScriptableString other)
        {
            Value = other.Value;
        }

        /// <summary>
        /// 使用字符串值增量给可资产化字符串值赋值
        /// </summary>
        /// <param name="amount">增量</param>
        public void ApplyChange(string amount)
        {
            Value += amount;
        }

        /// <summary>
        /// 使用可资产化字符串值增量给可资产化字符串值赋值
        /// </summary>
        /// <param name="amount">增量</param>
        public void ApplyChange(ScriptableString amount)
        {
            Value += amount.Value;
        }    
    }
}