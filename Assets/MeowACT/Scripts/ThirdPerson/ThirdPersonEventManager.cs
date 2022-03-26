// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 26/03/2022 14:46
// 最后一次修改于: 26/03/2022 21:37
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 第三人称事件管理器
    /// </summary>
    public class ThirdPersonEventManager
    {
        // 事件相关
        
        /// <summary>
        /// 事件委托类型
        /// </summary>
        public delegate void ThirdPersonEvent(object[] args);
        
        /// <summary>
        /// 开始冲刺
        /// </summary>
        public event ThirdPersonEvent BeginSprintEvent
        {
            add => EventMap["BeginSprintEvent"] = value;
            remove => EventMap["BeginSprintEvent"] = value;
        }
        
        /// <summary>
        /// 开始冲刺之后一帧
        /// </summary>
        public event ThirdPersonEvent AfterBeginSprintEvent
        {
            add => EventMap["AfterBeginSprintEvent"] = value;
            remove => EventMap["AfterBeginSprintEvent"] = value;
        }
        
        /// <summary>
        /// 结束冲刺
        /// </summary>
        public event ThirdPersonEvent EndSprintEvent
        {
            add => EventMap["EndSprintEvent"] = value;
            remove => EventMap["EndSprintEvent"] = value;
        }
        
        /// <summary>
        /// 开始近战攻击
        /// </summary>
        public event ThirdPersonEvent BeginMeleeAttackEvent
        {
            add => EventMap["BeginMeleeAttackEvent"] = value;
            remove => EventMap["BeginMeleeAttackEvent"] = value;
        }
        
        /// <summary>
        /// 结束近战攻击
        /// </summary>
        public event ThirdPersonEvent EndMeleeAttackEvent
        {
            add => EventMap["EndMeleeAttackEvent"] = value;
            remove => EventMap["EndMeleeAttackEvent"] = value;
        }
        
        /// <summary>
        /// 尝试造成伤害
        /// </summary>
        public event ThirdPersonEvent TryDoDamageEvent
        {
            add => EventMap["TryDoDamageEvent"] = value;
            remove => EventMap["TryDoDamageEvent"] = value;
        }
        
        /// <summary>
        /// 结束连击
        /// </summary>
        public event ThirdPersonEvent EndComboEvent
        {
            add => EventMap["EndComboEvent"] = value;
            remove => EventMap["EndComboEvent"] = value;
        }
        
        /// <summary>
        /// 血量改变
        /// </summary>
        public event ThirdPersonEvent OnHPChangeEvent
        {
            add => EventMap["OnHPChangeEvent"] = value;
            remove => EventMap["OnHPChangeEvent"] = value;
        }
        
        /// <summary>
        /// 体力改变
        /// </summary>
        public event ThirdPersonEvent OnNRGChangeEvent
        {
            add => EventMap["OnNRGChangeEvent"] = value;
            remove => EventMap["OnNRGChangeEvent"] = value;
        }
            
        /// <summary>
        /// 开始自动恢复体力
        /// </summary>
        public event ThirdPersonEvent BeginNRGRecoverEvent
        {
            add => EventMap["BeginNRGRecoverEvent"] = value;
            remove => EventMap["BeginNRGRecoverEvent"] = value;
        }
        
        /// <summary>
        /// 结束自动恢复体力
        /// </summary>
        public event ThirdPersonEvent EndNRGRecoverEvent
        {
            add => EventMap["EndNRGRecoverEvent"] = value;
            remove => EventMap["EndNRGRecoverEvent"] = value;
        }    

        /// <summary>
        /// 事件字典
        /// </summary>
        public Dictionary<string, ThirdPersonEvent> EventMap = new Dictionary<string, ThirdPersonEvent>();
        
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
            
            EventMap.Add("BeginSprintEvent", null);
            EventMap.Add("AfterBeginSprintEvent", null);
            EventMap.Add("EndSprintEvent", null);
            EventMap.Add("BeginMeleeAttackEvent", null);
            EventMap.Add("EndMeleeAttackEvent",null);
            EventMap.Add("TryDoDamageEvent", null);
            EventMap.Add("EndComboEvent", null);
            EventMap.Add("OnHPChangeEvent", null);
            EventMap.Add("OnNRGChangeEvent", null);
            EventMap.Add("BeginNRGRecoverEvent", null);
            EventMap.Add("EndNRGRecoverEvent", null);
        }

        public void Fire(string eventName, object[] args)
        {
            Debug.Log(EventMap[eventName] == null);
            EventMap[eventName]?.Invoke(args);
        }
    }
}