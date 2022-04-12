// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 11/04/2022 23:41
// 最后一次修改于: 12/04/2022 14:46
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using MeowFramework.Core.Scriptable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core.Entity
{
    /// <summary>
    /// 挂载 FlowScript 的实体
    /// </summary>
    public class BuffEntity : FlowScriptEntity
    {
        /// <summary>
        /// Buff 释放者
        /// </summary>
        [ShowInInspector]
        [Sirenix.OdinInspector.ReadOnly]
        [Tooltip("Buff 释放者")]
        private ActorEntity caster;

        /// <summary>
        /// Buff 接受者
        /// </summary>
        [ShowInInspector]
        [Sirenix.OdinInspector.ReadOnly]
        [Tooltip("Buff 接受者")]
        private ActorEntity receiver;
        
        /// <summary>
        /// 初始化 Buff
        /// </summary>
        public void BuffInitialize(ActorEntity caster, ActorEntity receiver, ScriptableBuff scriptableBuff)
        {
            this.caster = caster;
            this.receiver = receiver;
            
        }
        
        /// <summary>
        /// 启动 Buff
        /// </summary>
        public void StartBuff()
        {
            flowScriptController.StartBehaviour();
        }

        /// <summary>
        /// 暂停 Buff
        /// </summary>
        public void PauseBuff()
        {
            flowScriptController.PauseBehaviour();
        }
        
        /// <summary>
        /// 结束 Buff
        /// </summary>
        public void StopBuff()
        {
            flowScriptController.StopBehaviour();
        }

        /// <summary>
        /// 重置 Buff
        /// </summary>
        public void ResetBuff()
        {
            flowScriptController.RestartBehaviour();
        }
    }
}