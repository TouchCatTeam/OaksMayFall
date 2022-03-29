// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 30/03/2022 1:10
// 最后一次修改于: 30/03/2022 7:54
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 可素材化技能
    /// </summary>
    [CreateAssetMenu(fileName = "New Scriptable Ability", menuName = "MeowACT/Create Scriptable Ability")]
    public class ScriptableAbility : ScriptableObject
    {
        /// <summary>
        /// 技能持续时间种类
        /// </summary>
        [EnumToggleButtons]
        [Tooltip("技能持续时间种类")]
        public AbilityDurationType DurationType;

        [ShowIf("DurationType",AbilityDurationType.Durable)]
        [Tooltip("技能持续时间")]
        public float Duration;
    }
}