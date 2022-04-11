// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 05/04/2022 0:43
// 最后一次修改于: 11/04/2022 10:31
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.Core.FrameworkComponent
{
    [InlineEditor]
    public class InitializationComponent : BaseComponent
    {
        void Start()
        {
            Application.targetFrameRate = 60;
        }
    }
}
