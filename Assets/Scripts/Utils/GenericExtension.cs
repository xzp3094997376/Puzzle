using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenericExtension 
{

    /// <summary>
    /// 获取数组随机N个元素
    /// </summary>
    /// <param name="array">指定数组</param>
    /// <param name="count">获取元素个数</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T[] GetRandomChilds<T>(this T[] array, int count)
    {

        T[] tempArray = new T[array.Length];
        T[] tempResultArray = new T[count];

        array.CopyTo(tempArray, 0);

        tempArray.SortRandom<T>();

        System.Array.Copy(tempArray, tempResultArray, count);

        return tempResultArray;
    }

    /// <summary>
    /// 获取列表随机N个元素
    /// </summary>
    /// <param name="list"></param>
    /// <param name="count"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> GetRandomChilds<T>(this List<T> list, int count)
    {
        List<T> tempList = new List<T>();
                                                                                                
        // tempList = list.GetRange(0,list.Count);
        tempList.AddRange(list);
        tempList.SortRandom<T>();
        return tempList.GetRange(0, count);
    }

    /// <summary>
    /// 数组的2个元素位置调换
    /// </summary>
    public static void Swap<T>(this T[] array, int index1, int index2)
    {
        T temp = array[index2];
        array[index2] = array[index1];
        array[index1] = temp;
    }
    /// <summary>
    /// 列表的2个元素位置调换
    /// </summary>
    public static void Swap<T>(this List<T> list, int index1, int index2)
    {
        T temp = list[index2];
        list[index2] = list[index1];
        list[index1] = temp;
    }

    /// <summary>
    /// 乱序排序数组
    /// </summary>
    public static T[] SortRandom<T>(this T[] array)
    {
        int randomIndex;
        for (int i = array.Length - 1; i > 0; i--)
        {
            randomIndex = UnityEngine.Random.Range(0, i);
            array.Swap(randomIndex, i);
        }
        return array;
    }
    /// <summary>
    /// 乱序排序列表
    /// </summary>
    public static List<T> SortRandom<T>(this List<T> list)
    {
        int randomIndex;
        for (int i = list.Count - 1; i > 0; i--)
        {
            randomIndex = UnityEngine.Random.Range(0, i);
            list.Swap(randomIndex, i);
        }
        return list;
    }

    /// <summary>
    /// 二维数组转换为一维
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="nu"></param>
    public static T[] ArrayTwo2One<T>(this T[,] nu)
    {
        int row, col, count=0;
        row = nu.GetLength(0);//获取行Count
        col = nu.GetLength(1);//获取列Count
        T[] nums = new T[row * col];
        for (int i = 0; i < row; i++)   
        {
            for (int j = 0; j < col; j++, count++)
            {
                nums[count] = nu[i, j];
            }
        }

        return nums;
    }
}
