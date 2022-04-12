// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/04/2022 15:53
// 最后一次修改于: 12/04/2022 15:54
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.ComponentModel;
using MeowFramework.Core.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.TPSCharacter
{
    /// <summary>
    /// 第三人称 UI 控制器
    /// </summary>
    public partial class TPSCharacterUIController
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

        // 缓存
        
        // 缓存 - 模式改变
        
        private Coroutine aimUICoroutine;

        private Coroutine hpUICoroutine;

        private Coroutine nrgUICoroutine;

        /// <summary>
        /// 瞄准 UI 淡入
        /// </summary>
        /// <param name="duration">持续时间</param>
        /// <param name="smoothTime">平滑时间</param>
        private void AimUIFadeIn(float duration = 1f, float smoothTime = 0.2f)
        {
            if (aimUICoroutine != null)
                StopCoroutine(aimUICoroutine);
            aimUICoroutine = StartCoroutine(AimUI.GetComponent<CanvasGroup>().FadeToAlpha(1, duration, smoothTime));
        }
        
        /// <summary>
        /// 瞄准 UI 淡出
        /// </summary>
        /// <param name="duration">持续时间</param>
        /// <param name="smoothTime">平滑时间</param>
        private void AimUIFadeOut(float duration = 1f, float smoothTime = 0.2f)
        {
            if (aimUICoroutine != null)
                StopCoroutine(aimUICoroutine);
            aimUICoroutine = StartCoroutine(AimUI.GetComponent<CanvasGroup>().FadeToAlpha(0, duration, smoothTime));
        }
        
        /// <summary>
        /// 设置 UI 模式
        /// </summary>
        /// <param name="mode">模式</param>
        public void SetUIMode(TPSCharacterBehaviourMode mode)
        {
            this.mode = mode;
            switch (mode)
            {
                case TPSCharacterBehaviourMode.NoWeapon:
                    AimUIFadeOut();
                    break;
                case TPSCharacterBehaviourMode.Rifle:
                    AimUIFadeIn();
                    break;
            }
        }
    }
}