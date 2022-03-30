// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 30/03/2022 8:30
// 最后一次修改于: 30/03/2022 9:04
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

namespace MeowACT
{
    /// <summary>
    /// 技能执行内容种类
    /// </summary>
    public enum AbilityExecutionType
    {
        /// <summary>
        /// 技能执行内容为事件
        /// </summary>
        Event = 0,
        
        /// <summary>
        /// 技能执行内容为 Bolt 脚本
        /// </summary>
        Flow,
        
        /// <summary>
        /// 技能执行内容为事件和 Bolt 脚本
        /// </summary>
        Both,
    }
}