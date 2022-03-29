// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 25/03/2022 23:24
// 最后一次修改于: 29/03/2022 10:02
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 第三人称动画状态机控制器
    /// </summary>
    public class ThirdPersonAnimationController : MonoBehaviour
    {
        // 组件

        public Animator Anim;
        
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
        
        // 参数
        
        /// <summary>
        /// 是否落地
        /// </summary>
        public ScriptableBoolean IsGrounded;

        /// <summary>
        /// 玩家的水平速度
        /// </summary>
        public ScriptableVector3 HorizontalVelocity;
        
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
            Anim.SetBool(animIDGrounded, IsGrounded.Value);
            
            // 跳跃
            if (IsGrounded.Value)
                Anim.SetBool(animIDFreeFall, false);
            // 自由下落
            else
                Anim.SetBool(animIDFreeFall, true);
            
            // 移动
            moveAnimBlend = Mathf.SmoothDamp(moveAnimBlend, HorizontalVelocity.Value.magnitude, ref moveAnimBlendSmoothVelocity, moveAnimBlendSmoothTime);
            Anim.SetFloat(animIDSpeed, moveAnimBlend);
        }

        private void OnBeginMeleeAttack()
        {
            Anim.SetTrigger(animIDMeleeAttack);
        }
    }
}