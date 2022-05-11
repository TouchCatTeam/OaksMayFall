// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 20/04/2022 15:35
// 最后一次修改于: 22/04/2022 9:01
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Collections.Generic;
using System.Reflection;
using Unity.Collections;
using UnityEngine;

namespace MeowFramework.Core.Utility
{
    public static class TPSUtility
    {
        public static Vector3 GetBulletHitPoint(LayerMask ShootingLayerMask = default)
        {
            Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            if(Physics.Raycast(ray,out RaycastHit raycastHit, 999f, ShootingLayerMask.value))
                return raycastHit.point;
            else
                return ray.origin + ray.direction * 999f;
        }
    }
    
}
