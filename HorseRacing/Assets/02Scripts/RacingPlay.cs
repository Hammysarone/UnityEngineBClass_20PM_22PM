using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingPlay : MonoBehaviour
{
    [SerializeField] private List<HorseMovement> horses;
    [SerializeField] private Transform goalPoint;

    public void Play()
    {
        foreach (HorseMovement horse in horses)
        {
            horse.StartMove(goalPoint.position.z - horse.transform.position.z);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
