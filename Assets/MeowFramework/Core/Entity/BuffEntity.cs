// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 11/04/2022 23:41
// 最后一次修改于: 12/04/2022 20:28
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using MeowFramework.Core.Scriptable;
using NodeCanvas.Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

namespace MeowFramework.Core.Entity
{
    /// <summary>
    /// 挂载 FlowScript 的实体
    /// </summary>
    public partial class BuffEntity : FlowScriptEntity
    {
        /// <summary>
        /// 可资产化 Buff
        /// </summary>
        [ShowInInspector]
        [ReadOnly]
        [Tooltip("可资产化 Buff")]
        private ScriptableBuff scriptableBuff;
        
        /// <summary>
        /// Buff 释放者
        /// 不考虑释放着为多人的情况
        /// </summary>
        [ShowInInspector]
        [ReadOnly]
        [Tooltip("Buff 释放者")]
        private ActorEntity caster;

        /// <summary>
        /// Buff 释放者
        /// 不考虑释放着为多人的情况
        /// </summary>
        public ActorEntity Caster
        {
            get => caster;
            set
            {
                AfterCasterChange?.Invoke(caster, value);
                caster = value;
            }
        }
        
        /// <summary>
        /// Buff 接受者
        /// 不考虑接受者为多人的情况
        /// </summary>
        [ShowInInspector]
        [ReadOnly]
        [Tooltip("Buff 接受者")]
        private ActorEntity receiver;

        /// <summary>
        /// Buff 接受者
        /// 不考虑接受者为多人的情况
        /// </summary>
        public ActorEntity Receiver
        {
            get => receiver;
            set
            {
                AfterReceiverChange?.Invoke(receiver, value);
                receiver = value;
            }
        }

        /// <summary>
        /// 层级
        /// </summary>
        [ShowInInspector]
        [ReadOnly]
        [Tooltip("层级")]
        private int layer;

        /// <summary>
        /// 层级
        /// </summary>
        public int Layer
        {
            get => layer;
            set
            {
                // 如果有层级限制，并且超过了层级限制，那么触发层级变化失败委托
                if (scriptableBuff.LayerStackLimitType == BuffLayerStackLimitType.Limited &&
                    value > scriptableBuff.MaxLayers)
                {
                    AfterLayerChangeFailure?.Invoke(layer, value);
                    return;
                }
                // 否则触发层级变化成功委托
                AfterLayerChangeSuccess?.Invoke(layer, value);
                layer = value;
            }
        }

        /// <summary>
        /// 时长
        /// </summary>
        [ShowInInspector]
        [ReadOnly]
        [Tooltip("时长")]
        private float duration;

        /// <summary>
        /// 时长
        /// 允许将时长设置为 0
        /// </summary>
        public float Duration
        {
            get => duration;
            set
            {
                var validValue = Mathf.Clamp(value, 0, float.MaxValue);
                AfterDurationChange?.Invoke(duration, validValue);
                duration = validValue;
            }
        }

        /// <summary>
        /// 已流逝时间
        /// </summary>
        [ShowInInspector]
        [ReadOnly]
        [Tooltip("已流逝时间")]
        private float elapsedTime;

        /// <summary>
        /// 已流逝时间
        /// </summary>
        public float ElapsedTime
        {
            get => elapsedTime;
            set
            {
                elapsedTime = value;
                if (elapsedTime < 0)
                {
                    elapsedTime += Duration;
                    Layer -= 1;
                    AfterOneLayerTimeRunOut(Layer);
                    if (Layer == 0 && scriptableBuff.DurationType == BuffDurationType.Durable)
                        EndBuff();
                }
            }
        }

        /// <summary>
        /// FlowScript 更新模式
        /// </summary>
        [ShowInInspector]
        [ReadOnly]
        [Tooltip("已流逝时间")]
        private Graph.UpdateMode updateMode;

        /// <summary>
        /// FlowScript 更新模式
        /// </summary>
        public Graph.UpdateMode UpdateMode
        {
            get => updateMode;
            set
            {
                AfterUpdateModeChange?.Invoke();
                updateMode = value;
            }
        }
        
        /// <summary>
        /// 释放者改变时触发的委托
        /// </summary>
        [HideInInspector]
        public Action<ActorEntity, ActorEntity> AfterCasterChange;

        /// <summary>
        /// 接受者改变时触发的委托
        /// </summary>
        [HideInInspector]
        public Action<ActorEntity, ActorEntity> AfterReceiverChange;
        
        /// <summary>
        /// 层级变化失败时触发的委托
        /// </summary>
        [HideInInspector]
        public Action<int, int> AfterLayerChangeFailure; 
        
        /// <summary>
        /// 层级变化成功时触发的委托
        /// </summary>
        [HideInInspector]
        public Action<int, int> AfterLayerChangeSuccess;

        /// <summary>
        /// 时长变化成功时触发的委托
        /// </summary>
        [HideInInspector]
        public Action<float, float> AfterDurationChange;
        
        /// <summary>
        /// 每个层级时间耗尽时
        /// 采用每层时间独立计算
        /// 不考虑多层 Buff 只有一个时间的情况
        /// 可以在触发时间耗尽事件时将层数清零来实现多层 Buff 只有一个时间
        /// </summary>
        [HideInInspector]
        public Action<int> AfterOneLayerTimeRunOut;

        /// <summary>
        /// FlowScript 更新模式改变之后触发的委托
        /// </summary>
        public Action AfterUpdateModeChange;
    }
}