// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 27/03/2022 9:51
// 最后一次修改于: 11/04/2022 23:56
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using FlowCanvas;
using MeowFramework.Core.FrameworkComponent;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core.Scriptable
{
    /// <summary>
    /// 可资产化 Buff
    /// </summary>
    [CreateAssetMenu(fileName = "New Scriptable Buff", menuName = "MeowFramework/Scriptable Buff/Create Scriptable Buff")]
    public class ScriptableBuff : SerializedScriptableObject
    {
        /// <summary>
        /// Buff ID
        /// </summary>
        [ValidateInput("IDValidate")]
        [Tooltip("Buff ID")]
        public int BuffID;
        
        /// <summary>
        /// 俗名
        /// </summary>
        [Tooltip("俗名")]
        public string FriendlyName;
        
        /// <summary>
        /// 概述
        /// </summary>
        [TextArea]
        [Tooltip("概述")]
        public string Description = "";
        
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
        public BuffDurationType DurationType;

        /// <summary>
        /// 指令持续时间
        /// </summary>
        [ShowIf("DurationType",BuffDurationType.Durable)]
        [BoxGroup("Time")]
        [ValidateInput("DurationValidate")]
        [Tooltip("指令持续时间")]
        public float Duration;

        /// <summary>
        /// 什么时候应该重置 ElapsedTime
        /// </summary>
        [EnumToggleButtons]
        [ShowIf("@DurationType == BuffDurationType.Durable || DurationType == BuffDurationType.Infinite")]
        [BoxGroup("Time")]
        [Tooltip("什么时候应该重置已流逝时间")]
        public BuffResetElapsedTimeType WhenShouldResetElapsedTimeType;
        
        /// <summary>
        /// 是否能够被打断
        /// </summary>
        [ShowIf("@DurationType == BuffDurationType.Durable || DurationType == BuffDurationType.Infinite")]
        [BoxGroup("Time")]
        [Tooltip("是否能够被打断")]
        public bool CanBeInterrupted;
        
        /// <summary>
        /// Buff 元素 Tag 字典
        /// </summary>
        [Tooltip("元素标签字典")]
        public Dictionary<ElementType, bool> ElementTypeTagDictionary = new Dictionary<ElementType, bool>
        {
            {ElementType.Pyro, false},
            {ElementType.Hydro, false},
            {ElementType.Anemo, false},
            {ElementType.Electro, false},
            {ElementType.Cryo, false},
            {ElementType.Geo, false},
        };
        
        /// <summary>
        /// Buff FlowScript
        /// </summary>
        [Tooltip("FlowScript")]
        public FlowScript BuffFlowScript;

        /// <summary>
        /// 初始时注册 Buff ID
        /// </summary>
        private void OnEnable()
        {
            // 根据新的 ID 添加键值对
            BuffComponent.ScriptableBuffDictionary[BuffID] = this;
        }

        /// <summary>
        /// 验证函数：是否为非负数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IDValidate(int value) => value >= 0;
        
        /// <summary>
        /// 验证函数：是否为非负数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool DurationValidate(float value) => value >= 0;
    }
}