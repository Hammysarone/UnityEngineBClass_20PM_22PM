using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public KeyCode keyCode;
    private Transform _tr;
    public float speed;

    public void Hit(HitType hitType)
    {
        Debug.Log($"note hit ! {keyCode}, {hitType}");
        HitTypePopText.instance.HitType = hitType;
        switch (hitType)
        {
            case HitType.BAD:
                ScoringText.instance.score += Constants.SCORE_BAD;
                break;
            case HitType.MISS:
                ScoringText.instance.score += Constants.SCORE_MISS;
                break;
            case HitType.GOOD:
                ScoringText.instance.score += Constants.SCORE_GOOD;
                break;
            case HitType.GREAT:
                ScoringText.instance.score += Constants.SCORE_GREAT;
                break;
            case HitType.COOL:
                ScoringText.instance.score += Constants.SCORE_COOL;
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }

    private void Awake()
    {
        _tr = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _tr.Translate(Vector2.down * speed * Time.fixedDeltaTime);
    }
}