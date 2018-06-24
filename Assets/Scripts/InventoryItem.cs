using UnityEngine;
using System.Collections;

/// <summary>
/// A container for items inside the inventory.
/// </summary>
[System.Serializable]
public class InventoryItem
{
    public Item item;
    public int amount;

    public InventoryItem()
    {
        item = new Item();
        amount = -1;
    }

    public InventoryItem(Item item)
    {
        this.item = item;
        amount = 1;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public void SetAmount(int amount)
    {
        this.amount = Mathf.Abs(amount);
    }

    public int GetAmount()
    {
        return amount;
    }

    public void AddAmount(int amount)
    {
        this.amount += Mathf.Abs(amount);
    }

    public void AddOne()
    {
        amount++;
    }

    public void RemoveAmount(int amount)
    {
        if(Mathf.Abs(amount) <= this.amount)
        {
            this.amount -= amount;
        }else
        {
            this.amount = 0;
        }
    }

    public void RemoveOne()
    {
        if(amount > 0)
        {
            amount--;
        }
    }

    public string GetItemName()
    {
        return item.name;
    }

    public string GetItemType()
    {
        return item.type;
    }

    public int GetItemID()
    {
        return item.id;
    }

    public bool GetItemStackable()
    {
        return item.stackable;
    }
}
