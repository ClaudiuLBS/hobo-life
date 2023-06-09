using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public List<Item> items;
    private Item insideItem = null;

    //cosului i se face refill o data pe zi
    private void Start()
    {
        Refill();
        DayNightCycle.instance.OnDayPass += Refill;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (!collider.CompareTag("Player")) return;
        //la apasarea tastei E se verifica daca exista obiect in trash
        if (Input.GetKeyDown(KeyCode.E)) {
            if (insideItem == null)
                InfoHandler.instance.SetInfo("Nothing here boy");
            else
            {
                //adauga obiectul in inventar
                bool addedToInventory = PlayerMechanics.instance.AddItemToInventory(insideItem);
                if (addedToInventory)
                {
                    InfoHandler.instance.SetInfo($"Found {insideItem.title} boyyy");
                    insideItem = null;
                } else
                {
                    InfoHandler.instance.SetInfo($"Found {insideItem.title} but u don't have space in inventory");

                }

            }
        }
    }
    
    //umplerea cosului cu un item ales random
    private void Refill()
    {
        if (insideItem == null)
            insideItem = items[Random.Range(0, items.Count)];
        print("Bin Refilled");
    }
}
