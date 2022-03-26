// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 23/03/2022 8:05
// 最后一次修改于: 26/03/2022 7:13
// 版权所有: ThinkDifferentStudio
// 描述:
// ----------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace OaksMayFall
{
    public class NRGBarItem : MonoBehaviour
    {
        private const float AnimationDuration = 1f;
        private const float AnimationSmoothTime = 0.2f;
        private const float KeepSeconds = 0.4f;
        private const float FadeOutDuration = 1f;
        private const float FadeOutSmoothTime = 0.2f;

        [SerializeField] private Image fillImage;
        
        private RectTransform cachedTransform;
        private CanvasGroup cachedCanvasGroup;

        public float FillAmount => fillImage.fillAmount;
        
        private void Awake()
        {
            cachedTransform = GetComponent<RectTransform>();
            if (cachedTransform == null)
            {
                Log.Error("RectTransform is invalid.");
                return;
            }

            cachedCanvasGroup = GetComponent<CanvasGroup>();
            if (cachedCanvasGroup == null)
            {
                Log.Error("CanvasGroup is invalid.");
            }
        }

        public void AddFillAmount(float add)
        {
            Process(fillImage.fillAmount, Mathf.Clamp(fillImage.fillAmount + add, 0, 1));
        }
        
        /// <summary>
        /// 体力条物体的初始化
        /// </summary>
        /// <param name="fromNRGRatio">体力条初始值</param>
        /// <param name="toNRGRatio">体力条终点值</param>
        public void Process(float fromNRGRatio, float toNRGRatio)
        {
            gameObject.SetActive(true);
            // 停掉当前 MonoBehavior 的所有协程
            // 这就停掉了之前的体力条的变化
            StopAllCoroutines();

            cachedCanvasGroup.alpha = 1f;
            fillImage.fillAmount = fromNRGRatio;

            Refresh();

            // 开始体力条变化协程
            StartCoroutine(NRGBarCo(toNRGRatio, AnimationDuration, AnimationSmoothTime, KeepSeconds, FadeOutDuration,
                FadeOutSmoothTime));
        }

        public bool Refresh()
        {
            if (cachedCanvasGroup.alpha <= 0f)
            {
                return false;
            }

            return true;
        }

        public void Reset()
        {
            StopAllCoroutines();
            cachedCanvasGroup.alpha = 1f;
            fillImage.fillAmount = 1f;
            gameObject.SetActive(false);
        }

        private IEnumerator NRGBarCo(float value, float animationDuration, float animationSmoothTime, float keepDuration,
            float fadeOutDuration, float fadeOutSmoothTime)
        {
            yield return fillImage.SmoothFillAmount(value, animationDuration, animationSmoothTime);
            yield return new WaitForSeconds(keepDuration);
            yield return fillImage.SmoothFillAmount(1, animationDuration, animationSmoothTime);
            yield return cachedCanvasGroup.FadeToAlpha(0f, fadeOutDuration, fadeOutSmoothTime);
        }
    }
}
