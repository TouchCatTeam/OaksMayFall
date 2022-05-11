// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 10/04/2022 15:08
// 最后一次修改于: 20/04/2022 16:11
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

namespace MeowFramework.TPSCharacter
{
    /// <summary>
    /// 第三人称动画风格
    /// </summary>
    public enum TPSCharacterBehaviourMode
    {
        /// <summary>
        /// 没有武器
        /// </summary>
        NoWeapon = 0,
        
        /// <summary>
        /// 持步枪待机
        /// </summary>
        RifleIdle,
        
        /// <summary>
        /// 持步枪瞄准
        /// </summary>
        RifleAiming,
    }
}