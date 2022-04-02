// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 01/04/2022 22:22
// 最后一次修改于: 02/04/2022 1:02
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowACT
{
    /// <summary>
    /// 可资产化 string 变量
    /// </summary>
    [InlineEditor]
    [CreateAssetMenu(menuName = "MeowACT/Scriptable Variable/Create Scriptable String Variable")]
    public class ScriptableStringVariable : ScriptableGenericVariable<string>
    {
        
    }
}