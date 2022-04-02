// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 16/03/2022 20:31
// 最后一次修改于: 02/04/2022 23:05
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using UnityEngine;

namespace MeowFramework.Core
{
    public static class MathUtility
    {
        /// <summary>
        /// 是否为非负数
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsntNegative(float value)
        {
            return value >= 0f;
        }

        /// <summary>
        /// 是否远大于
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="baseValue">基准值</param>
        /// <returns></returns>
        public static bool IsMuchLarger(float value, float baseValue)
        {
            return value >= Mathf.Pow(baseValue,3);
        }
        public static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
	        if (lfAngle < -360f) lfAngle += 360f;
	        if (lfAngle > 360f) lfAngle -= 360f;
	        return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
