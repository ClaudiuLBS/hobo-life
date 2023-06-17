using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string nextScene;
    public Vector3 newPlayerPosition;
    private GameObject player;
    private void Start()
    {
        try {
            player = PlayerMechanics.instance.gameObject;
        } catch (NullReferenceException) {
            print("Door: Unable to find PlayerMechanics");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var characterController = player.GetComponent<CharacterController>();
            characterController.enabled = false;
            player.transform.position = newPlayerPosition;
            characterController.enabled = true;

            SceneManager.LoadScene(nextScene);
        }
    }
}