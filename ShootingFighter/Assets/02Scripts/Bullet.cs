using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    Transform _tr;

    private void Awake()
    {
        _tr = transform;
    }

    private void FixedUpdate()
    {
        Vector3 deltaMove = Vector3.forward * moveSpeed * Time.fixedDeltaTime;
        _tr.Translate(deltaMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
            Destroy(gameObject);
        else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<Enemy>().hp -= damage;
            Destroy(gameObject);
        }
    }
}
