using System.Collections;
using Unity.Mathematics;
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

        [SerializeField] private Image _fillImage = null;

        private Canvas m_ParentCanvas = null;
        private RectTransform m_CachedTransform = null;
        private CanvasGroup m_CachedCanvasGroup = null;
        private UEntity m_Owner = null;
        private int m_OwnerId = 0;

        public UEntity Owner => m_Owner;

        private void Awake()
        {
            m_CachedTransform = GetComponent<RectTransform>();
            if (m_CachedTransform == null)
            {
                Log.Error("RectTransform is invalid.");
                return;
            }

            m_CachedCanvasGroup = GetComponent<CanvasGroup>();
            if (m_CachedCanvasGroup == null)
            {
                Log.Error("CanvasGroup is invalid.");
                return;
            }
        }

        public void AddFillAmount(float add)
        {
            Process(_fillImage.fillAmount, Mathf.Clamp(_fillImage.fillAmount + add, 0, 1));
        }
        /// <summary>
        /// 体力条物体的初始化
        /// </summary>
        /// <param name="owner">体力条的实体主人</param>
        /// <param name="fromNRGRatio">体力条初始值</param>
        /// <param name="toNRGRatio">体力条终点值</param>
        public void Process(float fromNRGRatio, float toNRGRatio)
        {
            gameObject.SetActive(true);
            // 停掉当前 MonoBehavior 的所有协程
            // 这就停掉了之前的体力条的变化
            StopAllCoroutines();

            m_CachedCanvasGroup.alpha = 1f;
            _fillImage.fillAmount = fromNRGRatio;

            Refresh();

            // 开始体力条变化协程
            StartCoroutine(NRGBarCo(toNRGRatio, AnimationDuration, AnimationSmoothTime, KeepSeconds, FadeOutDuration,
                FadeOutSmoothTime));
        }

        public bool Refresh()
        {
            if (m_CachedCanvasGroup.alpha <= 0f)
            {
                return false;
            }

            return true;
        }

        public void Reset()
        {
            StopAllCoroutines();
            m_CachedCanvasGroup.alpha = 1f;
            _fillImage.fillAmount = 1f;
            m_Owner = null;
            gameObject.SetActive(false);
        }

        private IEnumerator NRGBarCo(float value, float animationDuration, float animationSmoothTime, float keepDuration,
            float fadeOutDuration, float fadeOutSmoothTime)
        {
            yield return _fillImage.SmoothFillAmount(value, animationDuration, animationSmoothTime);
            yield return new WaitForSeconds(keepDuration);
            yield return m_CachedCanvasGroup.FadeToAlpha(0f, fadeOutDuration, fadeOutSmoothTime);
        }
    }
}
