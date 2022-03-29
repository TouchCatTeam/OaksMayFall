// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 26/03/2022 14:46
// 最后一次修改于: 28/03/2022 21:15
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MeowACT
{
    /// <summary>
    /// 第三人称事件管理器
    /// </summary>
    public class ThirdPersonEventManager
    {
        // 事件相关

        /// <summary>
        /// 开始冲刺
        /// </summary>
        public event Action<object[]> BeginSprintEvent
        {
            add => eventMap["BeginSprintEvent"] = value;
            remove => eventMap["BeginSprintEvent"] = value;
        }
        
        /// <summary>
        /// 开始冲刺之后一帧
        /// </summary>
        public event Action<object[]> AfterBeginSprintEvent
        {
            add => eventMap["AfterBeginSprintEvent"] = value;
            remove => eventMap["AfterBeginSprintEvent"] = value;
        }
        
        /// <summary>
        /// 结束冲刺
        /// </summary>
        public event Action<object[]> EndSprintEvent
        {
            add => eventMap["EndSprintEvent"] = value;
            remove => eventMap["EndSprintEvent"] = value;
        }
        
        /// <summary>
        /// 开始近战攻击
        /// </summary>
        public event Action<object[]> BeginMeleeAttackEvent
        {
            add => eventMap["BeginMeleeAttackEvent"] = value;
            remove => eventMap["BeginMeleeAttackEvent"] = value;
        }
        
        /// <summary>
        /// 结束近战攻击
        /// </summary>
        public event Action<object[]> EndMeleeAttackEvent
        {
            add => eventMap["EndMeleeAttackEvent"] = value;
            remove => eventMap["EndMeleeAttackEvent"] = value;
        }
        
        /// <summary>
        /// 尝试造成伤害
        /// </summary>
        public event Action<object[]> TryDoDamageEvent
        {
            add => eventMap["TryDoDamageEvent"] = value;
            remove => eventMap["TryDoDamageEvent"] = value;
        }
        
        /// <summary>
        /// 结束连击
        /// </summary>
        public event Action<object[]> EndComboEvent
        {
            add => eventMap["EndComboEvent"] = value;
            remove => eventMap["EndComboEvent"] = value;
        }
        
        /// <summary>
        /// 血量改变
        /// </summary>
        public event Action<object[]> OnHPChangeEvent
        {
            add => eventMap["OnHPChangeEvent"] = value;
            remove => eventMap["OnHPChangeEvent"] = value;
        }
        
        /// <summary>
        /// 体力改变
        /// </summary>
        public event Action<object[]> OnNRGChangeEvent
        {
            add => eventMap["OnNRGChangeEvent"] = value;
            remove => eventMap["OnNRGChangeEvent"] = value;
        }
            
        /// <summary>
        /// 开始自动恢复体力
        /// </summary>
        public event Action<object[]> BeginNRGRecoverEvent
        {
            add => eventMap["BeginNRGRecoverEvent"] = value;
            remove => eventMap["BeginNRGRecoverEvent"] = value;
        }
        
        /// <summary>
        /// 结束自动恢复体力
        /// </summary>
        public event Action<object[]> EndNRGRecoverEvent
        {
            add => eventMap["EndNRGRecoverEvent"] = value;
            remove => eventMap["EndNRGRecoverEvent"] = value;
        }

        public UnityEvent<object[]> TestEvent;
        
        /// <summary>
        /// 事件字典
        /// </summary>
        private Dictionary<string, Action<object[]>> eventMap = new Dictionary<string, Action<object[]>>();
        
        /// <summary>
        /// 第三人称事件管理器的主人
        /// </summary>
        public ThirdPerson Owner;

        /// <summary>
        /// 第三人称事件管理器的构造函数
        /// </summary>
        /// <param name="owner">第三人称事件管理器的主人</param>
        public ThirdPersonEventManager(ThirdPerson owner)
        {
            Owner = owner;
            
            // 事件字典的键初始化
            
            eventMap.Add("BeginSprintEvent", null);
            eventMap.Add("AfterBeginSprintEvent", null);
            eventMap.Add("EndSprintEvent", null);
            eventMap.Add("BeginMeleeAttackEvent", null);
            eventMap.Add("EndMeleeAttackEvent",null);
            eventMap.Add("TryDoDamageEvent", null);
            eventMap.Add("EndComboEvent", null);
            eventMap.Add("OnHPChangeEvent", null);
            eventMap.Add("OnNRGChangeEvent", null);
            eventMap.Add("BeginNRGRecoverEvent", null);
            eventMap.Add("EndNRGRecoverEvent", null);
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="args">事件参数</param>
        public void Fire(string eventName, object[] args)
        {
            eventMap[eventName]?.Invoke(args);
        }
    }
}