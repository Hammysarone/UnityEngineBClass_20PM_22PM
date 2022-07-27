using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour, IComparable<TileInfo>
{
    public int index;
    new public string name;
    public string description;

    public virtual void OnTile()
    {
        Debug.Log($"여기는 {index}번째 칸인 {name}, {description}");
    }

    public int CompareTo(TileInfo other)
    {
        return index - other.index;
    }


    // 연산자 오버로딩
    public static bool operator > (TileInfo op1, TileInfo op2)
    {
        return op1.index > op2.index;
    }
    public static bool operator < (TileInfo op1, TileInfo op2)
    {
        return op1.index < op2.index;
    }
}
