//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using UnityEditor;
using UnityEngine;

namespace OaksMayFall.Editor.DataTableTools
{
    public sealed class DataTableGeneratorMenu
    {
        [MenuItem("GameFrameworkDataTable/Generate DataTables")]
        private static void GenerateDataTables()
        {
            // 数据表使用方法
            // 1.使用 Excel 创建数据表
            // 2.保存为 UTF - 8 的 txt 文件
            // 3.在 ProcedurePreload 中的 DataTableNames 中添加数据表名
            // 4.点击菜单栏中的生成工具，自动生成二进制文件的数据表和 DR+数据表名 类

            foreach (string dataTableName in ProcedurePreload.DataTableNames)
            {
                GameFramework.GameFrameworkLog.Debug(dataTableName);
                DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
                    break;
                }

                DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
                DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
            }

            AssetDatabase.Refresh();
        }
    }
}
