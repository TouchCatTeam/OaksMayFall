// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 10/04/2022 8:57
// 最后一次修改于: 10/04/2022 9:16
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using MeowFramework;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FlowCanvas.Nodes
{
    [Category("MeowFramework/Ability")]
    [Description("获得子弹落点")]
    public class GetBulletHit : CallableFunctionNode<RaycastHit>
    {
        public BBParameter<LayerMask> ShootingLayerMask = default;

        public override RaycastHit Invoke()
        {
            Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            if(Physics.Raycast(ray,out RaycastHit raycastHit, 999f, ShootingLayerMask.value))
            {
                return raycastHit;
            }

            return default;
        }
    }
}