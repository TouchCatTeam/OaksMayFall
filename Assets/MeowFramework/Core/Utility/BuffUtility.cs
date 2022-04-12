// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 11/04/2022 23:27
// 最后一次修改于: 12/04/2022 14:46
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using FlowCanvas;
using MeowFramework.Core.Entity;
using MeowFramework.Core.FrameworkComponent;
using UnityEngine;

namespace MeowFramework.Core.Utility
{
    public static class BuffUtility
    {
        /// <summary>
        /// 尝试释放 Buff
        /// </summary>
        /// <param name="caster">Buff 释放者</param>
        /// <param name="receiver">Buff 接受者</param>
        /// <param name="buffIndex"> Buff ID</param>
        /// <returns>Buff 实体</returns>
        public static BuffEntity TryAddBuff(ActorEntity caster, ActorEntity receiver, int buffIndex)
        {
            // 如果没有这个 Buff，就返回空
            if (!BuffComponent.ScriptableBuffDictionary.ContainsKey(buffIndex))
            {
                Debug.LogError($"Buff 组件的 Buff 字典中不存在键为 {buffIndex} 的条目！");
                return null;
            }

            // 如果有这个 Buff，获得 Buff 数据
            var scriptableBuff = BuffComponent.ScriptableBuffDictionary[buffIndex];

            if(!BuffComponent.BuffPoolRoot)
                Debug.LogError("Buff 组件下面没有对象池的根节点！");
            
            // 创建空物体作为 Buff 物体
            GameObject entity = new GameObject();
            
            // Ability 物体放在统一的 root 下
            entity.transform.SetParent(BuffComponent.BuffPoolRoot);
            entity.name = scriptableBuff.FriendlyName == "" ? "NewBuffEntity" : scriptableBuff.FriendlyName + "Entity";
            
            // 初始化 FlowScript 控制器
            FlowScriptController flowScriptController = entity.AddComponent<FlowScriptController>();
            flowScriptController.StopBehaviour();
            flowScriptController.behaviour = scriptableBuff.BuffFlowScript;
            
            // 为 Buff 物体添加 BuffEntity，方便控制
            var buffEntity =  entity.AddComponent<BuffEntity>();
            buffEntity.BuffInitialize(caster, receiver, scriptableBuff);
            
            // 执行 Buff
            buffEntity.StartBuff();
            
            return buffEntity;
        }
    }
}