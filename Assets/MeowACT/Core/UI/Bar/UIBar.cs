// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/03/2022 16:08
// 最后一次修改于: 29/03/2022 10:02
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MeowACT
{
    public class UIBar : MonoBehaviour
    {
        /// <summary>
        /// 值变化动画的时长
        /// </summary>
        private const float AnimationDuration = 1f;
        /// <summary>
        /// 值变化动画的过渡时间
        /// </summary>
        private const float AnimationSmoothTime = 0.2f;
        /// <summary>
        /// 值变化动画完成之后的等待时间
        /// </summary>
        private const float KeepSeconds = 0.4f;
        /// <summary>
        /// 值变化动画完成，等待完成之后，淡出动画的时长
        /// </summary>
        private const float FadeOutDuration = 1f;
        /// <summary>
        /// 值变化动画完成，等待完成之后，淡出动画的过渡时间
        /// </summary>
        private const float FadeOutSmoothTime = 0.2f;

        /// <summary>
        /// 填充图像
        /// </summary>
        [SerializeField] private Image fillImage;
        
        /// <summary>
        /// 填充图像的画布组
        /// </summary>
        private CanvasGroup canvasGroup;

        /// <summary>
        /// 基准值
        /// </summary>
        public ScriptableFloat BaseValue;
        
        /// <summary>
        /// 填充图像的填充值
        /// </summary>
        public float FillAmount => fillImage.fillAmount;
        
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                Debug.LogError("UI 缺少画布组");
            }
        }

        /// <summary>
        /// 根据终点值的值变化，同时显示
        /// </summary>
        /// <param name="toValue">终点值</param>
        public void SmoothValueAndShow(ScriptableFloat toValue)
        {
            SmoothValueAndShow(FillAmount, toValue.Value/BaseValue.Value);
        }
        
        /// <summary>
        /// 根据起点值和终点值的值变化，同时显示
        /// </summary>
        /// <param name="fromRatio">初始值</param>
        /// <param name="toRatio">终点值</param>
        public void SmoothValueAndShow(float fromRatio, float toRatio)
        {
            fillImage.gameObject.SetActive(true);
            
            // 停掉当前 MonoBehavior 的所有协程
            // 这就停掉了之前的值变化
            StopAllCoroutines();

            canvasGroup.alpha = 1f;
            fillImage.fillAmount = fromRatio;
            
            // 开始值变化协程
            StartCoroutine(BarCo(toRatio, AnimationDuration, AnimationSmoothTime, KeepSeconds, FadeOutDuration,
                FadeOutSmoothTime));
        }

        public void Reset()
        {
            StopAllCoroutines();
            canvasGroup.alpha = 1f;
            fillImage.fillAmount = 1f;
            fillImage.gameObject.SetActive(false);
        }

        private IEnumerator BarCo(float value, float animationDuration, float animationSmoothTime, float keepDuration,
            float fadeOutDuration, float fadeOutSmoothTime)
        {
            yield return fillImage.SmoothFillAmount(value, animationDuration, animationSmoothTime);
            yield return new WaitForSeconds(keepDuration);
            yield return canvasGroup.FadeToAlpha(0f, fadeOutDuration, fadeOutSmoothTime);
            fillImage.gameObject.SetActive(false);
        }
    }
}
