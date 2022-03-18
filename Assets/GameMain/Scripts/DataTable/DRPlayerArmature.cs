//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-03-18 11:23:42.145
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace OaksMayFall
{
    /// <summary>
    /// 主角表。
    /// </summary>
    public class DRPlayerArmature : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取主角编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取移动速度。
        /// </summary>
        public float MoveSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取转身速度。
        /// </summary>
        public float RotationSmoothTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取运动加速度。
        /// </summary>
        public float SpeedChangeRate
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取跳跃高度。
        /// </summary>
        public float JumpHeight
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取重力系数。
        /// </summary>
        public float Gravity
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取两次跳跃之间的间隔。
        /// </summary>
        public float JumpTimeout
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取起跳到下落之间的间隔。
        /// </summary>
        public float FallTimeout
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取落地球形碰撞检测中心点的竖向偏移量。
        /// </summary>
        public float GroundedOffset
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取落地球形碰撞检测的半径。
        /// </summary>
        public float GroundedRadius
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取落地球形碰撞检测的层级。
        /// </summary>
        public int GroundLayers
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取摄像机转动的速度。
        /// </summary>
        public float CameraRotSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取摄像机最大俯仰角。
        /// </summary>
        public float TopClamp
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取摄像机最小俯仰角。
        /// </summary>
        public float BottomClamp
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            MoveSpeed = float.Parse(columnStrings[index++]);
            RotationSmoothTime = float.Parse(columnStrings[index++]);
            SpeedChangeRate = float.Parse(columnStrings[index++]);
            JumpHeight = float.Parse(columnStrings[index++]);
            Gravity = float.Parse(columnStrings[index++]);
            JumpTimeout = float.Parse(columnStrings[index++]);
            FallTimeout = float.Parse(columnStrings[index++]);
            GroundedOffset = float.Parse(columnStrings[index++]);
            GroundedRadius = float.Parse(columnStrings[index++]);
            GroundLayers = int.Parse(columnStrings[index++]);
            CameraRotSpeed = float.Parse(columnStrings[index++]);
            TopClamp = float.Parse(columnStrings[index++]);
            BottomClamp = float.Parse(columnStrings[index++]);

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    MoveSpeed = binaryReader.ReadSingle();
                    RotationSmoothTime = binaryReader.ReadSingle();
                    SpeedChangeRate = binaryReader.ReadSingle();
                    JumpHeight = binaryReader.ReadSingle();
                    Gravity = binaryReader.ReadSingle();
                    JumpTimeout = binaryReader.ReadSingle();
                    FallTimeout = binaryReader.ReadSingle();
                    GroundedOffset = binaryReader.ReadSingle();
                    GroundedRadius = binaryReader.ReadSingle();
                    GroundLayers = binaryReader.Read7BitEncodedInt32();
                    CameraRotSpeed = binaryReader.ReadSingle();
                    TopClamp = binaryReader.ReadSingle();
                    BottomClamp = binaryReader.ReadSingle();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
