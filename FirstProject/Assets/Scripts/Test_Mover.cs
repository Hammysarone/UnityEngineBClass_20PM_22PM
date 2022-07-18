using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Mover : MonoBehaviour
{
    private Transform _transform;
    private Camera _camera;
    // Initialization 함수들
    // 본 클래스를 컴포넌트로 가지는 게임 오브젝트가 활성화 되어있을 때만 호출된다.

    // 한번 호출
    private void Awake()
    {
        // 인스턴스 멤버 변수 초기화
        _transform = transform;

        // Test_Mover 컴포넌트를 가지는 오브젝트가 가지고 있는 Camera 컴포넌트를 반환하는 함수
        _camera = GetComponent<Camera>();
    }

    // 게임 오브젝트가 활성화 될 때마다 호출 
    private void OnEnable()
    {
        
    }

    // 한번 호출
    void Start()
    {
        // 다른 GameObject의 컴포넌트 인스턴스 등의 외부 참조를 해서 초기화 해야할 때
        // Awake()에서 다 초기화 된 후에 실행되어야 할 때
    }

    // 물리 연산 시마다 호출되는 함수
    // Update()보다 많이 호출될 수도 있다
    // Update()에서 물리 연산을 하면 안되는 이유는
    // 기기 성능마다 다르게 호출되기 때문에 시간 당 이동거리 / 속도 변화량에 영향을 끼친다.
    private void FixedUpdate()
    {
        transform.position += Vector3.up * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 카메라 이동 등에 사용
    private void LateUpdate()
    {
        
    }

    // 어느때나 표시
    private void OnDrawGizmos()
    {

    }

    // 오브젝트가 선택됬을 때만 표시
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

    // 게임오브젝트가 비활성화될 때 호출
    private void OnDisable()
    {
        
    }
    
    // 게임오브젝트가 파괴될 때 호출
    private void OnDestroy()
    {
        
    }
}
