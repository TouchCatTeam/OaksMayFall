// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 10/04/2022 14:12
// 最后一次修改于: 10/04/2022 14:17
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections;
using ParadoxNotion.Design;
using UnityEngine;

namespace FlowCanvas.Nodes
{
    [Category("MeowFramework/Time")]
    [Description("画布组淡入淡出")]
    public class CanvasGroupFade : LatentActionNode<CanvasGroup, float, float, float, float>
    {
        public float timeLeft { get; private set; }
        public float normalized { get; private set; }

        public override IEnumerator Invoke(CanvasGroup canvasGroup, float alpha, float fadeTime = 1f, float smoothTime = 0.2f, float timeScale = 1f) {
            timeLeft = fadeTime;
            float smoothVelocity = 0f;
            
            while ( timeLeft > 0 ) {
                timeLeft -= Time.deltaTime * timeScale;
                timeLeft = Mathf.Max(timeLeft, 0);
                normalized = timeLeft / fadeTime;
                
                canvasGroup.alpha = Mathf.SmoothDamp(canvasGroup.alpha, alpha, ref smoothVelocity, smoothTime);
                
                yield return null;
            }
            
            canvasGroup.alpha = alpha;
            
            yield return null;
            
        }
    }
}