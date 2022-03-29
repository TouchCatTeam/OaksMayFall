// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 26/03/2022 2:15
// 最后一次修改于: 28/03/2022 15:28
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using System;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 第三人称行为控制器
    /// </summary>
    public class ThirdPersonActionController : MonoBehaviour
    {
        /// <summary>
        /// 输入
        /// </summary>
        [Tooltip("输入")]
        public MeowACTInputController Input;
        
        /// <summary>
        /// 冲刺的冷却时间
        /// </summary>
        [Tooltip("冲刺的冷却时间")]
        private float sprintTimeout = 0.5f;

        /// <summary>
        /// 攻击的冷却时间
        /// </summary>
        [Tooltip("攻击的冷却时间")]
        private float attackTimeout = 1f;

        public void Update()
        {
            TryAction();
        }

        public void TryAction()
        {
            if(TrySprint())
                return;
            TryMeleeAttack();
        }
        
        private bool TrySprint()
        {
            if (Input.Sprint && !AttributeManager.IsSprinting && !AttributeManager.IsFreezingMove)
            {
                FireBeginSprintEvent();
                
                var cotimer = TimerSystemSingleton.SingleInstance.CreateCoTimer(0,
                    delegate(object[] args) { FireAfterBeginSprintEvent(); });
                cotimer.Start();
                
                var timer = TimerSystemSingleton.SingleInstance.CreateTimer(sprintTimeout, false,
                    delegate(object[] args) { FireEndSprintEvent(); });
                timer.Start();

                return true;
            }

            return false;
        }

        private bool TryMeleeAttack()
        {   
            if (Input.Attack && !AttributeManager.IsMeleeAttacking)
            {
                FireBeginMeleeAttackEvent();

                return true;
            }

            return false;
        }

        private void FireBeginSprintEvent()
        {
            AttributeManager.IsSprinting = true;
            AttributeManager.IsSprintBegin = true;

            // 开始冲刺进入霸体
            AttributeManager.IsSuperArmor = true;
            
            // 开始冲刺消耗体力
            AttributeManager.NRG -= AttributeManager.SprintCost;

            EventManager.Fire("BeginSprintEvent", null);
        }

        private void FireAfterBeginSprintEvent()
        {
            AttributeManager.IsSprintBegin = false;
            
            EventManager.Fire("AfterBeginSprintEvent", null);
        }
        
        private void FireEndSprintEvent()
        {
            AttributeManager.IsSprinting = false;
            
            // 结束冲刺离开霸体
            AttributeManager.IsSuperArmor = true;
            
            EventManager.Fire("EndSprintEvent", null);
        }
        
        private void FireBeginMeleeAttackEvent()
        {
            AttributeManager.IsMeleeAttacking = true;
            AttributeManager.IsFreezingMove = true;

            // 开始近战攻击消耗体力
            AttributeManager.NRG -= AttributeManager.MeleeAttackCost;
            
            EventManager.Fire("BeginMeleeAttackEvent", null);
        }
        
        public void FireEndMeleeAttackEvent()
        {
            AttributeManager.IsMeleeAttacking = false;
            AttributeManager.IsFreezingMove = false;
            
            EventManager.Fire("EndMeleeAttackEvent", null);
        }
    }
}