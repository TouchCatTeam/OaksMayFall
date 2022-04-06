// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 02/04/2022 21:50
// 最后一次修改于: 05/04/2022 20:20
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace FlowCanvas.Nodes
{
    [Name("Sub Flow")]
    [DropReferenceType(typeof(FlowCanvas.FlowScript))]
    public class FlowNestedFlow : FlowNestedBase<FlowCanvas.FlowScript>
    {

    }
}