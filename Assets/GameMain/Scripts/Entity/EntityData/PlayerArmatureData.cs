using System;
using UnityEngine;
using GameFramework.DataTable;

namespace OaksMayFall
{
    [Serializable]
    public class PlayerArmatureData : UEntityData
    {
        // 阵营
        [SerializeField]
        private CampType ownerCamp = CampType.Unknown;

        [SerializeField]
        private float moveSpeed = 0f;

        [SerializeField]
        private float rotationSmoothTime = 0f;

        [SerializeField]
        private float speedChangeRate = 0f;

        [SerializeField]
        private float jumpHeight = 0f;

        [SerializeField]
        private float gravity = 0f;

        [SerializeField]
        private float terminalVelocity = 0f;
        
        [SerializeField]
        private float jumpTimeout = 0f;

        [SerializeField]
        private float fallTimeout = 0f;

        [SerializeField]
        private float groundedOffset = 0f;

        [SerializeField]
        private float groundedRadius = 0f;

        [SerializeField]
        private int groundLayers = 0;
        
        [SerializeField]
        private float cameraRotSpeed = 0f;
        
        [SerializeField]
        private float topClamp = 0f;

        [SerializeField]
        private float bottomClamp = 0f;

        public PlayerArmatureData(int entityId, int typeId, CampType ownerCamp)
            : base(entityId, typeId)
        {
            IDataTable<DRPlayerArmature> dtPlayerArmature = GameEntry.DataTable.GetDataTable<DRPlayerArmature>();
            DRPlayerArmature drPlayerArmature = dtPlayerArmature.GetDataRow(TypeId);
            
            this.ownerCamp = ownerCamp;
            moveSpeed = drPlayerArmature.MoveSpeed;
            rotationSmoothTime = drPlayerArmature.RotationSmoothTime;
            speedChangeRate = drPlayerArmature.SpeedChangeRate;
            jumpHeight = drPlayerArmature.JumpHeight;
            gravity = drPlayerArmature.Gravity;
            terminalVelocity = drPlayerArmature.TerminalVelocity;
            jumpTimeout = drPlayerArmature.JumpTimeout;
            fallTimeout = drPlayerArmature.FallTimeout;
            groundedOffset = drPlayerArmature.GroundedOffset;
            groundedRadius = drPlayerArmature.GroundedRadius;
            groundLayers = drPlayerArmature.GroundLayers;
            cameraRotSpeed = drPlayerArmature.CameraRotSpeed;
            topClamp = drPlayerArmature.TopClamp;
            bottomClamp = drPlayerArmature.BottomClamp;
        }

        public CampType OwnerCamp => ownerCamp;
        
        public float MoveSpeed => moveSpeed;
        
        public float RotationSmoothTime => rotationSmoothTime;

        public float SpeedChangeRate => speedChangeRate;

        public float TerminalVelocity => terminalVelocity;
        
        public float JumpHeight => jumpHeight;

        public float Gravity => gravity;

        public float JumpTimeout => jumpTimeout;

        public float FallTimeout => fallTimeout;

        public float GroundedOffset => groundedOffset;

        public float GroundedRadius => groundedRadius;

        public int GroundLayers => groundLayers;

        public float CameraRotSpeed => cameraRotSpeed;
        
        public float TopClamp => topClamp;

        public float BottomClamp => bottomClamp;
    }
}
