// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 26/03/2022 15:00
// 最后一次修改于: 26/03/2022 22:15
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 第三人称属性管理器
    /// </summary>
    public class ThirdPersonAttributeManager
    {
        // 刚体模拟

        public LayerMask pushLayers;
        public bool canPush;
        [Range(0.5f, 5f)] public float strength = 1.1f;

        // 运动参数

        /// <summary>
        /// 水平速度
        /// </summary>
        /// <returns></returns>
        public Vector3 HorizontalVelocity;

        /// <summary>
        /// 竖向速度
        /// </summary>
        public Vector3 VerticalVelocity;

        /// <summary>
        /// 血量
        /// </summary>
        private float hp;
        
        /// <summary>
        /// 最大血量
        /// </summary>
        public float MaxHP = 100f;
        
        /// <summary>
        /// 血量
        /// </summary>
        public float HP
        {
            get => hp;
            set
            {
                Owner.EventManager.Fire("OnHPChangeEvent", new object[]{hp/MaxHP, value/MaxHP});
                hp = value;
            }
        }

        /// <summary>
        /// 体力
        /// </summary>
        private float nrg;

        /// <summary>
        /// 最大体力
        /// </summary>
        public float MaxNRG = 100f;
        
        /// <summary>
        /// 体力
        /// </summary>
        public float NRG
        {
            get => nrg;
            set
            {
                Owner.EventManager.Fire("OnNRGChangeEvent", new object[]{nrg/MaxNRG, value/MaxNRG});
                nrg = value;
            }
        }

        /// <summary>
        /// 冲刺的体力消耗
        /// </summary>
        public float SprintCost = 10f;
        
        /// <summary>
        /// 近战攻击的体力消耗
        /// </summary>
        public float MeleeAttackCost = 10f;
        
        // 运动状态

        // 运动状态 - 冲刺相关

        /// <summary>
        /// 是否落地
        /// </summary>
        public bool IsGrounded = true;

        /// <summary>
        /// 是否正在静止运动
        /// </summary>
        public bool IsFreezingMove = false;

        /// <summary>
        /// 是否正在冲刺
        /// </summary>
        public bool IsSprinting = false;

        /// <summary>
        /// 是否开始冲刺
        /// </summary>
        public bool IsSprintBegin = false;

        // 运动状态 - 攻击相关

        /// <summary>
        /// 是否正在攻击
        /// </summary>
        public bool IsMeleeAttacking = false;

        /// <summary>
        /// 是否霸体
        /// </summary>
        public bool IsSuperArmor = false;
        
        /// <summary>
        /// 第三人称属性管理器的主人
        /// </summary>
        public ThirdPerson Owner;

        /// <summary>
        /// 第三人称属性管理器的构造函数
        /// </summary>
        /// <param name="owner">第三人称属性管理器的主人</param>
        public ThirdPersonAttributeManager(ThirdPerson owner)
        {
            Owner = owner;
        }
        
        public void Init()
        {
            HP = MaxHP;
            NRG = MaxNRG;
        }
    }
}