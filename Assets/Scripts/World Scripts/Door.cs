using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Item used to open door
    [SerializeField] private Collectable RequiredItem;
    private Item requiredItem;

    [SerializeField] private BoxCollider2D PhysicalCollider;

    private bool DoorOpened;

    private void Awake()
    {
        requiredItem = RequiredItem._thisItem;
    }

    private bool SearchForKey(GameObject player)
    {
        Inventory inv = player.GetComponent<Inventory>();
        bool hasKey = false;

        foreach (var kvp in inv._inventory)
        {
            if (kvp.Value == requiredItem)
            {
                hasKey = true;
                inv._inventory.Remove(kvp.Key);
                break;
            }
        }

        return hasKey;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool hasKey = SearchForKey(collision.gameObject);

            if (hasKey && !DoorOpened)
            {
                PhysicalCollider.enabled = false;
            }
        }        
    }
}
