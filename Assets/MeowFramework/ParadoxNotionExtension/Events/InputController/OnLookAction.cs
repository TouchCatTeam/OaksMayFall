// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 06/04/2022 15:04
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
    [Description("绑定 InputController 的 OnLookAction")]
    public class OnLookAction : EventNode
    {
        private FlowOutput raised;

        private ValueInput<MeowACTInputController> actionInput;

        /// <summary>
        /// 输入 Action 的参数
        /// </summary>
        private Vector2 value;
        
        private ValueOutput<Vector2> Value;
        
        public override void OnGraphStarted()
        {
            // 订阅事件
            actionInput.value.OnLookAction += EventRaised;
        }

        public override void OnGraphStoped()
        {
            // 取消订阅事件
            actionInput.value.OnLookAction -= EventRaised;
        }

        //Register the output flow port or any other port
        protected override void RegisterPorts()
        {
            raised = AddFlowOutput("Out");
            actionInput = AddValueInput<MeowACTInputController>("InputController");
            Value = AddValueOutput<Vector2>("Value", () => { return value; });
        }

        //Fire output flow
        void EventRaised(Vector2 value)
        {
            // 获取输入事件的参数
            this.value = value;
            
            // Flow 流程调用
            raised.Call(new Flow());
        }
    }
}