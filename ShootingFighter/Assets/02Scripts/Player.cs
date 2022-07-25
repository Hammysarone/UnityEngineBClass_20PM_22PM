using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    private float _hp;

    public float hpMax;
    [SerializeField] private Slider hpBar;

   public float hp
    {
        set
        {
            if(value < 0)
                value = 0;
            
            _hp = value;
            hpBar.value = _hp / hpMax;
            if (_hp <= 0)
                GameManager.instance.GameOver();
        }
        get { return _hp; }
    }

    private void Awake()
    {
        instance = this;
        hp = hpMax;
    }
    
    public void RecoverHP()
    {
        hp = hpMax;
    }
}
