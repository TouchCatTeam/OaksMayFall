// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 30/03/2022 8:55
// 最后一次修改于: 30/03/2022 9:04
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

namespace MeowACT
{
    /// <summary>
    /// 技能执行条件种类
    /// </summary>
    public enum AbilityConditionType
    {
        /// <summary>
        /// 无条件
        /// </summary>
        None = 0,
        
        /// <summary>
        /// 可资产化条件
        /// </summary>
        Scriptable,
        
        /// <summary>
        /// Bolt 脚本条件
        /// </summary>
        Flow,
        
        /// <summary>
        /// 可资产化条件和 Bolt 脚本条件都满足
        /// </summary>
        Both,
        
        /// <summary>
        /// 可资产化条件和 Bolt 脚本条件满足其一
        /// </summary>
        Either,
    }
}