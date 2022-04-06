// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 05/04/2022 15:25
// 最后一次修改于: 05/04/2022 15:45
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core.Scriptable.ScriptableAbility
{
    [CreateAssetMenu(fileName = "New Scriptable Ability",
        menuName = "MeowFramework/Scriptable Ability/Create Scriptable Ability")]
    public class ScriptableAbility : SerializedScriptableObject
    {
        /// <summary>
        /// Ability ID
        /// </summary>
        [Tooltip("Ability ID")]
        public int AbilityID;
    }
}