// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 29/03/2022 15:04
// 最后一次修改于: 31/03/2022 20:36
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 可资产化二维向量角色属性
    /// </summary>
    public class ScriptableVector2Attribute
    {
        /// <summary>
        /// 是否使用字面值
        /// </summary>
        [Tooltip("是否使用字面值\n如使用字面值，则只需要配置字面值\n如不需要字面值，则只需要配置可资产化值")]
        public bool IsLiteral = true;
        
        /// <summary>
        /// 字面值
        /// </summary>
        [ShowIf("@IsLiteral")]
        [Tooltip("字面值")]
        public Vector2 LiteralValue;
        
        /// <summary>
        /// 可资产化值
        /// </summary>
        [ShowIf("@!IsLiteral")]
        [Tooltip("可资产化值")]
        public ScriptableVector2 ScriptableVariable;

        /// <summary>
        /// 是否有额外的事件绑定
        /// </summary>
        [Tooltip("是否有额外的事件绑定")]
        public bool HaveAdditionalBindingEvent;
        
        /// <summary>
        /// 值改变事件
        /// </summary>
        [ShowIf("@HaveAdditionalBindingEvent")]
        [Tooltip("在值被改变时触发的事件")]
        public ScriptableEvent ChangeEvent;

        /// <summary>
        /// 事件参数
        /// </summary>
        [ShowIf("@HaveAdditionalBindingEvent")]
        [Tooltip("在值被改变时触发的事件所要传递的参数")]
        public List<object> Args = new List<object>();
        
        /// <summary>
        /// 可资产化二维向量角色属性的默认构造函数
        /// </summary>
        public ScriptableVector2Attribute()
        { }

        /// <summary>
        /// 可资产化二维向量角色属性的使用字面值的构造函数
        /// </summary>
        /// <param name="value">字面值</param>
        public ScriptableVector2Attribute(Vector2 value)
        {
            IsLiteral = true;
            LiteralValue = value;
        }

        /// <summary>
        /// 所指向的值
        /// </summary>
        public Vector2 Value
        {
            get => IsLiteral ? LiteralValue : ScriptableVariable.Value;
            set
            {
                // 如果事件不为空，并且新值与旧值不同，那么触发事件
                if(ChangeEvent != null && (IsLiteral ? LiteralValue : ScriptableVariable.Value) != value)
                    ChangeEvent.Raise(Args);
                
                if (IsLiteral)
                    LiteralValue = value;
                else
                    ScriptableVariable.Value = value;
            }
        }

        /// <summary>
        /// 从 ScriptableVector2Attribute 到 Vector2 的隐式类型转换
        /// </summary>
        /// <param name="reference">右值</param>
        /// <returns></returns>
        public static implicit operator Vector2(ScriptableVector2Attribute reference)
        {
            return reference.Value;
        }
    }
}