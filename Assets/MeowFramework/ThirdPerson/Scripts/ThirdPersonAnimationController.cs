// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 25/03/2022 23:24
// 最后一次修改于: 05/04/2022 1:02
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework
{
    /// <summary>
    /// 第三人称动画状态机控制器
    /// </summary>
    public class ThirdPersonAnimationController : SerializedMonoBehaviour
    {
        // 组件

        /// <summary>
        /// 第三人称运动管理器
        /// </summary>
        [BoxGroup("Component")]
        [Tooltip("第三人称运动管理器")]
        public ThirdPersonLocomotionController LocomotionController;
        
        /// <summary>
        /// 动画控制器
        /// </summary>
        [BoxGroup("Component")]
        [Tooltip("动画控制器")]
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
        [BoxGroup("Move")]
        [Tooltip("跑步动画混合值的过渡时间")]
        public float moveAnimBlendSmoothTime = 0.2f;

        public void Start()
        {
            AssignAnimationIDs();
        }

        public void Update()
        {
            SetAnimatorValue();
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
            Anim.SetBool(animIDGrounded, LocomotionController.IsGrounded);
            
            // 跳跃
            if (LocomotionController.IsGrounded)
                Anim.SetBool(animIDFreeFall, false);
            // 自由下落
            else
                Anim.SetBool(animIDFreeFall, true);
            
            // 移动
            moveAnimBlend = Mathf.SmoothDamp(moveAnimBlend, LocomotionController.HorizontalVelocity.Value.magnitude, ref moveAnimBlendSmoothVelocity, moveAnimBlendSmoothTime);
            Anim.SetFloat(animIDSpeed, moveAnimBlend);
        }

        private void OnBeginMeleeAttack()
        {
            Anim.SetTrigger(animIDMeleeAttack);
        }
    }
}