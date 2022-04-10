// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 07/04/2022 14:44
// 最后一次修改于: 10/04/2022 10:23
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using MeowFramework.TPSCharacter;
using ParadoxNotion.Design;

namespace FlowCanvas.Nodes
{
    [Category("MeowFramework/Events/InputController")]
    [Description("绑定 InputController 的 OnAimAction")]
    public class OnAimAction : EventNode
    {
        private FlowOutput raised;

        /// <summary>
        /// 输入控制器
        /// </summary>
        private ValueInput<TPSCharacterInputController> inputController;
        
        public override void OnGraphStarted()
        {
            // 订阅事件
            if(inputController.value != null)
                inputController.value.OnAimAction += EventRaised;
        }

        public override void OnGraphStoped()
        {
            // 取消订阅事件
            if(inputController.value != null)
                inputController.value.OnAimAction -= EventRaised;
        }

        //Register the output flow port or any other port
        protected override void RegisterPorts()
        {
            raised = AddFlowOutput("Out");
            inputController = AddValueInput<TPSCharacterInputController>("InputController");
        }

        //Fire output flow
        void EventRaised()
        {
            // Flow 流程调用
            raised.Call(new Flow());
        }
    }
}