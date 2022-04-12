// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/04/2022 14:44
// 最后一次修改于: 12/04/2022 20:36
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace MeowFramework.Core.Entity
{
    public class ActorEntity : FlowScriptEntity
    {
        /// <summary>
        /// 运行时的 Buff 的字典
        /// </summary>
        [Sirenix.OdinInspector.ReadOnly]
        [Tooltip("运行时的 Buff 的字典")]
        public Dictionary<int, BuffEntity> AliveBuffDictionary = new Dictionary<int, BuffEntity>();
    }
}