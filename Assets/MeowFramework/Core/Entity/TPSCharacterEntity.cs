// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/04/2022 14:45
// 最后一次修改于: 20/04/2022 2:36
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using MeowFramework.Core.Scriptable;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace MeowFramework.Core.Entity
{
    public class TPSCharacterEntity : ActorEntity
    {
        /// <summary>
        /// 血量
        /// </summary>
        [Header("Attribute")]
        [Tooltip("血量")] 
        public ScriptableFloatVariable HP;

        /// <summary>
        /// 最大血量
        /// </summary>
        [Tooltip("最大血量")] 
        public ScriptableFloatVariable MaxHP;
    }
}