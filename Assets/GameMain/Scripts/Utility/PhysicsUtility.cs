// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 26/03/2022 0:29
// 最后一次修改于: 26/03/2022 7:13
// 版权所有: ThinkDifferentStudio
// 描述:
// ----------------------------------------------

using UnityEngine;

namespace OaksMayFall
{
    public static class PhysicsUtility
    {
        public static void PushRigidBodies(ControllerColliderHit hit, LayerMask pushLayers, float strength)
        {
            // https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html

            // make sure we hit a non kinematic rigidbody
            Rigidbody body = hit.collider.attachedRigidbody;
            if (body == null || body.isKinematic) return;

            // make sure we only push desired layer(s)
            var bodyLayerMask = 1 << body.gameObject.layer;
            if ((bodyLayerMask & pushLayers.value) == 0) return;

            // We dont want to push objects below us
            if (hit.moveDirection.y < -0.3f) return;

            // Calculate push direction from move direction, horizontal motion only
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

            // Apply the push and take strength into account
            body.AddForce(pushDir * strength, ForceMode.Impulse);
        }
    }
}