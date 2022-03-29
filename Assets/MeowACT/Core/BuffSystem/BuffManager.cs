// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 27/03/2022 9:46
// 最后一次修改于: 28/03/2022 19:16
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// Buff 管理器
    /// </summary>
    public class BuffManager
    {
        /// <summary>
        /// Buff 管理器的主人
        /// </summary>
        public GameObject Owner;

        // private RList<IBuff> buffList = new RList<IBuff>();

        public List<int> bornBuffIDList = new List<int>();
        
        /// <summary>
        /// Buff 管理器的构造函数
        /// </summary>
        public BuffManager(GameObject owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// Buff 管理器初始化
        /// </summary>
        public void Init()
        {
            foreach (int id in bornBuffIDList)
            {
                
            }
        }
    }
}