using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventoryListBox : ListBox
{
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] Text Quantity;

    public override void Render()
    {
        StorageSlot storageSlot = obj as StorageSlot;
        if (storageSlot == null || storageSlot.Item == null)
        {
            itemBox.RenderNull();
            Quantity.gameObject.SetActive(false);
        }
        else
        {
            itemBox.Render(storageSlot.Item);
            Quantity.gameObject.SetActive(true);
            Quantity.text = storageSlot.Quantity.ToString();
        }
    }
}
