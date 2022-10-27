using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHpPotionL : Item
{
    public override void OnEarn()
    {
        Player.instance.hp += 3;
    }
}