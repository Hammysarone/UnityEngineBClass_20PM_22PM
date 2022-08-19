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
    private CapsuleCollider2D _col;

    public void Hurt(int damage)
    {
        if (invincible)
            return;

        hp -= damage;
        DamagePopUp.Create(transform.position + Vector3.up * _col.size.y * 0.7f, damage, gameObject.layer);
        if (hp > 0)
        {
            _controller.TryHurt();
            InvincibleForSeconds(1.0f);
        }
        else
        {
            _controller.TryDie();
            invincible = true;
        }
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
        _col = GetComponent<CapsuleCollider2D>();
        hp = _hpMax;
    }
}
