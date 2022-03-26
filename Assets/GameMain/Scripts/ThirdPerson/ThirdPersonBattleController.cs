// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 25/03/2022 22:33
// 最后一次修改于: 26/03/2022 7:53
// 版权所有: ThinkDifferentStudio
// 描述:
// ----------------------------------------------

namespace OaksMayFall
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
        }

        public void MeleeAttack()
        {
            
        }
        
        /// <summary>
        /// 动画事件：尝试攻击
        /// </summary>
        public void TryDoDamage()
        {
            
        }
    }
}
