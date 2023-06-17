using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickableItem: MonoBehaviour
{
    public Item item;

    public void Start() {
        SphereCollider trigger = this.AddComponent<SphereCollider>();
        trigger.isTrigger = true;
        trigger.radius = 2.3f;
    }

    private void OnTriggerStay(Collider collider)
    {
        // verific daca playerul este cel care a declansat triggerul 
        if (!collider.CompareTag("Player")) return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool pickedItem = PlayerMechanics.instance.AddItemToInventory(item);
            if (pickedItem) Destroy(gameObject);
            else InfoHandler.instance.SetInfo("Not enough space");
        }
    }
}
