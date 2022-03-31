// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 30/03/2022 2:02
// 最后一次修改于: 31/03/2022 10:12
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

namespace MeowACT
{
    /// <summary>
    /// 可资产化指令持续时间种类
    /// </summary>
    public enum CommandDurationType
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