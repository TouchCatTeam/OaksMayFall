// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 26/03/2022 10:38
// 最后一次修改于: 26/03/2022 21:16
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 第三人称 UI 控制器
    /// </summary>
    public class ThirdPersonUIController
    {
        /// <summary>
        /// 第三人称 UI 控制器的主人
        /// </summary>
        public ThirdPerson Owner;
        
        /// <summary>
        /// 血条实例
        /// </summary>
        private UIBar hpBar;
        
        /// <summary>
        /// 耐力条实例
        /// </summary>
        private UIBar nrgBar;
        
        /// <summary>
        /// 第三人称 UI 控制器的构造函数
        /// </summary>
        /// <param name="owner">第三人称 UI 控制器的主人</param>
        public ThirdPersonUIController(ThirdPerson owner)
        {
            Owner = owner;

            hpBar = Owner.transform.Find("HPBarRoot").Find("UIBar").GetComponent<UIBar>();
            if(hpBar == null)
                Debug.LogError("玩家 Perfab 身上没有血条");
            
            nrgBar = Owner.transform.Find("NRGBarRoot").Find("UIBar").GetComponent<UIBar>();
            if(nrgBar == null)
                Debug.LogError("玩家 Perfab 身上没有体力条");
            
            // 事件绑定

            Owner.EventManager.OnHPChangeEvent += UpdateHPBar;
            Owner.EventManager.OnNRGChangeEvent += UpdateNRGBar;
        }

        /// <summary>
        /// 第三人称 UI 控制器的析构函数
        /// </summary>
        ~ThirdPersonUIController()
        {
            // 事件解绑

            Owner.EventManager.OnHPChangeEvent -= UpdateHPBar;
            Owner.EventManager.OnNRGChangeEvent -= UpdateNRGBar;
        }
        
        /// <summary>
        /// 血条 UI 更新
        /// </summary>
        /// <param name="args">血量参数</param>
        private void UpdateHPBar(object[] args)
        {
            hpBar.SmoothValueAndShow((float) args[0], (float) args[1]);
        }
        
        /// <summary>
        /// 体力条 UI 更新
        /// </summary>
        /// <param name="args">体力参数</param>
        private void UpdateNRGBar(object[] args)
        {
            nrgBar.SmoothValueAndShow((float)args[0], (float)args[1]);
        }
    }
}