using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingPlay : MonoBehaviour
{
    public static RacingPlay instance;


    private bool isPlaying = false;
    private int totalHorseCount;
    [SerializeField] private List<HorseMovement> horses;
    [SerializeField] private Transform goalPoint;
    [SerializeField] private Transform platform1GradePoint;
    [SerializeField] private Transform platform2GradePoint;
    [SerializeField] private Transform platform3GradePoint;

    private List<Transform> horsesFinished = new List<Transform>();

    public void Play()
    {
        foreach (HorseMovement horse in horses)
        {
            horse.StartMove(goalPoint.position.z - horse.transform.position.z);
        }
        isPlaying = true;
    }

    public List<Transform> GetHorseTransforms()
    {
        List<Transform> tmpList = new List<Transform>();

        foreach (var horse in horses)
            tmpList.Add(horse.transform);
        return tmpList;
    }

    private void Awake()
    {
        instance = this;
        totalHorseCount = horses.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
                
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying == false)
            return;
        for (int i = horses.Count - 1; i > -1; i--)
        {
            if (horses[i].isFinished == true)
            {
                horsesFinished.Add(horses[i].transform);
                horses.Remove(horses[i]);
            }
        }

        if (horsesFinished.Count == totalHorseCount)
        {
            OnGameFinish();
        }

    }

    private void OnGameFinish()
    {
        horsesFinished[0].position = platform1GradePoint.position;
        horsesFinished[1].position = platform2GradePoint.position;
        horsesFinished[2].position = platform3GradePoint.position;

        isPlaying = false;
    }
}
