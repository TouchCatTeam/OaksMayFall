// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 29/03/2022 1:21
// 最后一次修改于: 29/03/2022 9:58
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 可资产化布尔值
    /// </summary>
    [CreateAssetMenu(menuName = "MeowACT/Scriptable Variable/Create Scriptable Boolean")]
    public class ScriptableBoolean : ScriptableObject
    {
#if UNITY_EDITOR
        /// <summary>
        /// 开发者注释
        /// </summary>
        [Multiline]
        public string DeveloperDescription = "";
#endif
        /// <summary>
        /// 布尔值
        /// </summary>
        public bool Value;

        /// <summary>
        /// 使用布尔值给可资产化布尔值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(bool other)
        {
            Value = other;
        }

        /// <summary>
        /// 使用可资产化布尔值给可资产化布尔值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(ScriptableBoolean other)
        {
            Value = other.Value;
        }
    }
}