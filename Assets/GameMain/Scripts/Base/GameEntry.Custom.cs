// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 11/03/2022 23:25
// 最后一次修改于: 30/03/2022 15:14
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 25/03/2022 16:50
// 最后一次修改于: 26/03/2022 7:13
// 版权所有: ThinkDifferentStudio
// 描述:
// ----------------------------------------------

using OaksMayFall;
using UnityEngine;

namespace OaksMayFall
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static BuiltinDataComponent BuiltinData
        {
            get;
            private set;
        }

        public static HPBarComponent HPBar
        {
            get;
            private set;
        }
        
        private static void InitCustomComponents()
        {
            BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
            HPBar = UnityGameFramework.Runtime.GameEntry.GetComponent<HPBarComponent>();
        }
    }
}
