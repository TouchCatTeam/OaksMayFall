// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/03/2022 16:08
// 最后一次修改于: 26/03/2022 14:13
// 版权所有: ThinkDifferentStudio
// 描述:
// ----------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace OaksMayFall
{
    public class HPBarItem : MonoBehaviour
    {
        // 动画参数
        
        private const float AnimationDuration = 1f;
        private const float AnimationSmoothTime = 0.2f;
        private const float KeepSeconds = 0.4f;
        private const float FadeOutDuration = 1f;
        private const float FadeOutSmoothTime = 0.2f;

        /// <summary>
        /// 血条 UI
        /// </summary>
        [SerializeField] private Slider hpBar = null;

        /// <summary>
        /// 血条的画布组
        /// </summary>
        private CanvasGroup canvasGroup = null;
        
        /// <summary>
        /// 血条的主人
        /// </summary>
        public GameObject Owner;

        private void Awake()
        {
            if (hpBar == null)
                Debug.LogError("没有给 HPBarItem 组件配置血条 UI 引用");
            
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                Debug.LogError("血条 Perfab 没有画布组");
        }
        
        /// <summary>
        /// 血条物体的值变化，同时显示
        /// </summary>
        /// <param name="owner">血条的主人</param>
        /// <param name="add">血条变化值</param>
        public void SmoothAddAndShow(GameObject owner, float add)
        {
            SmoothValueAndShow(owner, Mathf.Clamp(hpBar.value + add, 0, 1));
        }
        
        /// <summary>
        /// 血条物体的值变化，同时显示
        /// </summary>
        /// <param name="owner">血条的主人</param>
        /// <param name="toHPRatio">血条终点值</param>
        public void SmoothValueAndShow(GameObject owner, float toHPRatio)
        {
            SmoothValueAndShow(owner, hpBar.value, toHPRatio);
        }
        
        /// <summary>
        /// 血条物体的值变化，同时显示
        /// </summary>
        /// <param name="owner">血条的主人</param>
        /// <param name="fromHPRatio">血条初始值</param>
        /// <param name="toHPRatio">血条终点值</param>
        public void SmoothValueAndShow(GameObject owner, float fromHPRatio, float toHPRatio)
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

            hpBar.value = fromHPRatio;
            canvasGroup.alpha = 1f;
            Owner = owner;

            Refresh();

            // 开始血条变化协程
            StartCoroutine(HPBarCo(toHPRatio, AnimationDuration, AnimationSmoothTime, KeepSeconds, FadeOutDuration,
                FadeOutSmoothTime));
        }

        /// <summary>
        /// 更新 UI 的位置
        /// </summary>
        /// <returns></returns>
        public bool Refresh()
        {
            // 血条挂在 entity 的 HPBarRoot 下
            var hpBarRoot = Owner.transform.Find("HPBarRoot");
            if(hpBarRoot == null)
                Debug.LogError("要显示血条的主人的身上没有 HPBarRoot 用于挂载血条实例");
            transform.SetParent(hpBarRoot);
            
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            transform.localScale = Vector3.one;
            
            if (canvasGroup.alpha <= 0f)
            {
                GameEntry.HPBar.HideHPBar(this);
            }
            
            return true;
        }

        public void Reset()
        {
            StopAllCoroutines();
            canvasGroup.alpha = 1f;
            hpBar.value = 1f;
            Owner = null;
            gameObject.SetActive(false);
        }

        private IEnumerator HPBarCo(float value, float animationDuration, float animationSmoothTime, float keepDuration,
            float fadeOutDuration, float fadeOutSmoothTime)
        {
            yield return hpBar.SmoothValue(value, animationDuration, animationSmoothTime);
            yield return new WaitForSeconds(keepDuration);
            yield return canvasGroup.FadeToAlpha(0f, fadeOutDuration, fadeOutSmoothTime);
        }
    }
}
