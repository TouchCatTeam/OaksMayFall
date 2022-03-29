// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 27/03/2022 9:51
// 最后一次修改于: 28/03/2022 19:16
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using Bolt;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 可素材化 Buff
    /// </summary>
    [CreateAssetMenu(fileName = "New Scriptable Buff", menuName = "MeowACT/Create Scriptable Buff")]
    public class ScriptableBuff : ScriptableObject
    {
        /// <summary>
        /// 堆叠层数
        /// </summary>
        [Tooltip("堆叠层数")]
        public int Layers;

        /// <summary>
        /// 最大堆叠层数，负数表示无限
        /// </summary>
        [Tooltip("最大堆叠层数，负数表示无限")]
        public int MaxLayers;

        /// <summary>
        /// 时长，负数表示无限
        /// </summary>
        [Tooltip("时长，负数表示无限")]
        public float Duration;

        /// <summary>
        /// 复数层数时，时长是否能够叠加
        /// </summary>
        [Tooltip("复数层数时，时长是否能够叠加")]
        public bool CanDurationStacked;

        /// <summary>
        /// 复数层数时，Bolt 脚本是否能够重复执行
        /// </summary>
        [Tooltip("复数层数时，Bolt 脚本是否能够重复执行")]
        public bool CanFlowScriptRepeated;
        
        /// <summary>
        /// 标签列表
        /// </summary>
        [Tooltip("标签列表")]
        public List<BuffTag> Tags;
        
        /// <summary>
        /// Bolt 脚本
        /// </summary>
        [Tooltip("Bolt 脚本")]
        public Flow FlowScript;
    }
}