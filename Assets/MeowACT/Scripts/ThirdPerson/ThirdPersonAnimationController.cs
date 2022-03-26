// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 25/03/2022 23:24
// 最后一次修改于: 26/03/2022 21:16
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 第三人称动画状态机控制器
    /// </summary>
    public class ThirdPersonAnimationController
    {
        /// <summary>
        /// 第三人称动画状态机控制器的主人
        /// </summary>
        public ThirdPerson Owner;

        // id
	    
        /// <summary>
        /// 动画状态机的速度参数的 id
        /// </summary>
        private int animIDSpeed;
        /// <summary>
        /// 动画状态机的落地参数的 id
        /// </summary>
        private int animIDGrounded;
        /// <summary>
        /// 动画状态机的自由落体参数的 id
        /// </summary>
        private int animIDFreeFall;
        /// <summary>
        /// 动画状态机的近战攻击参数的 id
        /// </summary>
        private int animIDMeleeAttack;
        
        // 混合值
        
        /// <summary>
        /// 跑步动画混合值
        /// </summary>
        private float moveAnimBlend;
        /// <summary>
        /// 跑步动画混合值的过渡速度
        /// </summary>
        private float moveAnimBlendSmoothVelocity;
        /// <summary>
        /// 跑步动画混合值的过渡时间
        /// </summary>
        private float moveAnimBlendSmoothTime = 0.2f;
        
        /// <summary>
        /// 第三人称动画状态机控制器的构造函数
        /// </summary>
        /// <param name="owner">第三人称动画状态机控制器的主人</param>
        public ThirdPersonAnimationController(ThirdPerson owner)
        {
            Owner = owner;
            
            // 事件绑定

            Owner.EventManager.BeginMeleeAttackEvent += OnBeginMeleeAttackEvent;
        }

        /// <summary>
        /// 第三人称动画状态机控制器的析构函数
        /// </summary>
        ~ThirdPersonAnimationController()
        {
            // 事件解绑

            Owner.EventManager.BeginMeleeAttackEvent -= OnBeginMeleeAttackEvent;
        }
        
        /// <summary>
        /// 初始化动画状态机参数
        /// </summary>
        public void AssignAnimationIDs()
        {
            animIDSpeed = Animator.StringToHash("ForwardSpeed");
            animIDGrounded = Animator.StringToHash("Grounded");
            animIDFreeFall = Animator.StringToHash("FreeFall");
            animIDMeleeAttack = Animator.StringToHash("Attack");
        }

        /// <summary>
        /// 设置动画状态机参数
        /// </summary>
        public void SetAnimatorValue()
        {
            // 着地
            Owner.Anim.SetBool(animIDGrounded, Owner.AttributeManager.IsGrounded);
            
            // 跳跃
            if (Owner.AttributeManager.IsGrounded)
                Owner.Anim.SetBool(animIDFreeFall, false);
            // 自由下落
            else
                Owner.Anim.SetBool(animIDFreeFall, true);
            
            // 移动
            moveAnimBlend = Mathf.SmoothDamp(moveAnimBlend, Owner.AttributeManager.HorizontalVelocity.magnitude, ref moveAnimBlendSmoothVelocity, moveAnimBlendSmoothTime);
            Owner.Anim.SetFloat(animIDSpeed, moveAnimBlend);
        }

        private void OnBeginMeleeAttackEvent(object[] args)
        {
            Owner.Anim.SetTrigger(animIDMeleeAttack);
        }
    }
}