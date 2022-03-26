// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 25/03/2022 16:34
// 最后一次修改于: 26/03/2022 15:34
// 版权所有: CheapMiaoStudio
// 描述:
// ----------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace MeowACT
{
    public class RList<T>
    {
        public ListRemovableInForEach<T> collection = new ListRemovableInForEach<T>();
        
        public IEnumerator GetEnumerator()
        {
            return collection;
        }
        public void Remove(T t)
        {
            collection.Remove(t);
        }
    
        public void Add(T t)
        {
            collection.Add(t);
        }
    }
    public class ListRemovableInForEach<T> : IEnumerator
    {
        public List<T> list = new List<T>();
        public object current = null;
        public object Current
        {
            get { return current; }
        }
    
        private int icout = 0;
        public bool MoveNext()
        {
            if (icout >= list.Count)
            {
                Reset();
                return false;
            }
            else
            {
                current = list[icout];
                icout++;
                return true;
            }
        }
    
        public void Reset()
        {
            icout = 0;
        }
    
        public void Add(T t)
        {
            list.Add(t);
        }
    
        public void Remove(T t)
        {
            if (list.Contains(t))
            {
                if (list.IndexOf(t) <= icout)
                {
                    icout--;
                }
                list.Remove(t);
            }
        }
    }
}
