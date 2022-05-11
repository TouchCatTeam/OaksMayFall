// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 20/04/2022 15:17
// 最后一次修改于: 22/04/2022 14:43
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using MeowFramework.Core.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace MeowFramework.TPSCharacter
{
    public partial class TPSCharacterAnimationController
    {
        /// <summary>
        /// 子弹落点
        /// 建议放入一个场景根节点下的空物体
        /// </summary>
        [BoxGroup("IK")]
        [Required]
        [Tooltip("子弹落点 建议放入一个场景根节点下的空物体")]
        public Transform bulletHitPoint;
        
        /// <summary>
        /// 步枪待机姿态所用到的骨骼绑定
        /// </summary>
        [BoxGroup("IK")]
        [Tooltip("步枪待机姿态所用到的骨骼绑定")]
        public Rig rifleIdleRig;
        
        /// <summary>
        /// 步枪瞄准姿态所用到的骨骼绑定
        /// </summary>
        [BoxGroup("IK")]
        [Tooltip("步枪瞄准姿态所用到的骨骼绑定")]
        public Rig rifleAimingRig;

        private void UpdateRig()
        {
            // 子弹落点
            bulletHitPoint.position = TPSUtility.GetBulletHitPoint();
        }
    }
}