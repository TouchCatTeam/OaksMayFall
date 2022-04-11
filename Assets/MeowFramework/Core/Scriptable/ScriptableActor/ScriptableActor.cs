// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 05/04/2022 14:39
// 最后一次修改于: 12/04/2022 0:14
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core.Scriptable
{
    /// <summary>
    /// 可资产化 Buff
    /// </summary>
    [CreateAssetMenu(fileName = "New Scriptable Actor",
        menuName = "MeowFramework/Scriptable Actor/Create Scriptable Actor")]
    public class ScriptableActor : SerializedScriptableObject
    {
        /// <summary>
        /// Actor ID
        /// </summary>
        [Tooltip("Actor ID")] 
        public int ActorID;

        /// <summary>
        /// 可以使用的技能列表
        /// </summary>
        [Tooltip("可以使用的技能列表")] 
        public List<int> AbilityList = new List<int>();
    }
}