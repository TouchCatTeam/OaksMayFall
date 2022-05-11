// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 22/04/2022 9:01
// 最后一次修改于: 22/04/2022 21:33
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using UnityEngine;

namespace MeowFramework.Core.Switchable
{
    /// <summary>
    /// 可切换浮点
    /// </summary>
    public class SwitchableFloat : SwitchableGeneric<float>
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
                float target = TargetValueDict[mode];
                Value = Mathf.SmoothDamp(Value, target, ref smoothVelocity, SmoothTime);
            }
        }
    }
}