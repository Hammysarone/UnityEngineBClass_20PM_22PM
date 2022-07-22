using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(bulletPrefab, firePoint); // ������ ���ӿ�����Ʈ�� �ش� Transform�� ���� ��Ŵ\
            Instantiate(bulletPrefab, firePoint.position , firePoint.rotation);
        }
    }
}
