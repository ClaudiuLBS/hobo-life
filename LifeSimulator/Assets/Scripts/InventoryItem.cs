using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public int index;
    public void Use()
    {
        Item item = PlayerMechanics.instance.inventory[index];
        item.Use();
        PlayerMechanics.instance.inventory.RemoveAt(index);
        PlayerMenu.instance.UpdateInventory();
    }
    public void Remove()
    {
        PlayerMechanics.instance.inventory.RemoveAt(index);
        PlayerMenu.instance.UpdateInventory();
    }
}
