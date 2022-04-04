// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 28/03/2022 17:43
// 最后一次修改于: 04/04/2022 20:04
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace MeowFramework.Core
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

        
    }
}