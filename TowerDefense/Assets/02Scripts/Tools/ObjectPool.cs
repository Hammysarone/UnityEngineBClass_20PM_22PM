using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    public static ObjectPool instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<ObjectPool>("Assets/ObjectPool"));
            return _instance;
        }
    }

    private List<PoolElement> poolElements = new List<PoolElement>();
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private Dictionary<string, Queue<GameObject>> spawnedQueuePairs = new Dictionary<string, Queue<GameObject>>();

    //===============================================================
    //************************ Public Methods ***********************
    //===============================================================

    /// <summary>
    /// 필요한 객체 발주
    /// </summary>
    /// <param name="poolElement"></param>
    public void AddPoolElement(PoolElement poolElement)
        => poolElements.Add(poolElement);

    /// <summary>
    /// 발주된 객체 모두 생성
    /// </summary>
    public void InstantiateAllPoolElements()
    {
        foreach (PoolElement poolElement in poolElements)
        {
            if(spawnedQueuePairs.ContainsKey(poolElement.name) == false)
                spawnedQueuePairs.Add(poolElement.name, new Queue<GameObject>());

            for (int i = 0; i < poolElement.num; i++)
            {
                InstantiatePoolElement(poolElement);
            }
        }
    }

    /// <summary>
    /// 발주된 객체 빌리기
    /// </summary>
    /// <param name="name">빌릴 객체 이름</param>
    /// <param name="spawnPoint">빌려간 객체 소환 위치</param>
    /// <returns></returns>
    public GameObject Spawn(string name, Vector3 spawnPoint)
    {
        if (spawnedQueuePairs.ContainsKey(name) == false)
            return null;

        // 생성해놨던 객체들을 모두 다 빌려줬을때 새로 생성함
        if (spawnedQueuePairs[name].Count <= 0)
        {
            PoolElement poolElement = poolElements.Find(element => element.name == name);
            if (poolElement != null)
            {
                // 원래 소환 갯수에 비례해서 미리 많이 생성
                for (int i = 0; i < Math.Ceiling(Math.Log10(poolElement.num)); i++)
                {
                    InstantiatePoolElement(poolElement);
                }
            }
        }

        GameObject go = spawnedQueuePairs[name].Dequeue();
        go.transform.position = spawnPoint;
        go.transform.SetParent(null);
        go.SetActive(true);
        return go;
    }

    /// <summary>
    /// 빌려간 객체 되돌려놓기
    /// </summary>
    /// <param name="obj"></param>
    public void Return(GameObject obj)
    {
        if (spawnedQueuePairs.ContainsKey(obj.name) == false)
        {
            Debug.LogError($"[ObjectPool] : {obj.name}을 ObjectPool에 반환할 수 없음");
            return;
        }

        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        spawnedQueuePairs[obj.name].Enqueue(obj);
        obj.SetActive(false);
        RearrangeSiblings(obj);
    }

    //===============================================================
    //************************ Private Methods **********************
    //===============================================================

    private void Awake()
    {
        transform.position = new Vector3(5000, 5000, 5000);
    }
    private GameObject InstantiatePoolElement(PoolElement poolElement)
    {
        GameObject go = Instantiate(poolElement.prefab, transform);
        go.name = poolElement.name;
        spawnedQueuePairs[poolElement.name].Enqueue(go);
        go.SetActive(false);
        RearrangeSiblings(go);
        return go;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj">정렬하고 싶은 자식</param>
    private void RearrangeSiblings(GameObject obj)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name == obj.name)
            {
                obj.transform.SetSiblingIndex(i);
                return;
            }
        }

        obj.transform.SetAsLastSibling();
    }
}
