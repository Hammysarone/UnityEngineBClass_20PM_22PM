using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera playerFollowingCam;

    private void Awake()
    {
        mainCam.enabled = true;
        playerFollowingCam.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCam();
        }
    }

    private void SwitchCam()
    {
        playerFollowingCam.enabled = !playerFollowingCam.enabled;
        mainCam.enabled = !mainCam.enabled;
    }
}
