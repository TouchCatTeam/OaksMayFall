// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/04/2022 14:44
// 最后一次修改于: 12/04/2022 14:49
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core.Entity
{
    public class ActorEntity : FlowScriptEntity
    {
        /// <summary>
        /// 出生时附加的 Buff 的列表
        /// </summary>
        [Header("GamePlay")]
        [Tooltip("出生时附加的 Buff 的列表")]
        public List<int> bornBuffList = new List<int>();

        /// <summary>
        /// 运行时的 Buff 的列表
        /// </summary>
        [ShowInInspector]
        [Sirenix.OdinInspector.ReadOnly]
        [Tooltip("运行时的 Buff 的列表")]
        private List<BuffEntity> aliveBuffList = new List<BuffEntity>();
    }
}