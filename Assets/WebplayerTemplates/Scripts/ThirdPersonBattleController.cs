// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 25/03/2022 22:33
// 最后一次修改于: 26/03/2022 22:21
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 第三人称战斗控制器
    /// </summary>
    public class ThirdPersonBattleController
    {
        /// <summary>
        /// 第三人称战斗控制器的主人
        /// </summary>
        public ThirdPerson Owner;

        /// <summary>
        /// 第三人称战斗控制器的构造函数
        /// </summary>
        /// <param name="owner">第三人称战斗控制器的主人</param>
        public ThirdPersonBattleController(ThirdPerson owner)
        {
            Owner = owner;
            
            // 事件绑定

            Owner.EventManager.TryDoDamageEvent += TryDoDamage;
        }

        /// <summary>
        /// 第三人称战斗控制器的析构函数
        /// </summary>
        ~ThirdPersonBattleController()
        {
            // 事件解绑

            Owner.EventManager.TryDoDamageEvent -= TryDoDamage;
        }
        
        public void MeleeAttack()
        {
            
        }
        
        /// <summary>
        /// 动画事件：尝试攻击
        /// </summary>
        private void TryDoDamage(object[] args)
        {
            
        }
    }
}
