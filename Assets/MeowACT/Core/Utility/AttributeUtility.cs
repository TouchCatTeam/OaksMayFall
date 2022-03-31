// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 30/03/2022 21:57
// 最后一次修改于: 31/03/2022 8:42
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;

namespace MeowACT
{
    public static class AttributeUtility
    {
        public static IList<ValueDropdownItem<string>> GetFieldNames(UnityEngine.Object UObject)
        {
            if (UObject == null)
                return null;
            
            ValueDropdownList<string> valueDropdownItems = new ValueDropdownList<string>();
            FieldInfo[] filedInfos = UObject.GetType().GetFields(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static);
            foreach (var field in filedInfos)
            {
                valueDropdownItems.Add(field.Name);
            }
            
            return valueDropdownItems;
        }
        
        public static FieldInfo GetFieldInfoByName(UnityEngine.Object UObject, string name)
        {
            if (UObject == null)
                return null;
            
            ValueDropdownList<string> valueDropdownItems = new ValueDropdownList<string>();
            FieldInfo[] filedInfos = UObject.GetType().GetFields(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static);
            foreach (var field in filedInfos)
            {
                if (field.Name == name)
                    return field;
            }
            
            return null;
        }
        
        public static Type GetFieldTypeByName(UnityEngine.Object UObject, string name)
        {
            if (UObject == null)
                return null;
            
            ValueDropdownList<string> valueDropdownItems = new ValueDropdownList<string>();
            FieldInfo[] filedInfos = UObject.GetType().GetFields(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static);
            foreach (var field in filedInfos)
            {
                if (field.Name == name)
                    return field.FieldType;
            }
            
            return null;
        }

        /// <summary>
        /// 将字符串转化为指定类型
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="str">需要转换的字符串</param>
        /// <returns></returns>
        public static T ChangeTo<T>(string str)
        {
            T result = default(T);
            result = (T)Convert.ChangeType(str, typeof(T));
            return result;
        }

    }
}