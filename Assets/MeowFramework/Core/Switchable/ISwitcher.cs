// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 22/04/2022 19:08
// 最后一次修改于: 26/04/2022 9:37
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;

namespace MeowFramework.Core.Switchable
{
    /// <summary>
    /// 切换器的借口
    /// </summary>
    public interface ISwitcher
    {
        /// <summary>
        /// 可切换变量列表
        /// </summary>
        public List<ISwitchable> SwitchableList
        {
            get;
        }
    }
}