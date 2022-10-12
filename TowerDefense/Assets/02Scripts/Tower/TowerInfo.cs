using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerTypes
{
    MachineGun,
    RocketLauncher,
    LaserGun
}

[CreateAssetMenu(fileName = "TowerInfo", menuName = "TowerDefense/Create TowerInfo")]
public class TowerInfo : ScriptableObject
{
    public TowerTypes type;
    public int upgradeLevel;
    public int buildPrice;
    public int sellPrice;
    public Sprite icon;

}
