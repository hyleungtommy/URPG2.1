using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventoryBox : MonoBehaviour
{
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] Text Quantity;

    private StorageSlot storageSlot;

    public void Setup(StorageSlot storageSlot)
    {
        this.storageSlot = storageSlot;
    }

    public void Render()
    {
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
