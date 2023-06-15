using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHallManager : MonoBehaviour
{
    public GameObject canvas;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (Input.GetKey(KeyCode.E)) { 
                canvas.SetActive(true);
            }
        }
    }
}
