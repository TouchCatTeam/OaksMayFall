// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 11/04/2022 19:28
// 最后一次修改于: 11/04/2022 20:26
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

namespace MeowFramework.Core.Scriptable
{
    /// <summary>
    /// 可资产化 Buff 持续时间种类
    /// </summary>
    public enum BuffDurationType
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