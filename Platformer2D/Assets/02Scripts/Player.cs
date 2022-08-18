using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool invincible { get; private set; }
    private Coroutine _invincibleCoroutine = null;

    public int _damage;
    private int _hp;
    public int hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value < 0)
                value = 0;

            _hpBar.value = (float)value / _hpMax;
            _hp = value;
        }
    }

    [SerializeField] private Slider _hpBar;
    [SerializeField] private int _hpMax;
    private PlayerController _controller;

    public void Hurt(int damage)
    {
        hp -= damage;
        if (hp > 0)
            _controller.TryHurt();
        else
            _controller.TryDie();
    }

    public void InvincibleForSeconds(float second)
    {
        if (_invincibleCoroutine != null) StopCoroutine(_invincibleCoroutine);
        _invincibleCoroutine = StartCoroutine(E_InvincibleForSeconds(second));
    }

    IEnumerator E_InvincibleForSeconds(float second)
    {
        invincible = true;

        yield return new WaitForSeconds(second);
        invincible = false;
    }

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        hp = _hpMax;
    }
}
