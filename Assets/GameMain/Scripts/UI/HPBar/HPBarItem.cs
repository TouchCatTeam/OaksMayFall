// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/03/2022 16:08
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
    public class HPBarItem : MonoBehaviour
    {
        private const float AnimationDuration = 1f;
        private const float AnimationSmoothTime = 0.2f;
        private const float KeepSeconds = 0.4f;
        private const float FadeOutDuration = 1f;
        private const float FadeOutSmoothTime = 0.2f;

        [SerializeField]
        private Slider HPBar = null;

        private Canvas parentCanvas = null;
        private RectTransform cachedTransform = null;
        private CanvasGroup cachedCanvasGroup = null;
        private int OwnerId = 0;

        public UEntity Owner;

        public float HP => HPBar.value;
        
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

            cachedCanvasGroup.alpha = 1f;
            if (Owner != owner || OwnerId != owner.Id)
            {
                HPBar.value = fromHPRatio;
                Owner = owner;
                OwnerId = owner.Id;
            }

            Refresh();

            // 开始血条变化协程
            StartCoroutine(HPBarCo(toHPRatio, AnimationDuration, AnimationSmoothTime, KeepSeconds, FadeOutDuration,
                FadeOutSmoothTime));
        }

        /// <summary>
        /// 血条物体的进度处理
        /// </summary>
        /// <param name="owner">血条的实体主人</param>
        /// <param name="toHPRatio">血条终点值</param>
        public void Process(UEntity owner, float toHPRatio)
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

            cachedCanvasGroup.alpha = 1f;
            if (Owner != owner || OwnerId != owner.Id)
            {
                Owner = owner;
                OwnerId = owner.Id;
            }

            Refresh();

            // 开始血条变化协程
            StartCoroutine(HPBarCo(toHPRatio, AnimationDuration, AnimationSmoothTime, KeepSeconds, FadeOutDuration,
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
            HPBar.value = 1f;
            Owner = null;
            gameObject.SetActive(false);
        }

        private IEnumerator HPBarCo(float value, float animationDuration, float animationSmoothTime, float keepDuration,
            float fadeOutDuration, float fadeOutSmoothTime)
        {
            yield return HPBar.SmoothValue(value, animationDuration, animationSmoothTime);
            yield return new WaitForSeconds(keepDuration);
            yield return cachedCanvasGroup.FadeToAlpha(0f, fadeOutDuration, fadeOutSmoothTime);
        }
    }
}
