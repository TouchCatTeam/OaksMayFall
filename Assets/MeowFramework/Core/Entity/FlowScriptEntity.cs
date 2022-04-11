// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 11/04/2022 17:49
// 最后一次修改于: 12/04/2022 0:17
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using FlowCanvas;
using Sirenix.OdinInspector;

namespace MeowFramework.Core.Entity
{
    /// <summary>
    /// 挂载 FlowScript 的实体
    /// </summary>
    public class FlowScriptEntity : SerializedMonoBehaviour
    {
        /// <summary>
        /// FlowScript 控制器
        /// </summary>
        public FlowScriptController flowScriptController;
        
        private void Awake()
        {
            flowScriptController = GetComponent<FlowScriptController>();
        }
    }
}