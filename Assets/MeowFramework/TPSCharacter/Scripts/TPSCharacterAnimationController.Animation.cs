// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/04/2022 15:54
// 最后一次修改于: 17/04/2022 16:44
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using MeowFramework.Core;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;

namespace MeowFramework.TPSCharacter
{
    /// <summary>
    /// 第三人称动画状态机控制器
    /// </summary>
    public partial class TPSCharacterAnimationController
    {
        /// <summary>
        /// 向右的速度
        /// </summary>
        [BoxGroup("AnimatorParameter")]
        [Sirenix.OdinInspector.ReadOnly]
        [ShowInInspector]
        [Description("向右的速度")]
        private float rightSpeed;

        /// <summary>
        /// 向前的速度
        /// </summary>
        [BoxGroup("AnimatorParameter")]
        [Sirenix.OdinInspector.ReadOnly]
        [ShowInInspector]
        [Description("向前的速度")]
        private float forwardSpeed;

        /// <summary>
        /// 基准速度
        /// </summary>
        [BoxGroup("AnimatorParameter")]
        [Description("基准速度")]
        public float StandardSpeed = 10f;
        
        // 缓存
        
        // 缓存 - 动画机参数计算
        
        /// <summary>
        /// 摄像机的前方
        /// </summary>
        private Vector3 cameraForward;

        /// <summary>
        /// 摄像机的右方
        /// </summary>
        private Vector3 cameraRight;
        
        // 缓存 - id
	    
        /// <summary>
        /// 动画状态机的摄像机前方速度参数的 id
        /// </summary>
        private int animIDForwardSpeed;
        
        /// <summary>
        /// 动画状态机的摄像机右方速度参数的 id
        /// </summary>
        private int animIDRightSpeed;
        
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

        /// <summary>
        /// 初始化动画状态机参数
        /// </summary>
        public void AssignAnimationIDs()
        {
            animIDForwardSpeed = Animator.StringToHash("ForwardSpeed");
            animIDRightSpeed = Animator.StringToHash("RightSpeed");
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

            // 摄像机方向
            cameraForward = Camera.main.transform.forward;
            cameraRight = Camera.main.transform.right;
            cameraForward.y = 0;
            
            // 移动速度
            rightSpeed = Vector3.Dot(LocomotionController.HorizontalVelocity.Value, cameraRight);
            forwardSpeed = Vector3.Dot(LocomotionController.HorizontalVelocity.Value, cameraForward);

            Anim.SetFloat(animIDRightSpeed, rightSpeed/StandardSpeed);
            Anim.SetFloat(animIDForwardSpeed, forwardSpeed/StandardSpeed);
        }

        private void OnBeginMeleeAttack()
        {
            Anim.SetTrigger(animIDMeleeAttack);
        }
    }
}