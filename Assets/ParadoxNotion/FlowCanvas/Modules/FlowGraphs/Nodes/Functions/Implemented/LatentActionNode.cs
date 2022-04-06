// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 02/04/2022 21:50
// 最后一次修改于: 06/04/2022 9:38
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using ParadoxNotion.Services;

namespace FlowCanvas.Nodes
{

    ///<summary>Latent Action Nodes do not return any value and can span within multiple frames and have up to 10 parameters. They need Flow execution.</summary>
    abstract public class LatentActionNodeBase : SimplexNode
    {

        //...
        public enum InvocationMode
        {
            QueueCalls,
            FilterCalls
        }

        //enumerator - flow pair
        private struct RoutineData
        {
            public IEnumerator enumerator;
            public Flow flow;
            public RoutineData(IEnumerator enumerator, Flow flow) {
                this.enumerator = enumerator;
                this.flow = flow;
            }
        }

        public InvocationMode invocationMode;

        private FlowOutput onStart;
        private FlowOutput onUpdate;
        private FlowOutput onFinish;
        private Queue<RoutineData> routineQueue;
        private UnityEngine.Coroutine currentCoroutine;
        private bool graphStoped;

        public override string name {
            get { return routineQueue != null && routineQueue.Count > 0 ? string.Format("{0} [{1}]", base.name, routineQueue.Count.ToString()) : base.name; }
        }

        ///----------------------------------------------------------------------------------------------

        sealed public override void OnGraphStarted() { graphStoped = false; }
        sealed public override void OnGraphStoped() { graphStoped = true; BreakAll(false); }

        ///----------------------------------------------------------------------------------------------

        //begins a new coroutine
        protected void Begin(IEnumerator enumerator, Flow f) {
            // 封装传入的协程函数
            var data = new RoutineData(enumerator, f);
            // 如果当前节点没有正在执行的协程，那么立即进入传入的协程函数
            if ( currentCoroutine == null ) {
                currentCoroutine = parentNode.StartCoroutine(InternalCoroutine(data));
                return;
            }
            // 否则判断是否为协程队列模式，是则将传入的协程函数加入队列
            if ( routineQueue == null ) { routineQueue = new Queue<RoutineData>(); }
            if ( allowRoutineQueueing && invocationMode == InvocationMode.QueueCalls ) {
                routineQueue.Enqueue(data);
            }
            // 如果当前节点有正在执行的协程，并且不为协程队列模式，那么就不执行传入的协程函数，相当于起到了一个运行时筛选的功能
        }

        //breaks all coroutine queues
        protected void BreakAll(bool callFinish) {
            if ( currentCoroutine != null ) {
                parentNode.StopCoroutine(currentCoroutine);
                currentCoroutine = null;
                routineQueue = null;
                parentNode.SetStatus(NodeCanvas.Framework.Status.Resting);
                OnBreak();
                if ( !graphStoped && callFinish ) {
                    onFinish.Call(new Flow());
                }
            }
        }

        IEnumerator InternalCoroutine(RoutineData data) {
            Flow f = data.flow;
            parentNode.SetStatus(NodeCanvas.Framework.Status.Running);
            if ( onStart != null ) {
                onStart.Call(f);
            }

            f.BeginBreakBlock(() => { BreakAll(true); });
            while ( data.enumerator.MoveNext() ) {
                while ( !parentNode.graph.didUpdateLastFrame ) {
                    yield return null;
                }
                if ( onUpdate != null ) {
                    onUpdate.Call(f);
                }
                yield return data.enumerator.Current;
            }
            f.EndBreakBlock();

            parentNode.SetStatus(NodeCanvas.Framework.Status.Resting);
            onFinish.Call(f);
            currentCoroutine = null;

            if ( routineQueue != null && routineQueue.Count > 0 ) {
                var next = routineQueue.Dequeue();
                currentCoroutine = parentNode.StartCoroutine(InternalCoroutine(next));
            }
        }

        ///----------------------------------------------------------------------------------------------

        protected override void OnRegisterPorts(FlowNode node) {
            //to make update safe from previous version, the ID (2nd string), is same as the old version. The first string, is the display name.
            if ( allowRoutineQueueing ) {
                onStart = node.AddFlowOutput("Start", "Out");
                onUpdate = node.AddFlowOutput("Update", "Doing");
            }
            onFinish = node.AddFlowOutput("Finish", "Done");
            OnRegisterDerivedPorts(node);
            if ( allowRoutineQueueing ) {
                node.AddFlowInput("Break", (f) => { BreakAll(true); });
                node.AddFlowInput("Cancel", (f) => { BreakAll(false); });
            }
        }

