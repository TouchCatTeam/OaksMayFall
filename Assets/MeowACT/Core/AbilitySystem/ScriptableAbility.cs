// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 30/03/2022 1:10
// 最后一次修改于: 30/03/2022 11:49
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using Bolt;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace MeowACT
{
    /// <summary>
    /// 可素材化技能
    /// </summary>
    [CreateAssetMenu(fileName = "New Scriptable Ability", menuName = "MeowACT/Create Scriptable Ability")]
    public class ScriptableAbility : SerializedScriptableObject
    {
        /// <summary>
        /// 验证函数：是否为非负数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool DurationValidate(float value) => MathUtility.IsntNegative(value);
        
        /// <summary>
        /// 技能持续时间种类
        /// </summary>
        [EnumToggleButtons]
        [BoxGroup("Time")]
        [Tooltip("技能持续时间种类")]
        public AbilityDurationType DurationType;

        /// <summary>
        /// 技能持续时间
        /// </summary>
        [ShowIf("DurationType",AbilityDurationType.Durable)]
        [BoxGroup("Time")]
        [ValidateInput("DurationValidate")]
        [Tooltip("技能持续时间")]
        public float Duration;

        /// <summary>
        /// 是否能够被打断
        /// </summary>
        [ShowIf("@DurationType == AbilityDurationType.Durable || DurationType == AbilityDurationType.Infinite")]
        [BoxGroup("Time")]
        [Tooltip("是否能够被打断")]
        public bool CanBeInterrupted;

        /// <summary>
        /// 打断时是否需要执行内容
        /// </summary>
        [ShowIf("@CanBeInterrupted && (DurationType == AbilityDurationType.Durable || DurationType == AbilityDurationType.Infinite)")]
        [BoxGroup("Time")]
        [Tooltip("是否能够被打断")]
        public bool HasInterruptedExecution;
        
        /// <summary>
        /// 技能执行延迟
        /// </summary>
        [ShowIf("@HasInterruptedExecution && (DurationType == AbilityDurationType.Durable || DurationType == AbilityDurationType.Infinite)")]
        [BoxGroup("Time/Interrupted Execution")]
        [ValidateInput("DurationValidate")]
        [Tooltip("技能执行延迟")]
        public float InterruptedExecutionDelay;
        
        /// <summary>
        /// 技能执行内容种类
        /// </summary>
        [ShowIf("@HasInterruptedExecution && (DurationType == AbilityDurationType.Durable || DurationType == AbilityDurationType.Infinite)")]
        [EnumToggleButtons]
        [BoxGroup("Time/Interrupted Execution")]
        [Tooltip("技能执行内容种类")]
        public AbilityExecutionType InterruptedExecutionType;

        /// <summary>
        /// 技能执行内容为事件
        /// </summary>
        [ShowIf("@HasInterruptedExecution && InterruptedExecutionType != AbilityExecutionType.Flow && (DurationType == AbilityDurationType.Durable || DurationType == AbilityDurationType.Infinite)")]
        [BoxGroup("Time/Interrupted Execution")]
        [Tooltip("技能执行内容为事件")]
        [ItemCanBeNull]
        public List<UnityEvent<List<UnityEngine.Object>>> InterruptedEventExecution;
        
        /// <summary>
        /// 技能执行内容
        /// </summary>
        [ShowIf("@HasInterruptedExecution && InterruptedExecutionType != AbilityExecutionType.Event && (DurationType == AbilityDurationType.Durable || DurationType == AbilityDurationType.Infinite)")]
        [BoxGroup("Time/Interrupted Execution")]
        [Tooltip("技能执行内容为 Bolt 脚本")]
        [ItemCanBeNull]
        public List<FlowMacro> InterruptedFlowExecution;
        
        /// <summary>
        /// 技能执行条件种类
        /// </summary>
        [EnumToggleButtons]
        [BoxGroup("Condition")]
        [Tooltip("技能执行条件种类")]
        public AbilityConditionType ConditionType;
        
        /// <summary>
        /// 打断时是否需要执行内容
        /// </summary>
        [BoxGroup("Main Execution")]
        [Tooltip("是否能够被打断")]
        public bool HasMainExecution;
        
        /// <summary>
        /// 技能执行延迟
        /// </summary>
        [ShowIf("@HasMainExecution")]
        [BoxGroup("Main Execution")]
        [ValidateInput("DurationValidate")]
        [Tooltip("技能执行延迟")]
        public float MainExecutionDelay;
        
        /// <summary>
        /// 技能执行内容种类
        /// </summary>
        [ShowIf("@HasMainExecution")]
        [EnumToggleButtons]
        [BoxGroup("Main Execution")]
        [Tooltip("技能执行内容种类")]
        public AbilityExecutionType MainExecutionType;

        /// <summary>
        /// 技能执行内容为事件
        /// </summary>
        [ShowIf("@HasMainExecution && MainExecutionType != AbilityExecutionType.Flow")]
        [BoxGroup("Main Execution")]
        [Tooltip("技能执行内容为事件")]
        [ItemCanBeNull]
        public List<UnityEvent<List<UnityEngine.Object>>> MainEventExecution;
        
        /// <summary>
        /// 技能执行内容
        /// </summary>
        [ShowIf("@HasMainExecution && MainExecutionType != AbilityExecutionType.Event")]
        [BoxGroup("Main Execution")]
        [Tooltip("技能执行内容为 Bolt 脚本")]
        [ItemCanBeNull]
        public List<FlowMacro> MainFlowExecution;
    }
}