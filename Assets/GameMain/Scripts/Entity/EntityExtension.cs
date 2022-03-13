//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;
using System;
using UnityGameFramework.Runtime;

namespace OaksMayFall
{
    public static class EntityExtension
    {
        // 关于 EntityId 的约定：
        // 0 为无效
        // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
        // 负值用于本地生成的临时实体（如特效、FakeObject等）
        private static int s_SerialId = 0;

        // 不能从 'UnityGameFramework.Runtime.EntityLogic' 类型转换到 'UnityGameFramework.Runtime.Entity'
        //public static Entity GetGameEntity(this EntityComponent entityComponent, int entityId)
        //{
        //    UnityGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
        //    if (entity == null)
        //    {
        //        return null;
        //    }

        //    return (Entity)entity.Logic;
        //}

        public static void HideEntity(this EntityComponent entityComponent, UEntity entity)
        {
            entityComponent.HideEntity(entity.Entity);
        }

        public static void AttachEntity(this EntityComponent entityComponent, UEntity entity, int ownerId, string parentTransformPath = null, object userData = null)
        {
            entityComponent.AttachEntity(entity.Entity, ownerId, parentTransformPath, userData);
        }

        //public static void ShowBullet(this EntityComponent entityCompoennt, BulletData data)
        //{
        //    entityCompoennt.ShowEntity(typeof(Bullet), "Bullet", Constant.AssetPriority.BulletAsset, data);
        //}

        //public static void ShowEffect(this EntityComponent entityComponent, EffectData data)
        //{
        //    entityComponent.ShowEntity(typeof(Effect), "Effect", Constant.AssetPriority.EffectAsset, data);
        //}

        private static void ShowEntity(this EntityComponent entityComponent, Type logicType, string entityGroup, int priority, UEntityData data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            IDataTable<DRUEntity> dtUEntity = GameEntry.DataTable.GetDataTable<DRUEntity>();
            DRUEntity drUEntity = dtUEntity.GetDataRow(data.TypeId);
            if (drUEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
                return;
            }

            entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(drUEntity.AssetName), entityGroup, priority, data);
        }

        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return --s_SerialId;
        }
    }
}
