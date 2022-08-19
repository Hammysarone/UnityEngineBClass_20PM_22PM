using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private TMP_Text _textMeshPro;
    private float _disapearTimer = 0.5f;
    private float _disapearSpeed = 2.0f;
    private float _moveSpeedY = 0.5f;
    private Color _color;

    public static DamagePopUp Create(Vector3 pos, int damage, int layer)
    {
        DamagePopUp tmpDamagePopUp = Instantiate(DamagePopUpAssets.instance.GetDamagePopUp(layer), pos, Quaternion.identity);
        tmpDamagePopUp.Setup(damage);
        return tmpDamagePopUp;
    }

    private void Awake()
    {
        _textMeshPro = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        transform.position += Vector3.up * _moveSpeedY * Time.deltaTime;

        if(_disapearTimer < 0.0f)
        {
            _color.a -= _disapearSpeed * Time.deltaTime;
            _textMeshPro.color = _color;
            if(_color.a <= 0.0f)
                Destroy(gameObject);
        }
        else
        {
            _disapearSpeed -= Time.deltaTime;
        }
    }

    private void Setup(int damage)
    {
        _textMeshPro.SetText(damage.ToString());
    }
}
