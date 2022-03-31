// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 29/03/2022 14:48
// 最后一次修改于: 31/03/2022 17:41
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections.Generic;
using System.Xml.Schema;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;

    
namespace MeowACT
{
    /// <summary>
    /// 可资产化浮点值的引用
    /// </summary>
    public class ScriptableFloatReference
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
        public float LiteralValue;

        /// <summary>
        /// 可资产化值
        /// </summary>
        [ShowIf("@!IsLiteral")]
        [Tooltip("可资产化值")] 
        public ScriptableFloat ScriptableVariable;

        /// <summary>
        /// 验证值改变是否合法的委托
        /// </summary>
        [Sirenix.OdinInspector.ReadOnly]
        [FoldoutGroup("Delegate")]
        public Func<float, bool> ValidateValueChange;

        /// <summary>
        /// 值改变成功的委托
        /// </summary>
        [Sirenix.OdinInspector.ReadOnly]
        [FoldoutGroup("Delegate")]
        public Action<float> AfterValueChangeSucceeded;
        
        /// <summary>
        /// 值改变失败的委托
        /// </summary>
        [Sirenix.OdinInspector.ReadOnly]
        [FoldoutGroup("Delegate")]
        public Action<float, float> AfterValueChangeFailed;
        
        /// <summary>
        /// 处理值改变的委托
        /// </summary>
        [Sirenix.OdinInspector.ReadOnly]
        [FoldoutGroup("Delegate")]
        public Func<float, float> ValueChangeExpression;
        
        /// <summary>
        /// 可资产化浮点值的引用的默认构造函数
        /// </summary>
        public ScriptableFloatReference()
        {
        }

        /// <summary>
        /// 可资产化浮点值的引用的使用字面值的构造函数
        /// </summary>
        /// <param name="value">字面值</param>
        public ScriptableFloatReference(float value)
        {
            IsLiteral = true;
            LiteralValue = value;
        }

        /// <summary>
        /// 引用所指向的值
        /// </summary>
        public float Value
        {
            get => IsLiteral ? LiteralValue : ScriptableVariable.Value;
            set
            {
                // 只从右值 get 一次 以适应可能的特殊的属性
                var oldValue = IsLiteral ? LiteralValue : ScriptableVariable.Value;
                // 如果事件不为空，并且新值与旧值不同，那么触发事件
                if (oldValue != value)
                    if (ValidateValueChange(value))
                        AfterValueChangeSucceeded(value);
                    else
                        AfterValueChangeFailed(oldValue, value);

                if (IsLiteral)
                    LiteralValue = value;
                else
                    ScriptableVariable.Value = value;
            }
        }

        /// <summary>
        /// 从 ScriptableFloatReference 到 float 的隐式类型转换
        /// </summary>
        /// <param name="reference">右值</param>
        /// <returns></returns>
        public static implicit operator float(ScriptableFloatReference reference)
        {
            return reference.Value;
        }
    }
}