// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 28/03/2022 17:43
// 最后一次修改于: 31/03/2022 19:02
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using Bolt;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace MeowACT
{
    public class ActorBase : SerializedMonoBehaviour
    {
        /// <summary>
        /// 血量
        /// </summary>
        [Tooltip("血量")] 
        public ScriptableFloatReference HP = new ScriptableFloatReference();
        
        /// <summary>
        /// 最大血量
        /// </summary>
        [Tooltip("最大血量")] 
        public ScriptableFloatReference MaxHP = new ScriptableFloatReference();

        public UnityEvent<float> testEvent;
        
        public Func<bool> BeforeActorDie;
        
        public void ExecuteBuff()
        {
            
        }

        public void ExecuteAbility()
        {
            
        }

        
    }
}