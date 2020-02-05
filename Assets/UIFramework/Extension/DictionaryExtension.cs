using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对Dictory的扩展
/// </summary>
public static class DictionaryExtension
{
    /// <summary>
    /// 尝试根据key得到value，得到了直接返回value，没有直接返回null
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    /// <typeparam name="Tvalue"></typeparam>
    /// <param name="dict"></param>
    /// <param name=""></param>
    public static Tvalue TryGet<Tkey,Tvalue>(this Dictionary<Tkey,Tvalue> dict, Tkey key)
    {
        Tvalue value;
        dict.TryGetValue(key, out value);
        return value;
    }
}
