// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 06/04/2022 15:06
// 最后一次修改于: 06/04/2022 15:28
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using UnityEngine;
using MeowFramework.MeowACT;
using ParadoxNotion.Design;

namespace FlowCanvas.Nodes
{
    [Category("MeowFramework/Events/InputController")]
    [Description("绑定 InputController 的 OnSprintAction")]
    public class OnSprintAction : EventNode
    {
        private FlowOutput raised;

        private ValueInput<MeowACTInputController> actionInput;

        public override void OnGraphStarted()
        {
            // 订阅事件
            actionInput.value.OnSprintAction += EventRaised;
        }

        public override void OnGraphStoped()
        {
            // 取消订阅事件
            actionInput.value.OnSprintAction -= EventRaised;
        }

        //Register the output flow port or any other port
        protected override void RegisterPorts()
        {
            raised = AddFlowOutput("Out");
            actionInput = AddValueInput<MeowACTInputController>("InputController");
        }

        //Fire output flow
        void EventRaised()
        {
            // Flow 流程调用
            raised.Call(new Flow());
        }
    }
}