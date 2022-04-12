// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/04/2022 14:45
// 最后一次修改于: 12/04/2022 14:51
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using MeowFramework.Core.Scriptable;
using Sirenix.OdinInspector;
using UnityEngine;

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