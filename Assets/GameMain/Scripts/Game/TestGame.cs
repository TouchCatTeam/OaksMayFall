//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataTable;
using UnityEngine;

namespace OaksMayFall
{
    public class TestGame: GameBase
    {
        private float m_ElapseSeconds = 0f;

        public override GameMode GameMode
        {
            get
            {
                return GameMode.Test;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            IDataTable<DRPlayerArmature> dtPlayerArmature = GameEntry.DataTable.GetDataTable<DRPlayerArmature>();
            GameEntry.Entity.ShowPlayerArmature(new PlayerArmatureData(GameEntry.Entity.GenerateSerialId(),10000, CampType.Player)
            {
                Position = new Vector3(0f, 1f, 0f),
            });
        }
        
        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);
        }
    }
}
