using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseMovement : MonoBehaviour
{
    [SerializeField] private float minSpeed = 3.0f;
    [SerializeField] private float maxSpeed = 7.0f;
    private float _ranDistance;
    private float _targetDistance;
    private bool _doMove;

    public bool isFinished
    {
        get
        {
            return _ranDistance >= _targetDistance;
        }
    }

    public void StartMove(float targetDistance)
    {
        _doMove = true;
        _targetDistance = targetDistance;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_doMove && _ranDistance < _targetDistance)
            Move();
    }

    void Move()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        Vector3 moveVec = Vector3.forward * speed * Time.fixedDeltaTime;
        transform.Translate(moveVec);
        _ranDistance += moveVec.z;
    }
}
