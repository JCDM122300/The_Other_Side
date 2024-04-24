using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Item _thisItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<Inventory>(out Inventory inv))
            {
                inv.AddItem(_thisItem);
            }

            Destroy(gameObject);
        }
    }
}
