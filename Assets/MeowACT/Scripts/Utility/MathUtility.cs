// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 16/03/2022 20:31
// 最后一次修改于: 26/03/2022 16:29
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using UnityEngine;

namespace MeowACT
{
    public static class MathUtility
    {
        public static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
	        if (lfAngle < -360f) lfAngle += 360f;
	        if (lfAngle > 360f) lfAngle -= 360f;
	        return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
