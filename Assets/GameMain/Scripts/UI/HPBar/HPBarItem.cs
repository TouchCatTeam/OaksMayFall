//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace OaksMayFall
{
    public class HPBarItem : MonoBehaviour
    {
        private const float AnimationDuration = 1f;
        private const float AnimationSmoothTime = 0.2f;
        private const float KeepSeconds = 0.4f;
        private const float FadeOutDuration = 1f;
        private const float FadeOutSmoothTime = 0.2f;

        [SerializeField]
        private Slider m_HPBar = null;

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
        /// <summary>
        /// 血条物体的进度处理
        /// </summary>
        /// <param name="owner">血条的实体主人</param>
        /// <param name="fromHPRatio">血条初始值</param>
        /// <param name="toHPRatio">血条终点值</param>
        public void Process(UEntity owner, float fromHPRatio, float toHPRatio)
        {
            if (owner == null)
            {
                Log.Error("Owner is invalid.");
                return;
            }

            gameObject.SetActive(true);
            // 停掉当前 MonoBehavior 的所有协程
            // 这就停掉了之前的血条的变化
            StopAllCoroutines();

            m_CachedCanvasGroup.alpha = 1f;
            if (m_Owner != owner || m_OwnerId != owner.Id)
            {
                m_HPBar.value = fromHPRatio;
                m_Owner = owner;
                m_OwnerId = owner.Id;
            }

            Refresh();

            // 开始血条变化协程
            StartCoroutine(HPBarCo(toHPRatio, AnimationDuration, AnimationSmoothTime, KeepSeconds, FadeOutDuration,
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
            m_HPBar.value = 1f;
            m_Owner = null;
            gameObject.SetActive(false);
        }

        private IEnumerator HPBarCo(float value, float animationDuration, float animationSmoothTime, float keepDuration,
            float fadeOutDuration, float fadeOutSmoothTime)
        {
            yield return m_HPBar.SmoothValue(value, animationDuration, animationSmoothTime);
            yield return new WaitForSeconds(keepDuration);
            yield return m_CachedCanvasGroup.FadeToAlpha(0f, fadeOutDuration, fadeOutSmoothTime);
        }
    }
}
