// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 11/03/2022 23:22
// 最后一次修改于: 26/03/2022 16:29
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using UnityEngine;

namespace OaksMayFall
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = 60;
            InitBuiltinComponents();
            InitCustomComponents();
        }
    }
}