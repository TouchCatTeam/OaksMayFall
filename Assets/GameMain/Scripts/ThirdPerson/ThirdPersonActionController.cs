// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 26/03/2022 2:15
// 最后一次修改于: 26/03/2022 8:03
// 版权所有: ThinkDifferentStudio
// 描述:
// ----------------------------------------------

namespace OaksMayFall
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
            if (Owner.Input.Sprint && !Owner.IsSprinting)
            {
                FireBeginSprintEvent();
                
                var cotimer = GameEntry.Timer.CreateCoTimer(0,
                    delegate(object[] args) { FireAfterBeginSprintEvent(); });
                cotimer.Start();
                
                var timer = GameEntry.Timer.CreateTimer(sprintTimeout, false,
                    delegate(object[] args) { FireEndSprintEvent(); });
                timer.Start();

                return true;
            }

            return false;
        }

        private bool TryMeleeAttack()
        {   
            if (Owner.Input.Attack && !Owner.IsMeleeAttacking)
            {
                FireBeginMeleeAttackEvent();
                
                var timer = GameEntry.Timer.CreateTimer(attackTimeout, false,
                    delegate(object[] args) { FireEndMeleeAttackEvent(); });
                timer.Start();
                
                return true;
            }

            return false;
        }

        private void FireBeginSprintEvent()
        {
            Owner.IsSprinting = true;
            Owner.IsSprintBegin = true;
            
            Owner.FireBeginSprintEvent(null);
        }

        private void FireAfterBeginSprintEvent()
        {
            Owner.IsSprintBegin = false;
            
            Owner.FireAfterBeginSprintEvent(null);
        }
        
        private void FireEndSprintEvent()
        {
            Owner.IsSprinting = false;
            
            Owner.FireEndSprintEvent(null);
        }
        
        private void FireBeginMeleeAttackEvent()
        {
            Owner.IsMeleeAttacking = true;
            Owner.IsFreezingMove = true;
            
            Owner.FireBeginMeleeAttackEvent(null);
        }
        
        private void FireEndMeleeAttackEvent()
        {
            Owner.IsMeleeAttacking = false;
            Owner.IsFreezingMove = false;
            
            Owner.FireEndMeleeAttackEvent(null);
        }
    }
}