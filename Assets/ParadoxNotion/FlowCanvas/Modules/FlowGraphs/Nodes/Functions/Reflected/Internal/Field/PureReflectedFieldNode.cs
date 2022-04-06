// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 02/04/2022 21:50
// 最后一次修改于: 06/04/2022 11:01
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Reflection;
using ParadoxNotion;

namespace FlowCanvas.Nodes
{
    public class PureReflectedFieldNode : BaseReflectedFieldNode
    {
        private ValueInput instanceInput;
        private ValueInput valueInput;
        private object instanceObject;
        private object valueObject;

        protected override bool InitInternal(FieldInfo method) {
            instanceInput = null;
            instanceObject = null;
            valueObject = null;
            return true;
        }

        private void SetValue() {
            valueObject = valueInput != null ? valueInput.value : null;
            instanceObject = instanceInput != null ? instanceInput.value : null;
            fieldInfo.SetValue(instanceObject, valueObject);
        }

        private void GetValue() {
            instanceObject = instanceInput != null ? instanceInput.value : null;
            valueObject = fieldInfo.GetValue(instanceObject);
        }

        public override void RegisterPorts(FlowNode node, ReflectedFieldNodeWrapper.AccessMode accessMode) {
            if ( fieldInfo == null ) return;

            if ( accessMode == ReflectedFieldNodeWrapper.AccessMode.SetField && !fieldInfo.IsReadOnly() ) {
                var output = node.AddFlowOutput(" ");
                node.AddFlowInput(" ", flow =>
                {
                    SetValue();
                    output.Call(flow);
                });
            }

            //non static
            if ( instanceDef.paramMode != ParamMode.Undefined ) {
                instanceInput = node.AddValueInput(instanceDef.portName, instanceDef.paramType, instanceDef.portId);
                if ( accessMode == ReflectedFieldNodeWrapper.AccessMode.SetField && !fieldInfo.IsReadOnly() ) {
                    node.AddValueOutput(instanceDef.portName, instanceDef.paramType, () => instanceObject, instanceDef.portId);
                }
            } else {
                instanceInput = null;
                instanceObject = null;
            }

            if ( accessMode == ReflectedFieldNodeWrapper.AccessMode.SetField && !fieldInfo.IsReadOnly() ) {
                valueInput = node.AddValueInput(resultDef.portName, resultDef.paramType, resultDef.portId);
            } else {
                node.AddValueOutput(resultDef.portName, resultDef.portId, resultDef.paramType, () =>
                {
                    GetValue();
                    return valueObject;
                });
            }
        }
    }
}