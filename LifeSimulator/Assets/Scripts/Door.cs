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
        player = PlayerMechanics.instance.gameObject;
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