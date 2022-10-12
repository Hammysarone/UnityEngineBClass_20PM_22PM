using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;


    private Dictionary<object, Dictionary<IBuff<object>, Coroutine>> _buffs 
        = new Dictionary<object, Dictionary<IBuff<object>, Coroutine>>();

    private Coroutine _tmpCoroutine;

    public void ActiveBuff<T>(T target, IBuff<T> buff, float duration) where T : Object
    {
        if(_buffs.ContainsKey(target) == false)
        {
            _buffs.Add(target, new Dictionary<IBuff<object>, Coroutine>());
        }

        if (_buffs[target].ContainsKey((IBuff<object>)buff))
        {
            StopCoroutine(_buffs[target][(IBuff<object>)buff]);
            _buffs[target].Remove((IBuff<object>)buff);
        }
        _tmpCoroutine = StartCoroutine(E_ActiveBuff<T>(target, buff, duration));
        _buffs[target].Add((IBuff<object>)buff, _tmpCoroutine);
        Debug.Log($"BuffManager : 버프 {buff}가 {target}에게 활성화 됨");
        
    }

    public void DeactiveAllBuff<T>(T target)
    {
        // 걸려있는 버프가 있는지 체크
        if (_buffs.ContainsKey(target) == false)
            return;

        // 모든 버프 중지
        foreach (Coroutine buffCoroutine in _buffs[target].Values)
            StopCoroutine(buffCoroutine);

        _buffs.Remove(target);
        Debug.Log($"BuffManager : {target}의 모든 버프가 비활성화 됨");
    }

    private IEnumerator E_ActiveBuff<T>(T target, IBuff<T> buff, float duration)
    {
        buff.OnActive(target);

        float timer = duration;
        while(timer > 0.0f)
        {
            buff.OnDuration(target);
            timer -= Time.deltaTime;
            yield return null;
        }

        buff.OnDeactivate(target);

        _buffs[target][(IBuff<object>)buff] = null;
        _buffs[target].Remove((IBuff<object>)buff);
    }

    private void Awake()
    {
        instance = this;
    }
}