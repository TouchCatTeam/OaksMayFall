// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 01/04/2022 22:21
// 最后一次修改于: 12/04/2022 15:07
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core.Scriptable
{
    /// <summary>
    /// 可资产化 float 变量
    /// </summary>
    [InlineEditor]
    [CreateAssetMenu(menuName = "MeowFramework/Scriptable Variable/Create Scriptable Float Variable")]
    public class ScriptableFloatVariable : SerializedScriptableObject
    {
        /// <summary>
        /// 值
        /// </summary>
        [ShowInInspector]
        private float value;
        
        /// <summary>
        /// 值
        /// </summary>
        public float Value
        {
            get => value;
            set
            {
                AfterSetValue?.Invoke(this.value, value);
                this.value = value;
            }
        }
        
        /// <summary>
        /// 设置值时进行的委托
        /// </summary>
        [HideInInspector]
        public Action<float,float> AfterSetValue;   
    }
}