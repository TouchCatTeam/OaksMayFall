// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 06/04/2022 1:57
// 最后一次修改于: 06/04/2022 2:20
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections;
using ParadoxNotion.Design;
using UnityEngine;

namespace FlowCanvas.Nodes
{
    [Category("MeowFramework/Time")]
    [Description("协程等待一段适用缩放的时间")]
    public class WaitWithTimeScale : LatentActionNode<float, float>
    {
        public float timeLeft { get; private set; }
        public float normalized { get; private set; }

        public override IEnumerator Invoke(float time = 1f, float timeScale = 1f) {
            timeLeft = time;
            while ( timeLeft > 0 ) {
                timeLeft -= Time.deltaTime * timeScale;
                timeLeft = Mathf.Max(timeLeft, 0);
                normalized = timeLeft / time;
                yield return null;
            }
        }
    }
}