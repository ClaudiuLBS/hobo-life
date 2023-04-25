using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    public static PlayerMenu instance;
    public GameObject itemUI;
    public Transform inventory;
    public GameObject playerMenuUI;
    // Start is called before the first frame update
    void Awake() {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyUp(KeyCode.Tab)) {
            playerMenuUI.SetActive(!playerMenuUI.activeSelf);
            if (playerMenuUI.activeSelf)
               UpdateInventory();
        }
    }

    public void UpdateInventory()
    {
        foreach (Transform t in inventory)
            Destroy(t.gameObject);

        for (int i = 0; i < PlayerMechanics.instance.inventory.Count; i++)
        {
            Item item = PlayerMechanics.instance.inventory[i];
            GameObject obj = Instantiate(itemUI, inventory);
            obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.title;
            obj.transform.GetChild(1).GetComponent<Image>().sprite = item.icon;
            var itemInventory = obj.GetComponent<InventoryItem>();
            itemInventory.index = i;
        }
    }
}
