// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/04/2022 19:31
// 最后一次修改于: 12/04/2022 20:42
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using MeowFramework.Core.Scriptable;
using NodeCanvas.Framework;
using UnityEngine;

namespace MeowFramework.Core.Entity
{
    /// <summary>
    /// 挂载 FlowScript 的实体
    /// </summary>
    public partial class BuffEntity
    {

        /// <summary>
        /// 初始化 Buff
        /// </summary>
        public void BuffInitialize(ActorEntity caster, ActorEntity receiver, ScriptableBuff scriptableBuff, int layer = 0)
        {
            this.caster = caster;
            this.receiver = receiver;
            this.scriptableBuff = scriptableBuff;
            this.layer = layer;
            updateMode = scriptableBuff.UpdateMode;
        }
        
        /// <summary>
        /// 启动 Buff
        /// </summary>
        public void StartBuffFlow()
        {
            flowScriptController.StartBehaviour(updateMode);
        }

        /// <summary>
        /// 更新 Buff
        /// </summary>
        public void UpdateBuffFlow()
        {
            flowScriptController.UpdateBehaviour();
        }
        
        /// <summary>
        /// 暂停 Buff
        /// </summary>
        public void PauseBuffFlow()
        {
            flowScriptController.PauseBehaviour();
        }
        
        /// <summary>
        /// 结束 Buff
        /// </summary>
        public void StopBuffFlow()
        {
            flowScriptController.StopBehaviour();
        }

        /// <summary>
        /// 重置 Buff
        /// </summary>
        public void ResetBuffFlow()
        {
            flowScriptController.RestartBehaviour();
        }

        /// <summary>
        /// Buff 结束
        /// </summary>
        public void EndBuff()
        {
            GameObject.Destroy(this);
        }
    }
}