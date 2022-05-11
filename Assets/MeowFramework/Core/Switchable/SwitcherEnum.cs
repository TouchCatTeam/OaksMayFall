// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 22/04/2022 18:45
// 最后一次修改于: 26/04/2022 23:05
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core.Switchable
{
    public class SwitcherEnum<T> : ISwitcher where T: Enum
    {
        /// <summary>
        /// 主人
        /// </summary>
        private SerializedMonoBehaviour owner;

        /// <summary>
        /// 主人
        /// </summary>
        public SerializedMonoBehaviour Owner
        {
            set => owner = value;
        }

        /// <summary>
        /// 行动模式
        /// </summary>
        [ShowInInspector]
        [Tooltip("行动模式")]
        private T mode;

        /// <summary>
        /// 行动模式
        /// </summary>
        public T Mode
        {
            get => mode;
            set
            {
                if (owner != null)
                {
                    if (switchValueCoroutine != null)
                        owner.StopCoroutine(switchValueCoroutine);
                    switchValueCoroutine = owner.StartCoroutine(ModeTransition(value));
                    mode = value;
                }
            }
        }
        
        /// <summary>
        /// 切换模式的过渡时间
        /// </summary>
        [Tooltip("切换模式的过渡时间")]
        public float ModeTransitionTime = 1f;
        
        /// <summary>
        /// 可切换变量列表
        /// </summary>
        private List<ISwitchable> switchableList;

        /// <summary>
        /// 可切换变量列表
        /// </summary>
        public List<ISwitchable> SwitchableList
        {
            get
            {
                if(switchableList == null)
                    switchableList = new List<ISwitchable>();
                return switchableList;
            }
        }
        
        // 缓存

        /// <summary>
        /// 切换变量的协程
        /// </summary>
        private Coroutine switchValueCoroutine;
        
        /// <summary>
        /// 模式过渡：使变量在不同预设值之间切换
        /// </summary>
        /// <param name="mode">预设模式</param>
        /// <returns></returns>
        private IEnumerator ModeTransition(T mode)
        {
            float time = ModeTransitionTime;
            while(time > 0)
            {
                time -= Time.deltaTime;
                foreach (ISwitchable switchable in switchableList)
                {
                    switchable.SwitchValue(mode);
                }

                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return null;
        }
    }
}