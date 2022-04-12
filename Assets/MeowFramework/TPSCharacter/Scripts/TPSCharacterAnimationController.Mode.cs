// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/04/2022 15:55
// 最后一次修改于: 12/04/2022 15:56
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
    public partial class TPSCharacterAnimationController : SerializedMonoBehaviour
    {
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