using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public List<Item> items;
    private Item insideItem = null;

    private void Start()
    {
        Refill();
        DayNightCycle.instance.OnDayPass += Refill;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (!collider.CompareTag("Player")) return;
        if (Input.GetKeyDown(KeyCode.E)) {
            if (insideItem == null)
                InfoHandler.instance.setInfo("Nothing here boy");
            else
            {
                bool addedToInventory = PlayerMechanics.instance.AddItemToInventory(insideItem);
                if (addedToInventory)
                {
                    InfoHandler.instance.setInfo($"Found {insideItem.title} boyyy");
                    insideItem = null;
                } else
                {
                    InfoHandler.instance.setInfo($"Found {insideItem.title} but u don't have space in inventory");
                }

            }
        }
    }
    
    private void Refill()
    {
        if (insideItem == null)
            insideItem = items[Random.Range(0, items.Count)];
        print("Bin Refilled");
    }
}
