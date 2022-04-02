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
    /// 阵营类型。
    /// </summary>
    public enum CampType : byte
    {
        Unknown = 0,

        /// <summary>
        /// 第一玩家阵营。
        /// </summary>
        Player,

        /// <summary>
        /// 第一敌人阵营。
        /// </summary>
        Enemy,

        /// <summary>
        /// 第一中立阵营。
        /// </summary>
        Neutral,

        /// <summary>
        /// 第二玩家阵营。
        /// </summary>
        Player2,

        /// <summary>
        /// 第二敌人阵营。
        /// </summary>
        Enemy2,

        /// <summary>
        /// 第二中立阵营
        /// </summary>
        Neutral2,
    }
}
