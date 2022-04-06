// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 28/03/2022 17:43
// 最后一次修改于: 05/04/2022 17:18
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using NodeCanvas.Framework;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core
{
    public class ActorBase : SerializedMonoBehaviour
    {
        /// <summary>
        /// 黑板
        /// </summary>
        [BoxGroup("Component")]
        [Tooltip("黑板")]
        public IBlackboard Blackboard;
        
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
        
    }
}