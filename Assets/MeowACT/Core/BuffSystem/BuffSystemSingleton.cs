// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 27/03/2022 10:00
// 最后一次修改于: 30/03/2022 15:14
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// Buff 系统管理器
    /// </summary>
    public class BuffSystemSingleton : MonoBehaviour
    {
        /// <summary>
        /// Buff 系统管理器管理器单例
        /// </summary>
        private static readonly BuffSystemSingleton singleInstance;

        /// <summary>
        /// Buff 系统管理器管理器单例
        /// </summary>
        public static BuffSystemSingleton SingleInstance => singleInstance;

        /// <summary>
        /// buff 字典
        /// </summary>
        // private Dictionary<int, IBuff> buffMap = new Dictionary<int, IBuff>();
        
        /// <summary>
        /// Buff 池
        /// </summary>
        // private static Stack<IBuff> buffPool = new Stack<IBuff>();

        /// <summary>
        /// Buff 系统管理器管理器单例的静态构造函数
        /// 首次实例化该类或任何的静态成员被引用时调用该静态构造函数
        /// </summary>
        static BuffSystemSingleton()
        {
            var gameObject = new GameObject();
            DontDestroyOnLoad(gameObject);
            singleInstance = gameObject.AddComponent<BuffSystemSingleton>();
        }
    }
}