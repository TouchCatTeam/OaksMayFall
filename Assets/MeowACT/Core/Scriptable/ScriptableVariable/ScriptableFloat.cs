// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 29/03/2022 1:19
// 最后一次修改于: 31/03/2022 15:46
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 可资产化浮点值
    /// </summary>
    [CreateAssetMenu(menuName = "MeowACT/Scriptable Variable/Create Scriptable Float")]
    public class ScriptableFloat : SerializedScriptableObject
    {
#if UNITY_EDITOR
        /// <summary>
        /// 开发者注释
        /// </summary>
        [Multiline]
        public string DeveloperDescription = "";
#endif
        /// <summary>
        /// 浮点值
        /// </summary>
        public float Value;

        /// <summary>
        /// 使用浮点值给可资产化浮点值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(float other)
        {
            Value = other;
        }
        
        /// <summary>
        /// 使用整型值给可资产化浮点值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(int other)
        {
            Value = other;
        }

        /// <summary>
        /// 使用可资产化浮点值给可资产化浮点值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(ScriptableFloat other)
        {
            Value = other.Value;
        }

        /// <summary>
        /// 使用可资产化整型值给可资产化浮点值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(ScriptableInt other)
        {
            Value = other.Value;
        }
        
        /// <summary>
        /// 使用浮点值增量给可资产化浮点值赋值
        /// </summary>
        /// <param name="amount">增量</param>
        public void ApplyChange(float amount)
        {
            Value += amount;
        }

        /// <summary>
        /// 使用整型值增量给可资产化浮点值赋值
        /// </summary>
        /// <param name="amount">增量</param>
        public void ApplyChange(int amount)
        {
            Value += amount;
        }
        
        /// <summary>
        /// 使用可资产化浮点值增量给可资产化浮点值赋值
        /// </summary>
        /// <param name="amount">增量</param>
        public void ApplyChange(ScriptableFloat amount)
        {
            Value += amount.Value;
        }    
        
        /// <summary>
        /// 使用可资产化整型值增量给可资产化浮点值赋值
        /// </summary>
        /// <param name="amount">增量</param>
        public void ApplyChange(ScriptableInt amount)
        {
            Value += amount.Value;
        }   
    }
}