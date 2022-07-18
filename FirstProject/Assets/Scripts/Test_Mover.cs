using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Mover : MonoBehaviour
{
    private Transform _transform;
    private Camera _camera;
    // Initialization �Լ���
    // �� Ŭ������ ������Ʈ�� ������ ���� ������Ʈ�� Ȱ��ȭ �Ǿ����� ���� ȣ��ȴ�.

    // �ѹ� ȣ��
    private void Awake()
    {
        // �ν��Ͻ� ��� ���� �ʱ�ȭ
        _transform = transform;

        // Test_Mover ������Ʈ�� ������ ������Ʈ�� ������ �ִ� Camera ������Ʈ�� ��ȯ�ϴ� �Լ�
        _camera = GetComponent<Camera>();
    }

    // ���� ������Ʈ�� Ȱ��ȭ �� ������ ȣ�� 
    private void OnEnable()
    {
        
    }

    // �ѹ� ȣ��
    void Start()
    {
        // �ٸ� GameObject�� ������Ʈ �ν��Ͻ� ���� �ܺ� ������ �ؼ� �ʱ�ȭ �ؾ��� ��
        // Awake()���� �� �ʱ�ȭ �� �Ŀ� ����Ǿ�� �� ��
    }

    // ���� ���� �ø��� ȣ��Ǵ� �Լ�
    // Update()���� ���� ȣ��� ���� �ִ�
    // Update()���� ���� ������ �ϸ� �ȵǴ� ������
    // ��� ���ɸ��� �ٸ��� ȣ��Ǳ� ������ �ð� �� �̵��Ÿ� / �ӵ� ��ȭ���� ������ ��ģ��.
    private void FixedUpdate()
    {
        transform.position += Vector3.up * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // ī�޶� �̵� � ���
    private void LateUpdate()
    {
        
    }

    // ������� ǥ��
    private void OnDrawGizmos()
    {

    }

    // ������Ʈ�� ���É��� ���� ǥ��
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.up, Vector3.one);
    }

    private void OnApplicationFocus(bool focus)
    {
        
    }

    private void OnApplicationPause(bool pause)
    {
        
    }

    private void OnApplicationQuit()
    {
        
    }

    // ���ӿ�����Ʈ�� ��Ȱ��ȭ�� �� ȣ��
    private void OnDisable()
    {
        
    }
    
    // ���ӿ�����Ʈ�� �ı��� �� ȣ��
    private void OnDestroy()
    {
        
    }
}
