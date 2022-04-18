// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/04/2022 15:48
// 最后一次修改于: 18/04/2022 15:34
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections;
using System.ComponentModel;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.TPSCharacter
{
	/// <summary>
	/// 第三人称运动控制器
	/// </summary>
    public partial class TPSCharacterLocomotionController
    {
	    // 模式
	    
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
	    /// 没有武器时角色移动速度
	    /// </summary>
	    [BoxGroup("Mode")]
	    [ShowInInspector]
	    [Description("没有武器时角色移动速度")]
	    private float noWeaponWalkSpeed = 4f;

	    /// <summary>
	    /// 持步枪时角色移动速度
	    /// </summary>
	    [BoxGroup("Mode")]
	    [ShowInInspector]
	    [Description("持步枪时角色移动速度")]
	    private float rifleWalkSpeed = 2f;
	    
	    /// <summary>
	    /// 没有武器时摄像机的 FOV
	    /// </summary>
	    [BoxGroup("Mode")]
	    [ShowInInspector]
	    [Description("摄像机的目标 FOV")]
	    private float noWeaponFOV = 40f;
	    
	    /// <summary>
	    /// 持步枪时摄像机的 FOV
	    /// </summary>
	    [BoxGroup("Mode")]
	    [ShowInInspector]
	    [Description("摄像机的目标 FOV")]
	    private float rifleFOV = 30f;
	    
	    /// <summary>
	    /// 摄像机的 FOV 的平滑时间
	    /// </summary>
	    [BoxGroup("Mode")]
	    [ShowInInspector]
	    [Description("摄像机的目标 FOV 的平滑时间")]
	    private float fovSmoothTime = 0.2f;
        
	    /// <summary>
	    /// 没有武器时摄像机的侧向位置
	    /// </summary>
	    [BoxGroup("Mode")]
	    [ShowInInspector]
	    [Description("摄像机的目标侧向位置")]
	    private float noWeaponSide = 0.5f;

	    /// <summary>
	    /// 持步枪时摄像机的侧向位置
	    /// </summary>
	    [BoxGroup("Mode")]
	    [ShowInInspector]
	    [Description("摄像机的目标侧向位置")]
	    private float rifleSide = 1f;
	    
	    /// <summary>
	    /// 摄像机侧向位置的平滑时间
	    /// </summary>
	    [BoxGroup("Mode")]
	    [ShowInInspector]
	    [Description("摄像机侧向位置的平滑时间")]
	    private float cameraSideSmoothTime = 0.2f;

	    // 缓存

	    // 缓存 - 运动模式

	    /// <summary>
	    /// 模式改变协程
	    /// </summary>
	    private Coroutine modeChangeCoroutine;
	    
	    /// <summary>
	    /// 摄像机的目标 FOV 平滑速度
	    /// </summary>
	    private float fovSmoothVelocity;
	    
	    /// <summary>
	    /// 摄像机侧向位置的平滑速度
	    /// </summary>
	    private float cameraSideSmoothVelocity;
	    
        /// <summary>
        /// 开始模式改变的协程函数
        /// </summary>
        /// <param name="targetFOV">目标 FOV</param>
        /// <param name="targetSide">目标侧向位置</param>
        /// <returns></returns>
        private IEnumerator StartModeChange(float targetFOV, float targetSide)
        {
	        // 摄像机第三人称跟随组件
	        var camera3rdPersonFollow =
		        PlayerFollowCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

	        // 初始化计时器
	        var timeLeft = modeTransitionTime;
            
	        // 在给定时间内平滑
	        // 平滑时间结束时，被平滑项接近终点值但不是终点值
	        // 因此最后需要给被平滑项赋终点值，这可能产生一个抖动
	        // 因此平滑时间需要在保证效果的同时尽可能小，才能让最后的抖动变小
	        while (timeLeft > 0)
	        {
		        timeLeft -= Time.deltaTime;
		        PlayerFollowCamera.m_Lens.FieldOfView = Mathf.SmoothDamp(PlayerFollowCamera.m_Lens.FieldOfView,
			        targetFOV, ref fovSmoothVelocity, fovSmoothTime);
		        camera3rdPersonFollow.CameraSide = Mathf.SmoothDamp(camera3rdPersonFollow.CameraSide, targetSide,
			        ref cameraSideSmoothVelocity, cameraSideSmoothTime);
		        
		        yield return null;
	        }
	        
	        // 摄像机焦距设置赋终点值
	        PlayerFollowCamera.m_Lens.FieldOfView = targetFOV;
	        // 摄像机侧向位置赋终点值
	        camera3rdPersonFollow.CameraSide = targetSide;
            
	        yield return null;
	        
        }
        
        /// <summary>
        /// 改变运动模式
        /// </summary>
        /// <param name="mode">模式</param>
        public void SetLocomotionMode(TPSCharacterBehaviourMode mode)
        {
	        this.mode = mode;
	        if(modeChangeCoroutine != null)
				StopCoroutine(modeChangeCoroutine);
	        switch (mode)
	        {
		        case TPSCharacterBehaviourMode.NoWeapon:
			        modeChangeCoroutine = StartCoroutine(StartModeChange(noWeaponFOV, noWeaponSide));
			        break;
		        case TPSCharacterBehaviourMode.Rifle:
			        modeChangeCoroutine = StartCoroutine(StartModeChange(rifleFOV, rifleSide));
			        break;
	        }
        }
    }
}

