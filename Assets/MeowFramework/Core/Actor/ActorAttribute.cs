// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 01/04/2022 14:32
// 最后一次修改于: 02/04/2022 0:59
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core
{
    /// <summary>
    /// 角色属性泛型
    /// </summary>
    public class ActorAttribute<T> where T : IComparable, IConvertible, IEquatable<T>
    {
        /// <summary>
        /// 开发者注释
        /// </summary>
        [TextArea]
        [Tooltip("开发者注释")]
        public string DeveloperDescription = "";
        
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
            get
            {
                if (OnGetValue != null)
                    return OnGetValue(this.value);
                else
                    return this.value;
            }
            set
            {
                OnSetValue(this.value, ref value);
                this.value = value;
            }
        }
            
        /// <summary>
        /// 值改变委托的委托类型
        /// </summary>
        [HideInInspector]
        public delegate bool OnSetValueDelegate(T oldValue, ref T value);
        
        /// <summary>
        /// 获取值时进行的委托
        /// 同时承担验证验证值改变是否合法的功能
        /// </summary>
        [HideInInspector]
        public Func<T,T> OnGetValue;

        /// <summary>
        /// 设置值时进行的委托
        /// 同时承担验证验证值改变是否合法的功能
        /// </summary>
        [HideInInspector]
        public OnSetValueDelegate OnSetValue;
    }
}