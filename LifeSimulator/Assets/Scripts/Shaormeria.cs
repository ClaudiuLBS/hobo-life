using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shaormeria : MonoBehaviour
{
    public static Shaormeria instance;
    private PlayerMechanics player;
    private PlayerMenu playerMenu;
    private List<string> currentShaorma = new List<string>();
    private Dictionary<string, int> orderedShaorma = new Dictionary<string, int>();
    
    public GameObject orderDisplay;
    public GameObject shaormeriaCanvas;
    public GameObject shaormeriaCamera;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        player = PlayerMechanics.instance;
        playerMenu = PlayerMenu.instance;
        TakeOrder();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Enter();
        }
    }

    public void Enter()
    {
        ToggleShaormeria(true);
    }

    public void TakeOrder()
    {
        currentShaorma = new List<string>();
        var ingredientsCount = transform.childCount;
        for (int i = 0; i < ingredientsCount; i++)
        {
            var ingredient = transform.GetChild(i).name;
            orderedShaorma[ingredient] = Random.Range(0, 4);
            orderDisplay.transform.Find(ingredient).GetComponent<TextMeshProUGUI>().text = $"{orderedShaorma[ingredient]}x {ingredient}";
        }
    }

    public void AddIngredientToShaorma(string ingredient)
    {
        if (!currentShaorma.Contains(ingredient) || currentShaorma.Where(x => x == ingredient).Count() <= 3)
        {
            currentShaorma.Add(ingredient);
            InfoHandler.instance.SetInfo($"Added {ingredient}");
        }
        else
        {
            InfoHandler.instance.SetInfo($"Can't add any more {ingredient}");
        }
    }

    public void DeliverShaorma()
    {
        int tips = 10;
        var ingredientsCount = transform.childCount;
        for (int i = 0; i < ingredientsCount; i++)
        {
            var ingredient = transform.GetChild(i).name;
            var ingredientCount = currentShaorma.Where(x => x == ingredient).Count();

            if (orderedShaorma[ingredient] != ingredientCount)
                tips -= 2;
        }
        player.AddMoney(tips);
        InfoHandler.instance.SetInfo($"Earned {tips}$");
        TakeOrder();
    }

    public void Leave()
    {
        ToggleShaormeria(false);
        var characterController = player.GetComponent<CharacterController>();
        characterController.enabled = false;
        player.transform.position = new Vector3(-10, 0.5f, 5);
        characterController.enabled = true;
    }

    private void ToggleShaormeria(bool value)
    {
        player.gameObject.SetActive(!value);
        playerMenu.setActive(!value);
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        shaormeriaCanvas.SetActive(value);
        shaormeriaCamera.SetActive(value);
    }
}
