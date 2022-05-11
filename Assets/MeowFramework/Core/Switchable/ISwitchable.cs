// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 22/04/2022 9:00
// 最后一次修改于: 25/04/2022 10:57
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;

namespace MeowFramework.Core.Switchable
{
    /// <summary>
    /// 切换变量的接口
    /// </summary>
    public interface ISwitchable
    {
        /// <summary>
        /// 使变量在不同预设值之间切换
        /// </summary>
        /// <param name="mode">预设模式</param>
        public void SwitchValue(Enum mode);
    }
}