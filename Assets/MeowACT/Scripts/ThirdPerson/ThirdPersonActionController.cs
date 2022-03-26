// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 26/03/2022 2:15
// 最后一次修改于: 26/03/2022 22:21
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 第三人称行为控制器
    /// </summary>
    public class ThirdPersonActionController
    {
        /// <summary>
        /// 第三人称行为控制器的主人
        /// </summary>
        public ThirdPerson Owner;
        
        /// <summary>
        /// 冲刺的冷却时间
        /// </summary>
        private float sprintTimeout = 0.5f;

        /// <summary>
        /// 攻击的冷却时间
        /// </summary>
        private float attackTimeout = 1f;
        
        /// <summary>
        /// 第三人称行为控制器的构造函数
        /// </summary>
        /// <param name="owner">第三人称行为控制器的主人</param>
        public ThirdPersonActionController(ThirdPerson owner)
        {
            Owner = owner;
        }
        
        public void TryAction()
        {
            if(TrySprint())
                return;
            TryMeleeAttack();
        }
        
        private bool TrySprint()
        {
            if (Owner.Input.Sprint && !Owner.AttributeManager.IsSprinting && !Owner.AttributeManager.IsFreezingMove)
            {
                FireBeginSprintEvent();
                
                var cotimer = TimerManagerSingleton.SingleInstance.CreateCoTimer(0,
                    delegate(object[] args) { FireAfterBeginSprintEvent(); });
                cotimer.Start();
                
                var timer = TimerManagerSingleton.SingleInstance.CreateTimer(sprintTimeout, false,
                    delegate(object[] args) { FireEndSprintEvent(); });
                timer.Start();

                return true;
            }

            return false;
        }

        private bool TryMeleeAttack()
        {   
            if (Owner.Input.Attack && !Owner.AttributeManager.IsMeleeAttacking)
            {
                FireBeginMeleeAttackEvent();
                
                var timer = TimerManagerSingleton.SingleInstance.CreateTimer(attackTimeout, false,
                    delegate(object[] args) { FireEndMeleeAttackEvent(); });
                timer.Start();
                
                return true;
            }

            return false;
        }

        private void FireBeginSprintEvent()
        {
            Owner.AttributeManager.IsSprinting = true;
            Owner.AttributeManager.IsSprintBegin = true;

            // 开始冲刺进入霸体
            Owner.AttributeManager.IsSuperArmor = true;
            
            // 开始冲刺消耗体力
            Owner.AttributeManager.NRG -= Owner.AttributeManager.SprintCost;

            Owner.EventManager.Fire("BeginSprintEvent", null);
        }

        private void FireAfterBeginSprintEvent()
        {
            Owner.AttributeManager.IsSprintBegin = false;
            
            Owner.EventManager.Fire("AfterBeginSprintEvent", null);
        }
        
        private void FireEndSprintEvent()
        {
            Owner.AttributeManager.IsSprinting = false;
            
            // 结束冲刺离开霸体
            Owner.AttributeManager.IsSuperArmor = true;
            
            Owner.EventManager.Fire("EndSprintEvent", null);
        }
        
        private void FireBeginMeleeAttackEvent()
        {
            Owner.AttributeManager.IsMeleeAttacking = true;
            Owner.AttributeManager.IsFreezingMove = true;

            // 开始近战攻击消耗体力
            Owner.AttributeManager.NRG -= Owner.AttributeManager.MeleeAttackCost;
            
            Owner.EventManager.Fire("BeginMeleeAttackEvent", null);
        }
        
        private void FireEndMeleeAttackEvent()
        {
            Owner.AttributeManager.IsMeleeAttacking = false;
            Owner.AttributeManager.IsFreezingMove = false;
            
            Owner.EventManager.Fire("EndMeleeAttackEvent", null);
        }
    }
}