        ///----------------------------------------------------------------------------------------------

        ///<summary>Register the special per type ports</summary>
        abstract protected void OnRegisterDerivedPorts(FlowNode node);
        ///<summary>Called when Break input is called.</summary>
        virtual public void OnBreak() { }
        ///<summary>Should extra ports be shown?</summary>
        virtual public bool allowRoutineQueueing {
            get { return true; }
        }
    }

    ///----------------------------------------------------------------------------------------------

    abstract public class LatentActionNode : LatentActionNodeBase
    {
        abstract public IEnumerator Invoke();
        sealed protected override void OnRegisterDerivedPorts(FlowNode node) {
            node.AddFlowInput("In", (f) => { Begin(Invoke(), f); });
        }
    }

    abstract public class LatentActionNode<T1> : LatentActionNodeBase
    {
        abstract public IEnumerator Invoke(T1 a);
        sealed protected override void OnRegisterDerivedPorts(FlowNode node) {
            var p1 = node.AddValueInput<T1>(parameters[0].Name);
            node.AddFlowInput("In", (f) => { Begin(Invoke(p1.value), f); });
        }
    }

    abstract public class LatentActionNode<T1, T2> : LatentActionNodeBase
    {
        abstract public IEnumerator Invoke(T1 a, T2 b);
        sealed protected override void OnRegisterDerivedPorts(FlowNode node) {
            var p1 = node.AddValueInput<T1>(parameters[0].Name);
            var p2 = node.AddValueInput<T2>(parameters[1].Name);
            node.AddFlowInput("In", (f) => { Begin(Invoke(p1.value, p2.value), f); });
        }
    }

    abstract public class LatentActionNode<T1, T2, T3> : LatentActionNodeBase
    {
        abstract public IEnumerator Invoke(T1 a, T2 b, T3 c);
        sealed protected override void OnRegisterDerivedPorts(FlowNode node) {
            var p1 = node.AddValueInput<T1>(parameters[0].Name);
            var p2 = node.AddValueInput<T2>(parameters[1].Name);
            var p3 = node.AddValueInput<T3>(parameters[2].Name);
            node.AddFlowInput("In", (f) => { Begin(Invoke(p1.value, p2.value, p3.value), f); });
        }
    }

    abstract public class LatentActionNode<T1, T2, T3, T4> : LatentActionNodeBase
    {
        abstract public IEnumerator Invoke(T1 a, T2 b, T3 c, T4 d);
        sealed protected override void OnRegisterDerivedPorts(FlowNode node) {
            var p1 = node.AddValueInput<T1>(parameters[0].Name);
            var p2 = node.AddValueInput<T2>(parameters[1].Name);
            var p3 = node.AddValueInput<T3>(parameters[2].Name);
            var p4 = node.AddValueInput<T4>(parameters[3].Name);
            node.AddFlowInput("In", (f) => { Begin(Invoke(p1.value, p2.value, p3.value, p4.value), f); });
        }
    }

    abstract public class LatentActionNode<T1, T2, T3, T4, T5> : LatentActionNodeBase
    {
        abstract public IEnumerator Invoke(T1 a, T2 b, T3 c, T4 d, T5 e);
        sealed protected override void OnRegisterDerivedPorts(FlowNode node) {
            var p1 = node.AddValueInput<T1>(parameters[0].Name);
            var p2 = node.AddValueInput<T2>(parameters[1].Name);
            var p3 = node.AddValueInput<T3>(parameters[2].Name);
            var p4 = node.AddValueInput<T4>(parameters[3].Name);
            var p5 = node.AddValueInput<T5>(parameters[4].Name);
            node.AddFlowInput("In", (f) => { Begin(Invoke(p1.value, p2.value, p3.value, p4.value, p5.value), f); });
        }
    }

