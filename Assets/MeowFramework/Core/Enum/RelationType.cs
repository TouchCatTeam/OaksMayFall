// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/03/2022 14:58
// 最后一次修改于: 30/03/2022 15:15
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

namespace MeowFramework.Core
{
    /// <summary>
    /// 关系类型。
    /// </summary>
    public enum RelationType : byte
    {
        /// <summary>
        /// 未知的。
        /// </summary>
        Unknown,

        /// <summary>
        /// 友好的。
        /// </summary>
        Friendly,

        /// <summary>
        /// 中立的。
        /// </summary>
        Neutral,

        /// <summary>
        /// 敌对的。
        /// </summary>
        Hostile
    }
}
