using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public int index;
    //metoda de folosire a unui item
    public void Use()
    {
        //get item la index specificat
        Item item = PlayerMechanics.instance.inventory[index];
        item.Use();
        //eliminarea obiectului
        PlayerMechanics.instance.inventory.RemoveAt(index);
        //update inventar
        PlayerMenu.instance.UpdateInventory();
    }

    //metoda de eliminare obiect
    public void Remove()
    {
        PlayerMechanics.instance.inventory.RemoveAt(index);
        PlayerMenu.instance.UpdateInventory();
    }
}
