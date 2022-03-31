// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 29/03/2022 7:56
// 最后一次修改于: 30/03/2022 15:14
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 可资产化三维向量值
    /// </summary>
    [CreateAssetMenu(menuName = "MeowACT/Scriptable Variable/Create Scriptable Vector3")]
    public class ScriptableVector3 : ScriptableObject
    {
#if UNITY_EDITOR
        /// <summary>
        /// 开发者注释
        /// </summary>
        [Multiline]
        public string DeveloperDescription = "";
#endif
        /// <summary>
        /// 三维向量值
        /// </summary>
        public Vector3 Value;

        /// <summary>
        /// 使用三维向量值给可资产化三维向量值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(Vector3 other)
        {
            Value = other;
        }

        /// <summary>
        /// 使用整型值给可资产化三维向量值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(int other)
        {
            Value = other * Vector3.one;
        }
        
        /// <summary>
        /// 使用浮点值给可资产化三维向量值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(float other)
        {
            Value = other * Vector3.one;
        }
        
        /// <summary>
        /// 使用可资产化三维向量值给可资产化三维向量值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(ScriptableVector3 other)
        {
            Value = other.Value;
        }

        /// <summary>
        /// 使用可资产化整型值给可资产化三维向量值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(ScriptableInt other)
        {
            Value = other.Value * Vector3.one;
        }
        
        /// <summary>
        /// 使用可资产化浮点值给可资产化三维向量值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(ScriptableFloat other)
        {
            Value = other.Value * Vector3.one;
        }
        
        /// <summary>
        /// 使用三维向量值增量给可资产化三维向量值赋值
        /// </summary>
        /// <param name="amount">增量</param>
        public void ApplyChange(Vector3 amount)
        {
            Value += amount;
        }

        /// <summary>
        /// 使用整型值增量给可资产化三维向量值赋值
        /// </summary>
        /// <param name="amount">增量</param>
        public void ApplyChange(int amount)
        {
            Value += amount * Vector3.one;
        }
        
        /// <summary>
        /// 使用浮点值增量给可资产化三维向量值赋值
        /// </summary>
        /// <param name="amount">增量</param>
        public void ApplyChange(float amount)
        {
            Value += amount * Vector3.one;
        }
        
        /// <summary>
        /// 使用可资产化三维向量值增量给可资产化三维向量值赋值
        /// </summary>
        /// <param name="amount">增量</param>
        public void ApplyChange(ScriptableVector3 amount)
        {
            Value += amount.Value;
        }    
        
        /// <summary>
        /// 使用可资产化整型值增量给可资产化三维向量值赋值
        /// </summary>
        /// <param name="amount">增量</param>
        public void ApplyChange(ScriptableInt amount)
        {
            Value += amount.Value * Vector3.one;
        } 
        
        /// <summary>
        /// 使用可资产化浮点值增量给可资产化三维向量值赋值
        /// </summary>
        /// <param name="amount">增量</param>
        public void ApplyChange(ScriptableFloat amount)
        {
            Value += amount.Value * Vector3.one;
        } 
    }
}