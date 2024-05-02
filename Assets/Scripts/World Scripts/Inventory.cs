using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, Item> _inventory;

    public Collectable InitialNote;

    private void Awake()
    {
        _inventory = new Dictionary<string, Item>();
        _inventory.Add(InitialNote.name, InitialNote._thisItem);
    }

    public void AddItem(Item newItm)
    {
        if (!_inventory.ContainsKey(newItm.Name))
        {
            _inventory.Add(newItm.Name, newItm);
            Debug.Log("Collected " + newItm.Name);
        }        
    }

    public bool ContainsItem(string itemName)
    {
        return _inventory.ContainsKey(itemName);
    }
}
