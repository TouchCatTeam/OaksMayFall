//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using UnityEngine;

namespace OaksMayFall
{
    [Serializable]
    public abstract class UEntityData    {
        [SerializeField]
        private int id = 0;

        [SerializeField]
        private int typeId = 0;

        [SerializeField]
        private Vector3 position = Vector3.zero;

        [SerializeField]
        private Quaternion rotation = Quaternion.identity;

        public UEntityData(int entityId, int typeId)
        {
            id = entityId;
            this.typeId = typeId;
        }

        /// <summary>
        /// 实体编号。
        /// </summary>
        public int Id => id;

        /// <summary>
        /// 实体类型编号。
        /// </summary>
        public int TypeId => typeId;

        /// <summary>
        /// 实体位置。
        /// </summary>
        public Vector3 Position
        {
            get => position;
            set => position = value;
        }

        /// <summary>
        /// 实体朝向。
        /// </summary>
        public Quaternion Rotation
        {
            get => rotation;
            set => rotation = value;
        }
    }
}
