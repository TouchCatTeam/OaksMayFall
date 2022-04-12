// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 11/04/2022 23:27
// 最后一次修改于: 12/04/2022 20:42
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
        /// <param name="buffID"> Buff ID</param>
        /// <returns>Buff 实体</returns>
        public static BuffEntity TryAddBuff(ActorEntity caster, ActorEntity receiver, int buffID, int layer)
        {
            // 要检查这个 Buff 是否已经被生成了
            // 如果每一次 TryAddBuff 都生成一个 Buff 物体，那就没办法做到层数的叠加
            // 也不用在 root 下面找，因为可能会有很多 Buff
            // 所以那就直接找 receiver 吧
            // 所以要约定 receiver 不为 null
            if (receiver == null)
            {
                Debug.LogError($"{caster} 释放的 ID 为 {buffID} 的 Buff 没有接受者！");
                return null;
            }
            
            // 如果没有这个 Buff，就返回空
            if (!BuffComponent.ScriptableBuffDictionary.ContainsKey(buffID))
            {
                Debug.LogError($"{caster} 释放的 ID 为 {buffID} 的 Buff 在 Buff 组件的字典中不存在！");
                return null;
            }

            // 如果接受者身上已经有了相同 id 的 buff，那么直接增加层数
            if (receiver.AliveBuffDictionary.ContainsKey(buffID))
            {
                var buff = receiver.AliveBuffDictionary[buffID];
                buff.Layer += layer;
                return buff;
            }
            
            // 接受者身上没有相同 id 的 buff
            // 获得 Buff 数据
            var scriptableBuff = BuffComponent.ScriptableBuffDictionary[buffID];

            if(!BuffComponent.BuffPoolRoot)
                Debug.LogError("Buff 组件下面没有对象池的根节点！");
            
            // 创建空物体作为 Buff 物体
            GameObject entity = new GameObject();
            
            // Buff 物体放在统一的 root 下
            entity.transform.SetParent(BuffComponent.BuffPoolRoot);
            entity.name = scriptableBuff.FriendlyName == "" ? "NewBuffEntity" : scriptableBuff.FriendlyName + "Entity";
            
            // 初始化 FlowScript 控制器
            FlowScriptController flowScriptController = entity.AddComponent<FlowScriptController>();
            flowScriptController.StopBehaviour();
            flowScriptController.behaviour = scriptableBuff.BuffFlowScript;
            
            // 为 Buff 物体添加 BuffEntity，方便控制
            var buffEntity =  entity.AddComponent<BuffEntity>();
            buffEntity.BuffInitialize(caster, receiver, scriptableBuff, layer);
            
            // 执行 Buff
            buffEntity.StartBuffFlow();
            
            return buffEntity;
        }
    }
}