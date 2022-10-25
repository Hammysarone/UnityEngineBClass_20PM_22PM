using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool isDetected;

    [SerializeField] private LayerMask _groundLayer;

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & _groundLayer) > 0)
            isDetected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & _groundLayer) > 0)
            isDetected = false;
    }
}