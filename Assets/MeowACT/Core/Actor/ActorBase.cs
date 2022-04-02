// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 28/03/2022 17:43
// 最后一次修改于: 02/04/2022 16:20
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
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
        public ActorAttribute<float> HP = new ActorAttribute<float>();
        
        /// <summary>
        /// 最大血量
        /// </summary>
        [Tooltip("最大血量")] 
        public ActorAttribute<float> MaxHP = new ActorAttribute<float>();

        public bool TestFuncDebug()
        {
            Debug.Log("TestFunc is working in C# script");
            return true;
        }
        public void ExecuteBuff()
        {
            
        }

        public void ExecuteAbility()
        {
            
        }

        
    }
}