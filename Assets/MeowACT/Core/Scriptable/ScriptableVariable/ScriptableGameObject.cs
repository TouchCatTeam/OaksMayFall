// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 29/03/2022 9:36
// 最后一次修改于: 30/03/2022 15:15
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 可资产化游戏对象值
    /// </summary>
    [CreateAssetMenu(menuName = "MeowACT/Scriptable Variable/Create Scriptable GameObject")]
    public class ScriptableGameObject : ScriptableObject
    {
#if UNITY_EDITOR
        /// <summary>
        /// 开发者注释
        /// </summary>
        [Multiline] public string DeveloperDescription = "";
#endif
        /// <summary>
        /// 游戏对象值
        /// </summary>
        public GameObject Value;

        /// <summary>
        /// 使用游戏对象值给可资产化游戏对象值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(GameObject other)
        {
            Value = other;
        }

        /// <summary>
        /// 使用可资产化游戏对象值给可资产化游戏对象值赋值
        /// </summary>
        /// <param name="other">右值</param>
        public void SetValue(ScriptableGameObject other)
        {
            Value = other.Value;
        }
    }
}