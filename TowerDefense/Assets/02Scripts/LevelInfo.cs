using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "TowerDefense/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    public int lifeInit;
    public int moneyInit;
}