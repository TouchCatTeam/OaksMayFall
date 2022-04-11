// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 01/04/2022 14:32
// 最后一次修改于: 11/04/2022 21:13
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using MeowFramework.Core.Scriptable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core.Entity
{
    /// <summary>
    /// 角色属性泛型
    /// </summary>
    public class ActorAttribute<T>
    {
        /// <summary>
        /// 是否使用字面值
        /// </summary>
        [Tooltip("是否使用字面值")]
        public bool IsLiteral = true;

        /// <summary>
        /// 角色属性的值
        /// </summary>
        [ShowIf("@IsLiteral")]
        [Tooltip("值")]
        private T value;

        /// <summary>
        /// 可资产化值
        /// </summary>
        [ShowIf("@!IsLiteral")]
        [ShowInInspector]
        [Tooltip("可资产化值")]
        private ScriptableGenericVariable<T> scriptableValue;

        public T Value
        {
            get => value;
            set
            {
                AfterSetValue(this.value, value);
                this.value = value;
            }
        }
        
        /// <summary>
        /// 设置值时进行的委托
        /// </summary>
        [HideInInspector]
        public Action<T,T> AfterSetValue;
    }
}