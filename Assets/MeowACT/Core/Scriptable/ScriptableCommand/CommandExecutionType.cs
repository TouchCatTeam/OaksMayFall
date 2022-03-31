// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 30/03/2022 8:30
// 最后一次修改于: 31/03/2022 10:12
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

namespace MeowACT
{
    /// <summary>
    /// 可资产化指令执行内容种类
    /// </summary>
    public enum CommandExecutionType
    {
        /// <summary>
        /// 可资产化指令执行内容为事件
        /// </summary>
        Event = 0,
        
        /// <summary>
        /// 可资产化指令执行内容为 Bolt 脚本
        /// </summary>
        Flow,
        
        /// <summary>
        /// 可资产化指令执行内容为事件和 Bolt 脚本
        /// </summary>
        Both,
    }
}