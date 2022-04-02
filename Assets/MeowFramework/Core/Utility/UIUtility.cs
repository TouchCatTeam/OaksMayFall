// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 26/03/2022 16:27
// 最后一次修改于: 02/04/2022 23:05
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MeowFramework.Core
{
    public static class UIUtility
    {
        public static IEnumerator FadeToAlpha(this CanvasGroup canvasGroup, float alpha, float duration, float smoothTime)
        {
            float time = 0f;
            float smoothVelocity = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.SmoothDamp(canvasGroup.alpha, alpha, ref smoothVelocity, smoothTime);
                yield return new WaitForEndOfFrame();
            }

            canvasGroup.alpha = alpha;
        }

        public static IEnumerator SmoothValue(this Slider slider, float value, float duration, float smoothTime)
        {
            float time = 0f;
            float smoothVelocity = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                slider.value = Mathf.SmoothDamp(slider.value, value, ref smoothVelocity, smoothTime);
                
                yield return new WaitForEndOfFrame();
            }

            slider.value = value;
        }
        
        public static IEnumerator SmoothFillAmount(this Image image, float value, float duration, float smoothTime)
        {
            float time = 0f;
            float smoothVelocity = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                image.fillAmount = Mathf.SmoothDamp(image.fillAmount, value, ref smoothVelocity, smoothTime);
                
                yield return new WaitForEndOfFrame();
            }

            image.fillAmount = value;
        }
    }
}