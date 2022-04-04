// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 03/04/2022 21:28
// 最后一次修改于: 04/04/2022 8:52
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using MeowFramework.Core;
using ParadoxNotion.Design;

namespace FlowCanvas.Nodes
{

    [Category("Events/MeowFramework")]
    public class OnSetAttributeValue<T> : EventNode
    {
        private FlowOutput raised;

        private ValueInput<ActorAttribute<T>> actorAttribute;

        private T oldValue;

        private T newValue;

        private ValueOutput<T> OldValue;

        private ValueOutput<T> NewValue;

        public override void OnGraphStarted()
        {
            // 订阅事件
            actorAttribute.value.AfterSetValue += EventRaised;
        }

        public override void OnGraphStoped()
        {
            // 取消订阅事件
            actorAttribute.value.AfterSetValue -= EventRaised;
        }

        //Register the output flow port or any other port
        protected override void RegisterPorts()
        {
            raised = AddFlowOutput("Out");
            actorAttribute = AddValueInput<ActorAttribute<T>>("ActorAttribute");
            OldValue = AddValueOutput<T>("OldValue", () => { return oldValue; });
            NewValue = AddValueOutput<T>("NewValue", () => { return newValue; });
        }

        //Fire output flow
        void EventRaised(T oldValue, T newValue)
        {
            // 从事件中获取参数
            this.oldValue = oldValue;
            this.newValue = newValue;

            // Flow 流程调用
            raised.Call(new Flow());
        }
    }
}