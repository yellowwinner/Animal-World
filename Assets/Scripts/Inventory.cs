using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private ItemDatabase itemDatabase;
    public int slotAmount = 15;

    //Testing
    //public List<InventoryItem> slots;
    public InventoryItem[] slots;

	// Use this for initialization
	void Start () {
        itemDatabase = gameObject.GetComponent<ItemDatabase>();
        slots = new InventoryItem[slotAmount];
        //slots = new List<InventoryItem>();

        for (int i = 0; i < slotAmount; i++)
        {
            //slots.Add(new InventoryItem());
            slots[i] = new InventoryItem();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Get the InventoryItem inside the specified slot.
    /// </summary>
    /// <param name="slot">The slot.</param>
    /// <returns>The InventoryItem object inside that slot.</returns>
    public InventoryItem GetItem(int slot)
    {
        if(Mathf.Abs(slot) <= slotAmount-1)
        {
            
            return slots[slot];
        }
        return null;
    }

    /// <summary>
    /// Swap the two specified slots.
    /// </summary>
    /// <param name="slot1">The first slot.</param>
    /// <param name="slot2">The second slot.</param>
    public void Swap(int slot1, int slot2)
    {
        //if(Mathf.Abs(slot1) <= slots.Count-1 && Mathf.Abs(slot2) <= slots.Count-1)
        if (Mathf.Abs(slot1) <= slots.Length - 1 && Mathf.Abs(slot2) <= slots.Length - 1)
        {
            InventoryItem slot1Item = slots[Mathf.Abs(slot1)];
            slots[Mathf.Abs(slot1)] = slots[Mathf.Abs(slot2)];
            slots[Mathf.Abs(slot2)] = slot1Item;
        }
    }

    /// <summary>
    /// Remove the InventoryItem inside the specified slot.
    /// </summary>
    /// <param name="slot">The slot.</param>
    public void RemoveItem(int slot)
    {
        //if(Mathf.Abs(slot) <= slots.Count-1)
        if(Mathf.Abs(slot) <= slots.Length-1)
        {
            slots[Mathf.Abs(slot)] = new InventoryItem();
        }
    }
}
