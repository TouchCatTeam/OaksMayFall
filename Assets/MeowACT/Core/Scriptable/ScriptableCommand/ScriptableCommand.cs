// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 30/03/2022 1:10
// 最后一次修改于: 31/03/2022 11:12
// 版权所有: CheapMeowStudio
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
    /// 可资产化指令
    /// </summary>
    [InlineEditor]
    [CreateAssetMenu(fileName = "New Scriptable Command", menuName = "MeowACT/Create Scriptable Command")]
    public class ScriptableCommand : SerializedScriptableObject
    {
        /// <summary>
        /// 是否有执行内容
        /// </summary>
        [BoxGroup("Main Execution")]
        [Tooltip("是否有执行内容")]
        public bool HasMainExecution;

        /// <summary>
        /// 指令执行内容种类
        /// </summary>
        [ShowIf("@HasMainExecution")]
        [EnumToggleButtons]
        [BoxGroup("Main Execution")]
        [Tooltip("指令执行内容种类")]
        public CommandExecutionType MainExecutionType;

        /// <summary>
        /// 指令执行内容为事件
        /// </summary>
        [ShowIf("@HasMainExecution && MainExecutionType != CommandExecutionType.Flow")]
        [BoxGroup("Main Execution")]
        [Tooltip("指令执行内容为事件")]
        [ItemCanBeNull]
        public List<UnityEvent<List<UnityEngine.Object>>> MainEventExecution;
        
        /// <summary>
        /// 指令执行内容
        /// </summary>
        [ShowIf("@HasMainExecution && MainExecutionType != CommandExecutionType.Event")]
        [BoxGroup("Main Execution")]
        [Tooltip("指令执行内容为 Bolt 脚本")]
        [ItemCanBeNull]
        public FlowMacro MainFlowExecution;
    }
}