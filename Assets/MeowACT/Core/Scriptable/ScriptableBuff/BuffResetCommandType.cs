// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 31/03/2022 10:48
// 最后一次修改于: 31/03/2022 11:05
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

namespace MeowACT
{
    /// <summary>
    /// Buff 重置 Command
    /// </summary>
    public enum BuffResetCommandType
    {
        /// <summary>
        /// 永远不重置 Command
        /// </summary>
        Never = 0,
        
        /// <summary>
        /// 层数增加时重置 Command
        /// </summary>
        WhenLayerAdd,
        
        /// <summary>
        /// 层数减少时重置 Command
        /// </summary>
        WhenLayerRemove,
        
        /// <summary>
        /// 层数改变时重置 Command
        /// </summary>
        WhenLayerChange,
    }
}