// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 29/03/2022 15:01
// 最后一次修改于: 29/03/2022 16:31
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 可资产化触发器值的引用
    /// </summary>
    [Serializable]
    public class ScriptableTriggerReference
    {
        /// <summary>
        /// 是否使用字面值
        /// </summary>
        [Tooltip("是否使用字面值\n如使用字面值，则只需要配置字面值\n如不需要字面值，则只需要配置可资产化值")]
        public bool IsLiteral = true;
        
        /// <summary>
        /// 字面值
        /// </summary>
        [Tooltip("字面值")]
        public bool LiteralValue;
        
        /// <summary>
        /// 可资产化值
        /// </summary>
        [Tooltip("可资产化值")]
        public ScriptableBoolean Variable;

        /// <summary>
        /// 值改变事件
        /// </summary>
        public ScriptableEvent ChangeEvent;

        /// <summary>
        /// 触发器事件
        /// 也就是从真变回假时的事件
        /// </summary>
        [Tooltip("在值被改变时触发的事件")]
        public ScriptableEvent TriggerEvent;
        
        /// <summary>
        /// 事件参数
        /// </summary>
        [Tooltip("在值被改变时触发的事件所要传递的参数")]
        public List<UnityEngine.Object> Args;
        
        /// <summary>
        /// 可资产化触发器值的引用的默认构造函数
        /// </summary>
        public ScriptableTriggerReference()
        { }

        /// <summary>
        /// 可资产化触发器值的引用的使用字面值的构造函数
        /// </summary>
        /// <param name="value">字面值</param>
        public ScriptableTriggerReference(bool value)
        {
            IsLiteral = true;
            LiteralValue = value;
        }

        /// <summary>
        /// 引用所指向的值
        /// </summary>
        public bool Value
        {
            get
            {
                if (IsLiteral ? LiteralValue : Variable.Value == true)
                {
                    // 如果触发器事件不为空，那么触发触发器事件
                    if(TriggerEvent != null)
                        TriggerEvent.Raise(Args);
                    
                    if (IsLiteral)
                        LiteralValue = false;
                    else
                        Variable.Value = false;
                }
                return IsLiteral ? LiteralValue : Variable.Value;
            }
            set
            {
                // 如果事件不为空，并且新值与旧值不同，那么触发事件
                if(ChangeEvent != null && (IsLiteral ? LiteralValue : Variable.Value) != value)
                    ChangeEvent.Raise(Args);
                
                if (IsLiteral)
                    LiteralValue = value;
                else
                    Variable.Value = value;
            }
        }

        /// <summary>
        /// 从 ScriptableTriggerReference 到 bool 的隐式类型转换
        /// </summary>
        /// <param name="reference">右值</param>
        /// <returns></returns>
        public static implicit operator bool(ScriptableTriggerReference reference)
        {
            return reference.Value;
        }
    }
}