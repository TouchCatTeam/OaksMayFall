// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 30/03/2022 2:02
// 最后一次修改于: 30/03/2022 7:54
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

namespace MeowACT
{
    /// <summary>
    /// 技能持续时间种类
    /// </summary>
    public enum AbilityDurationType
    {
        /// <summary>
        /// 瞬间
        /// </summary>
        Instant = 0,
        
        /// <summary>
        /// 持续一帧
        /// </summary>
        OneFrame,
        
        /// <summary>
        /// 持续一段时间
        /// </summary>
        Durable,
        
        /// <summary>
        /// 无限时间
        /// </summary>
        Infinite,
    }
}