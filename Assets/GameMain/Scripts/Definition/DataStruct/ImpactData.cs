﻿// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/03/2022 14:58
// 最后一次修改于: 30/03/2022 15:14
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System.Runtime.InteropServices;
using MeowACT;

namespace OaksMayFall
{
    [StructLayout(LayoutKind.Auto)]
    public struct ImpactData
    {
        private readonly CampType m_Camp;
        private readonly int m_HP;
        private readonly int m_Attack;
        private readonly int m_Defense;

        public ImpactData(CampType camp, int hp, int attack, int defense)
        {
            m_Camp = camp;
            m_HP = hp;
            m_Attack = attack;
            m_Defense = defense;
        }

        public CampType Camp
        {
            get
            {
                return m_Camp;
            }
        }

        public int HP
        {
            get
            {
                return m_HP;
            }
        }

        public int Attack
        {
            get
            {
                return m_Attack;
            }
        }

        public int Defense
        {
            get
            {
                return m_Defense;
            }
        }
    }
}
