// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 07/04/2022 5:45
// 最后一次修改于: 07/04/2022 6:41
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections;
using MeowFramework;
using MeowFramework.MeowACT;
using ParadoxNotion.Design;
using UnityEngine;

namespace FlowCanvas.Nodes
{
    [Category("MeowFramework/Ability")]
    [Description("在一段时间内覆盖水平速度")]
    public class OverrideHorizontalVelocity : LatentActionNode<MeowACTInputController, ThirdPersonLocomotionController, float, float, float>
    {
        public float timeLeft { get; private set; }
        public float normalized { get; private set; }
        
        public override IEnumerator Invoke(MeowACTInputController inputController, ThirdPersonLocomotionController locomotionController, float velocityOverride = 0f, float time = 1f, float timeScale = 1f)
        {
            // 空值检查
            if (inputController == null)
                yield return null;
            if (locomotionController == null)
                yield return null;
            
            inputController.EnableMoveInput(false);
            locomotionController.SetHorizontalVelocityOverride(velocityOverride);
            
            timeLeft = time;
            while ( timeLeft > 0 ) {
                timeLeft -= Time.deltaTime * timeScale;
                timeLeft = Mathf.Max(timeLeft, 0);
                normalized = timeLeft / time;
                yield return null;
            }
            
            locomotionController.CancelHorizontalVelocityOverride();
            inputController.EnableMoveInput(true);
            
            // 最后要返回 null，不然协程函数末尾的语句的执行时间会有问题
            yield return null;
        }
    }
}