    abstract public class LatentActionNode<T1, T2, T3, T4, T5, T6> : LatentActionNodeBase
    {
        abstract public IEnumerator Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f);
        sealed protected override void OnRegisterDerivedPorts(FlowNode node) {
            var p1 = node.AddValueInput<T1>(parameters[0].Name);
            var p2 = node.AddValueInput<T2>(parameters[1].Name);
            var p3 = node.AddValueInput<T3>(parameters[2].Name);
            var p4 = node.AddValueInput<T4>(parameters[3].Name);
            var p5 = node.AddValueInput<T5>(parameters[4].Name);
            var p6 = node.AddValueInput<T6>(parameters[5].Name);
            node.AddFlowInput("In", (f) => { Begin(Invoke(p1.value, p2.value, p3.value, p4.value, p5.value, p6.value), f); });
        }
    }

    abstract public class LatentActionNode<T1, T2, T3, T4, T5, T6, T7> : LatentActionNodeBase
    {
        abstract public IEnumerator Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g);
        sealed protected override void OnRegisterDerivedPorts(FlowNode node) {
            var p1 = node.AddValueInput<T1>(parameters[0].Name);
            var p2 = node.AddValueInput<T2>(parameters[1].Name);
            var p3 = node.AddValueInput<T3>(parameters[2].Name);
            var p4 = node.AddValueInput<T4>(parameters[3].Name);
            var p5 = node.AddValueInput<T5>(parameters[4].Name);
            var p6 = node.AddValueInput<T6>(parameters[5].Name);
            var p7 = node.AddValueInput<T7>(parameters[6].Name);
            node.AddFlowInput("In", (f) => { Begin(Invoke(p1.value, p2.value, p3.value, p4.value, p5.value, p6.value, p7.value), f); });
        }
    }

    abstract public class LatentActionNode<T1, T2, T3, T4, T5, T6, T7, T8> : LatentActionNodeBase
    {
        abstract public IEnumerator Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h);
        sealed protected override void OnRegisterDerivedPorts(FlowNode node) {
            var p1 = node.AddValueInput<T1>(parameters[0].Name);
            var p2 = node.AddValueInput<T2>(parameters[1].Name);
            var p3 = node.AddValueInput<T3>(parameters[2].Name);
            var p4 = node.AddValueInput<T4>(parameters[3].Name);
            var p5 = node.AddValueInput<T5>(parameters[4].Name);
            var p6 = node.AddValueInput<T6>(parameters[5].Name);
            var p7 = node.AddValueInput<T7>(parameters[6].Name);
            var p8 = node.AddValueInput<T8>(parameters[7].Name);
            node.AddFlowInput("In", (f) => { Begin(Invoke(p1.value, p2.value, p3.value, p4.value, p5.value, p6.value, p7.value, p8.value), f); });
        }
    }

    abstract public class LatentActionNode<T1, T2, T3, T4, T5, T6, T7, T8, T9> : LatentActionNodeBase
    {
        abstract public IEnumerator Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i);
        sealed protected override void OnRegisterDerivedPorts(FlowNode node) {
            var p1 = node.AddValueInput<T1>(parameters[0].Name);
            var p2 = node.AddValueInput<T2>(parameters[1].Name);
            var p3 = node.AddValueInput<T3>(parameters[2].Name);
            var p4 = node.AddValueInput<T4>(parameters[3].Name);
            var p5 = node.AddValueInput<T5>(parameters[4].Name);
            var p6 = node.AddValueInput<T6>(parameters[5].Name);
            var p7 = node.AddValueInput<T7>(parameters[6].Name);
            var p8 = node.AddValueInput<T8>(parameters[7].Name);
            var p9 = node.AddValueInput<T9>(parameters[8].Name);
            node.AddFlowInput("In", (f) => { Begin(Invoke(p1.value, p2.value, p3.value, p4.value, p5.value, p6.value, p7.value, p8.value, p9.value), f); });
        }
    }

    abstract public class LatentActionNode<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : LatentActionNodeBase
    {
        abstract public IEnumerator Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j);
        sealed protected override void OnRegisterDerivedPorts(FlowNode node) {
            var p1 = node.AddValueInput<T1>(parameters[0].Name);
            var p2 = node.AddValueInput<T2>(parameters[1].Name);
            var p3 = node.AddValueInput<T3>(parameters[2].Name);
            var p4 = node.AddValueInput<T4>(parameters[3].Name);
            var p5 = node.AddValueInput<T5>(parameters[4].Name);
            var p6 = node.AddValueInput<T6>(parameters[5].Name);
            var p7 = node.AddValueInput<T7>(parameters[6].Name);
            var p8 = node.AddValueInput<T8>(parameters[7].Name);
            var p9 = node.AddValueInput<T9>(parameters[8].Name);
            var p10 = node.AddValueInput<T10>(parameters[9].Name);
            node.AddFlowInput("In", (f) => { Begin(Invoke(p1.value, p2.value, p3.value, p4.value, p5.value, p6.value, p7.value, p8.value, p9.value, p10.value), f); });
        }
    }

}