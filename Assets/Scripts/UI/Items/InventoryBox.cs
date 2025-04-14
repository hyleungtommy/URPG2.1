using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventoryBox : MonoBehaviour
{
    [SerializeField] Image ItemImage;
    [SerializeField] Text Quantity;
    [SerializeField] Image Rarity;

    private StorageSlot storageSlot;

    public void Setup(StorageSlot storageSlot)
    {
        this.storageSlot = storageSlot;
    }

    public void Render()
    {
        if (storageSlot == null || storageSlot.Item == null)
        {
            ItemImage.gameObject.SetActive(false);
            Quantity.gameObject.SetActive(false);
            Rarity.gameObject.SetActive(false);
        }
        else
        {
            ItemImage.gameObject.SetActive(true);
            Quantity.gameObject.SetActive(true);
            ItemImage.sprite = storageSlot.Item.Icon;
            Quantity.text = storageSlot.Quantity.ToString();
            if (storageSlot.Item.Rarity > 0)
            {
                Rarity.gameObject.SetActive(true);
                Rarity.color = Constant.itemRarityColor[storageSlot.Item.Rarity];
            }
            else
            {
                Rarity.gameObject.SetActive(false);
            }
        }
    }
}
