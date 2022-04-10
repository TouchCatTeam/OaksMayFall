// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 25/03/2022 23:24
// 最后一次修改于: 10/04/2022 22:15
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
    public class TPSCharacterAnimationController : SerializedMonoBehaviour
    {
        // 组件

        /// <summary>
        /// 第三人称运动管理器
        /// </summary>
        [BoxGroup("Component")]
        [Required]
        [Tooltip("第三人称运动管理器")]
        public TPSCharacterLocomotionController LocomotionController;
        
        /// <summary>
        /// 动画控制器
        /// </summary>
        [BoxGroup("Component")]
        [Required]
        [Tooltip("动画控制器")]
        public Animator Anim;

        /// <summary>
        /// 行动模式
        /// </summary>
        [BoxGroup("Mode")]
        [ShowInInspector]
        [Sirenix.OdinInspector.ReadOnly]
        [Description("行动模式")]
        private TPSCharacterBehaviourMode mode;

        /// <summary>
        /// 切换模式的过渡时间
        /// </summary>
        [BoxGroup("Mode")]
        [ShowInInspector]
        [Description("切换模式的过渡时间")]
        private float modeTransitionTime = 1f;

        /// <summary>
        /// 层级平滑时间
        /// </summary>
        [BoxGroup("Mode")]
        [ShowInInspector]
        [Description("层级平滑时间")]
        private float layerWeightSmoothTime = 0.2f;
        
        /// <summary>
        /// 向前的速度
        /// </summary>
        [BoxGroup("AnimatorParameter")]
        [Sirenix.OdinInspector.ReadOnly]
        [ShowInInspector]
        [Description("向前的速度")]
        private float forwardSpeed;

        /// <summary>
        /// 向右的速度
        /// </summary>
        [BoxGroup("AnimatorParameter")]
        [Sirenix.OdinInspector.ReadOnly]
        [ShowInInspector]
        [Description("向右的速度")]
        private float rightSpeed;

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
        
        // 缓存 - 模式改变

        /// <summary>
        /// 模式改变协程
        /// </summary>
        private Coroutine modeChangeCoroutine;
        
        /// <summary>
        /// 旧层级权重平滑速度
        /// </summary>
        private float fromLayerWeightSmoothVelocity;
        
        /// <summary>
        /// 新层级权重平滑速度
        /// </summary>
        private float toLayerWeightSmoothVelocity;
        
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

            // 移动
            if (mode == TPSCharacterBehaviourMode.NoWeapon)
            {
	            forwardSpeed = LocomotionController.HorizontalVelocity.Value.magnitude;

	            Anim.SetFloat(animIDForwardSpeed, forwardSpeed);
            }
            else if (mode == TPSCharacterBehaviourMode.Rifle)
            {
	            cameraForward = Camera.main.transform.forward;
	            cameraRight = Camera.main.transform.right;
	            cameraForward.y = 0;
	            forwardSpeed = Vector3.Dot(LocomotionController.HorizontalVelocity.Value, cameraForward);
	            rightSpeed = Vector3.Dot(LocomotionController.HorizontalVelocity.Value, cameraRight);

	            Anim.SetFloat(animIDForwardSpeed, forwardSpeed);
	            Anim.SetFloat(animIDRightSpeed, rightSpeed);
            }
        }

        private void OnBeginMeleeAttack()
        {
            Anim.SetTrigger(animIDMeleeAttack);
        }

        /// <summary>
        /// 开始模式改变的协程函数
        /// </summary>
        /// <param name="targetFOV">目标 FOV</param>
        /// <param name="targetSide">目标侧向位置</param>
        /// <returns></returns>
        private IEnumerator StartModeChange(int fromLayer, int toLayer)
        {
	        // 初始化计时器
	        var timeLeft = modeTransitionTime;

	        // 层级
	        var fromWeight = Anim.GetLayerWeight(fromLayer);
	        var toWeight = Anim.GetLayerWeight(toLayer);
	        
	        // 在给定时间内平滑
	        // 平滑时间结束时，被平滑项接近终点值但不是终点值
	        // 因此最后需要给被平滑项赋终点值，这可能产生一个抖动
	        // 因此平滑时间需要在保证效果的同时尽可能小，才能让最后的抖动变小
	        while (timeLeft > 0)
	        {
		        timeLeft -= Time.deltaTime;
		        fromWeight = Mathf.SmoothDamp(fromWeight, 0,
			        ref fromLayerWeightSmoothVelocity, layerWeightSmoothTime);
		        toWeight = Mathf.SmoothDamp(toWeight, 1,
			        ref toLayerWeightSmoothVelocity, layerWeightSmoothTime);
		        Anim.SetLayerWeight(fromLayer, fromWeight);
		        Anim.SetLayerWeight(toLayer, toWeight);
		        yield return null;
	        }
	        
	        // 赋终点值
	        Anim.SetLayerWeight(fromLayer, 0);
	        Anim.SetLayerWeight(toLayer, 1);
            
	        yield return null;
	        
        }
        
        /// <summary>
        /// 设置动画模式
        /// </summary>
        /// <param name="mode">模式</param>
        public void SetAnimationMode(TPSCharacterBehaviourMode mode)
        {
	        this.mode = mode;
	        if(modeChangeCoroutine != null)
				StopCoroutine(modeChangeCoroutine);
	        switch (mode)
	        {
		        case TPSCharacterBehaviourMode.NoWeapon:
			        modeChangeCoroutine = StartCoroutine(StartModeChange(1, 0));
			        break;
		        case TPSCharacterBehaviourMode.Rifle:
			        modeChangeCoroutine = StartCoroutine(StartModeChange(0, 1));
			        break;
	        }
        }
    }
}