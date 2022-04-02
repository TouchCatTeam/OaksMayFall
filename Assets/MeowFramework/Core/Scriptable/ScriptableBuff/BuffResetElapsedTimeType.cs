// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 31/03/2022 11:02
// 最后一次修改于: 02/04/2022 23:05
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

namespace MeowFramework.Core
{
    public enum BuffResetElapsedTimeType
    {
        /// <summary>
        /// 永远不重置 ElapsedTime
        /// </summary>
        Never = 0,
        
        /// <summary>
        /// 层数增加时重置 ElapsedTime
        /// </summary>
        WhenLayerAdd,
        
        /// <summary>
        /// 层数减少时重置 ElapsedTime
        /// </summary>
        WhenLayerRemove,
        
        /// <summary>
        /// 层数改变时重置 ElapsedTime
        /// </summary>
        WhenLayerChange,
    }
}