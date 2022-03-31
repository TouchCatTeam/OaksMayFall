// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 27/03/2022 9:51
// 最后一次修改于: 31/03/2022 19:01
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 可素材化 Buff
    /// </summary>
    [CreateAssetMenu(fileName = "New Scriptable Buff", menuName = "MeowACT/Create Scriptable Buff")]
    public class ScriptableBuff : SerializedScriptableObject
    {
        /// <summary>
        /// 堆叠层数限制
        /// </summary>
        [EnumToggleButtons]
        [BoxGroup("Layer")]
        [Tooltip("堆叠层数限制")]
        public BuffLayerStackLimitType LayerStackLimit;

        /// <summary>
        /// 最大堆叠层数
        /// </summary>
        [ShowIf("@LayerStackLimit == BuffLayerStackLimitType.Limited")]
        [BoxGroup("Layer")]
        [Tooltip("最大堆叠层数")]
        public int MaxLayers;

        /// <summary>
        /// 什么时候应该重置 Command
        /// </summary>
        [EnumToggleButtons]
        [ShowIf("@LayerStackLimit == BuffLayerStackLimitType.Limited && MaxLayers == 0")]
        [BoxGroup("Layer")]
        [Tooltip("什么时候应该重置命令")]
        public BuffResetCommandType WhenShouldResetCommand;
        
        /// <summary>
        /// 指令持续时间种类
        /// </summary>
        [EnumToggleButtons]
        [BoxGroup("Time")]
        [Tooltip("指令持续时间种类")]
        public CommandDurationType DurationType;

        /// <summary>
        /// 指令持续时间
        /// </summary>
        [ShowIf("DurationType",CommandDurationType.Durable)]
        [BoxGroup("Time")]
        [ValidateInput("DurationValidate")]
        [Tooltip("指令持续时间")]
        public float Duration;

        /// <summary>
        /// 什么时候应该重置 ElapsedTime
        /// </summary>
        [EnumToggleButtons]
        [ShowIf("@DurationType == CommandDurationType.Durable || DurationType == CommandDurationType.Infinite")]
        [BoxGroup("Time")]
        [Tooltip("什么时候应该重置已流逝时间")]
        public BuffResetElapsedTimeType WhenShouldResetElapsedTimeType;
        
        /// <summary>
        /// 是否能够被打断
        /// </summary>
        [ShowIf("@DurationType == CommandDurationType.Durable || DurationType == CommandDurationType.Infinite")]
        [BoxGroup("Time")]
        [Tooltip("是否能够被打断")]
        public bool CanBeInterrupted;

        
        /// <summary>
        /// Buff 元素 Tag 字典
        /// </summary>
        [Tooltip("元素标签字典")]
        public Dictionary<Element, bool> ElementTagDictionary = new Dictionary<Element, bool>
        {
            {Element.Pyro, false},
            {Element.Hydro, false},
            {Element.Anemo, false},
            {Element.Electro, false},
            {Element.Cryo, false},
            {Element.Geo, false},
        };
        
        /// <summary>
        /// 指令
        /// </summary>
        [Tooltip("指令")]
        public ScriptableCommand Command;
        
        /// <summary>
        /// 验证函数：是否为非负数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool DurationValidate(float value) => MathUtility.IsntNegative(value);
    }
}