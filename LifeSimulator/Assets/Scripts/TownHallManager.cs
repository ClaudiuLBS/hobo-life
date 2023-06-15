using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownHallManager : MonoBehaviour
{
    public GameObject idCardCanvas;
    private GameObject genericCanvas;
    private PlayerMechanics player;
    public TextMeshProUGUI firstName, lastName, birthDate, gender, citizenship, cnp;
    public GameObject secondaryCamera;
    private GameObject menuManager;

    private void Start()
    {
        genericCanvas = GameObject.Find("GenericCanvas");
        player = PlayerMechanics.instance;
        menuManager = PlayerMenu.instance.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (player.idCard != null)
            return;

        if (other.CompareTag("Player")) {
            if (Input.GetKey(KeyCode.E)) {
                Cursor.lockState = CursorLockMode.None;
                idCardCanvas.SetActive(true);
                genericCanvas.SetActive(false);
                player.gameObject.SetActive(false);
                secondaryCamera.SetActive(true);
                menuManager.SetActive(false);
            }
        }
    }

    public void SubmitIdCard()
    {
        player.SetIdCard(firstName.text, lastName.text, birthDate.text, cnp.text, citizenship.text, gender.text);
        Cursor.lockState = CursorLockMode.Locked;
        idCardCanvas.SetActive(false);
        genericCanvas.SetActive(true);
        player.gameObject.SetActive(true);
        secondaryCamera.SetActive(false);
        menuManager.SetActive(true);
    }
}
