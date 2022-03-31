// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 28/03/2022 17:43
// 最后一次修改于: 30/03/2022 15:14
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using Bolt;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowACT
{
    public class ActorBase : MonoBehaviour
    {
        /// <summary>
        /// 血量
        /// </summary>
        [Tooltip("血量")] 
        public ScriptableFloatReference HP;
        
        /// <summary>
        /// 最大血量
        /// </summary>
        [Tooltip("最大血量")] 
        public ScriptableFloatReference MaxHP;
    }
}