// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 25/03/2022 23:24
// 最后一次修改于: 12/04/2022 15:56
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using MeowFramework.Core;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;

namespace MeowFramework.TPSCharacter
{
    /// <summary>
    /// 第三人称动画状态机控制器
    /// </summary>
    public partial class TPSCharacterAnimationController : SerializedMonoBehaviour
    {
        // 组件

        /// <summary>
        /// 第三人称运动管理器
        /// </summary>
        [BoxGroup("Component")]
        [Required]
        [Tooltip("第三人称运动管理器")]
        public TPSCharacterLocomotionController LocomotionController;
        
        /// <summary>
        /// 动画控制器
        /// </summary>
        [BoxGroup("Component")]
        [Required]
        [Tooltip("动画控制器")]
        public Animator Anim;

        public void Start()
        {
            AssignAnimationIDs();
        }

        public void Update()
        {
            SetAnimatorValue();
        }
    }
}