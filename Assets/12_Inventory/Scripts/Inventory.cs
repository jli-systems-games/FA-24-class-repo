using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static List<InventoryItem> inventory = new List<InventoryItem>();
    public List<Item> uiItems = new List<Item>();

    #region Constraints
    public int inventoryMax;
    //public float inventoryWeightMax;

    #endregion

    private int _amountTemp = 1;
    public void AddViaUI(string itemToAdd)
    {
        foreach(Item uiItem in uiItems)
        {
            if(uiItem.itemName == itemToAdd)
            {
                Add(uiItem, _amountTemp);
            }
        }
    }

    public void AddAmountViaUI(int amountToAdd)
    {
        _amountTemp = amountToAdd;
    }

    public void DisplayInventory()
    {

    }

    public void Add(Item newItem, int newAmount)
    {
        bool newEntry = false;

        foreach (InventoryItem item in inventory)
        {
            if (item.inventoryItem == newItem)
            {
                item.inventoryCount += newAmount;
                newEntry = true;
                break;
            }
        }

        if (newEntry == false)
        {
            InventoryItem newInventoryItem = new InventoryItem();
            newInventoryItem.inventoryCount = newAmount;
            newInventoryItem.inventoryItem = newItem;

            inventory.Add(newInventoryItem);
        }

        foreach (InventoryItem item in inventory)
        {
            Debug.Log(item.inventoryItem.itemName + " " + item.inventoryCount);
        }
    }
    public void Remove(Item removeItem, int removeAmount)
    {
        foreach (InventoryItem item in inventory)
        {
            if (item.inventoryItem == removeItem)
            {
                if(removeAmount >= item.inventoryCount)
                {
                    inventory.Remove(item);
                }
                else
                {
                    item.inventoryCount -= removeAmount;
                }
            }
        }
    }

}
