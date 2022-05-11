// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 26/04/2022 10:09
// 最后一次修改于: 26/04/2022 10:10
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;

namespace MeowFramework.Core.Switchable
{
    public class SwitchableBool : SwitchableGeneric<bool>
    {
        // 实现接口
        
        /// <summary>
        /// 使变量在不同预设值之间切换
        /// </summary>
        /// <param name="mode">预设模式</param>
        public override void SwitchValue(Enum mode)
        {
            // SmoothDamp 
            if (TargetValueDict.ContainsKey(mode))
            {
                Value = TargetValueDict[mode];
            }
        }
    }
}