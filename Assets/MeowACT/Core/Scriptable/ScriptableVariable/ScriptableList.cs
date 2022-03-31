// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 29/03/2022 1:31
// 最后一次修改于: 30/03/2022 15:14
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 可资产化列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [CreateAssetMenu(menuName = "MeowACT/Scriptable Variable/Create Scriptable List")]
    public class ScriptableList<T> : ScriptableObject
    {
        /// <summary>
        /// 列表
        /// </summary>
        public List<T> Items = new List<T>();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="thing"></param>
        public void Add(T thing)
        {
            if (!Items.Contains(thing))
                Items.Add(thing);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="thing"></param>
        public void Remove(T thing)
        {
            if (Items.Contains(thing))
                Items.Remove(thing);
        }
    }
}