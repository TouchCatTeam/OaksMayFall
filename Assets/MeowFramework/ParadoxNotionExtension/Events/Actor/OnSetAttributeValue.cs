// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 06/04/2022 9:51
// 最后一次修改于: 12/04/2022 14:34
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using MeowFramework.Core.Scriptable;
using ParadoxNotion.Design;

namespace FlowCanvas.Nodes{
    
    [Category("MeowFramework/Events/Variable")]
    public class OnSetFloatVariable : EventNode
    {
        private FlowOutput raised;

        private ValueInput<ScriptableFloatVariable> variable;

        private float oldValue;

        private float newValue;
        
        private ValueOutput<float> OldValue;

        private ValueOutput<float> NewValue;
        
        public override void OnGraphStarted()
        {
            // 订阅事件
            variable.value.AfterSetValue += EventRaised;
        }

        public override void OnGraphStoped()
        {
            // 取消订阅事件
            variable.value.AfterSetValue -= EventRaised;
        }

        //Register the output flow port or any other port
        protected override void RegisterPorts()
        {
            raised = AddFlowOutput("Out");
            variable = AddValueInput<ScriptableFloatVariable>("ActorAttribute");
            OldValue = AddValueOutput<float>("OldValue", () => { return oldValue; });
            NewValue = AddValueOutput<float>("NewValue", () => { return newValue; });
        }

        //Fire output flow
        void EventRaised(float oldValue,float newValue)
        {
            // 从事件中获取参数
            this.oldValue = oldValue;
            this.newValue = newValue;
            
            // Flow 流程调用
            raised.Call(new Flow());
        }
    }
}