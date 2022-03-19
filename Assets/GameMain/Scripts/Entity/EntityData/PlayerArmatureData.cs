using System;
using UnityEngine;
using GameFramework.DataTable;

namespace OaksMayFall
{
    [Serializable]
    public class PlayerArmatureData : UEntityData
    {
        /// <summary>
        /// 阵营
        /// </summary>
        [SerializeField] private CampType ownerCamp = CampType.Unknown;
        /// <summary>
        /// 生命最大值
        /// </summary>
        [SerializeField] private float maxHP;
        public PlayerArmatureData(int entityId, int typeId, CampType ownerCamp)
            : base(entityId, typeId)
        {
            IDataTable<DRPlayerArmature> dtPlayerArmature = GameEntry.DataTable.GetDataTable<DRPlayerArmature>();
            DRPlayerArmature drPlayerArmature = dtPlayerArmature.GetDataRow(TypeId);
            
            this.ownerCamp = ownerCamp;
            maxHP = drPlayerArmature.MaxHP;
        }

        /// <summary>
        /// 阵营
        /// </summary>
        public CampType OwnerCamp => ownerCamp;
        /// <summary>
        /// 生命最大值
        /// </summary>
        public float MaxHP => maxHP;
    }
}
