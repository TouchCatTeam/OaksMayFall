#if UNITY_5_4_OR_NEWER
using ParadoxNotion.Design;
using System.Collections.Generic;
using UnityEngine;

namespace FlowCanvas.Nodes
{

    [Name("Particle Collision 2D")]
    [Category("Events/Object")]
    [Description("Called when any Particle System collided with the target collider 2D object")]
    public class ParticleCollision2DEvents : RouterEventNode<Collider2D>
    {

        private FlowOutput onCollision;
        private Collider2D receiver;
        private ParticleSystem particle;
        private List<ParticleCollisionEvent> collisionEvents;

        protected override void RegisterPorts() {
            onCollision = AddFlowOutput("On Particle Collision");
            AddValueOutput<Collider2D>("Receiver", () => { return receiver; });
            AddValueOutput<ParticleSystem>("Particle System", () => { return particle; });
            AddValueOutput<Vector3>("Collision Point", () => { return collisionEvents[0].intersection; });
            AddValueOutput<Vector3>("Collision Normal", () => { return collisionEvents[0].normal; });
            AddValueOutput<Vector3>("Collision Velocity", () => { return collisionEvents[0].velocity; });
        }

        protected override void Subscribe(ParadoxNotion.Services.EventRouter router) {
            router.onParticleCollision += OnParticleCollision;
        }

        protected override void UnSubscribe(ParadoxNotion.Services.EventRouter router) {
            router.onParticleCollision -= OnParticleCollision;
        }

        void OnParticleCollision(ParadoxNotion.EventData<GameObject> msg) {
            this.receiver = ResolveReceiver(msg.receiver);
            this.particle = msg.value.GetComponent<ParticleSystem>();
            this.collisionEvents = new List<ParticleCollisionEvent>();
            if ( particle != null ) { particle.GetCollisionEvents(receiver.gameObject, collisionEvents); }
            onCollision.Call(new Flow());
        }
    }
}
#endif