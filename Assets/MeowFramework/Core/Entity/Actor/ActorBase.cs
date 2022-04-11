// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 28/03/2022 17:43
// 最后一次修改于: 11/04/2022 23:56
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using MeowFramework.Core.FrameworkComponent;
using MeowFramework.Core.Scriptable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core.Entity
{
    public class ActorBase : SerializedMonoBehaviour
    {
        /// <summary>
        /// 血量
        /// </summary>
        [BoxGroup("Attribute")]
        [Tooltip("血量")] 
        public ActorAttribute<float> HP = new ActorAttribute<float>();
        
        /// <summary>
        /// 最大血量
        /// </summary>
        [BoxGroup("Attribute")]
        [Tooltip("最大血量")] 
        public ActorAttribute<float> MaxHP = new ActorAttribute<float>();

        /// <summary>
        /// 出生时附加的 Buff 的字典
        /// </summary>
        [BoxGroup("GamePlay")]
        [Tooltip("出生时附加的 Buff 的字典")]
        public Dictionary<int,ScriptableBuff> bornBuffDictionary = new Dictionary<int,ScriptableBuff>();

        /// <summary>
        /// 运行时的 Buff 的列表
        /// </summary>
        [BoxGroup("GamePlay")]
        [ShowInInspector]
        [Sirenix.OdinInspector.ReadOnly]
        [Tooltip("运行时的 Buff 的列表")]
        private Dictionary<int, BuffEntity> aliveBuffList = new Dictionary<int, BuffEntity>();
    }
}