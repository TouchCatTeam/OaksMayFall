// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 07/04/2022 10:48
// 最后一次修改于: 07/04/2022 11:04
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections;
using MeowFramework;
using MeowFramework.MeowACT;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace FlowCanvas.Nodes
{
    [Category("MeowFramework/Ability")]
    [Description("覆盖水平速度直至某个条件达成")]
    public class OverrideHorizontalVelocityUntil : LatentActionNode<float>
    {
        /// <summary>
        /// 输入控制器
        /// </summary>
        [Description("输入控制器")]
        public BBParameter<MeowACTInputController> InputController;

        /// <summary>
        /// 移动控制器
        /// </summary>
        [Description("移动控制器")]
        public BBParameter<ThirdPersonLocomotionController> LocomotionController;

        /// <summary>
        /// 结束条件
        /// </summary>
        private ValueInput<bool> condition;
        
        /// <summary>
        /// 不允许协程队列
        /// </summary>
        public override bool allowRoutineQueueing
        {
            get { return false; }
        }

        /// <summary>
        /// 速度覆盖的协程函数
        /// </summary>
        /// <param name="velocityOverride">速度覆盖值</param>
        /// <returns></returns>
        public override IEnumerator Invoke(float velocityOverride = 0f)
        {
            // 空值检查
            if (InputController.value == null)
                yield break;
            if (LocomotionController.value == null)
                yield break;
            
            InputController.value.EnableMoveInput(false);
            LocomotionController.value.SetHorizontalVelocityOverride(velocityOverride);
            
            yield return new UnityEngine.WaitUntil(condition.GetValue);
            
            LocomotionController.value.CancelHorizontalVelocityOverride();
            InputController.value.EnableMoveInput(true);
            
            // 最后要返回 null，不然协程函数末尾的语句的执行时间会有问题
            yield return null;
        }
        
        //since we want to check the condition per frame, this is implementing this way instead of a parameter
        protected override void OnRegisterExtraPorts(FlowNode node)
        {
            condition = node.AddValueInput<bool>("Condition");
        }
    }
}