using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectionRow : MonoBehaviour
{
    [SerializeField] Image ItemImage;
    [SerializeField] Text Quantity;
    [SerializeField] Image Rarity;
    [SerializeField] Text ItemName;
    private ItemWithQuantity itemWithQuantity;

    public void Setup(ItemWithQuantity itemWithQuantity)
    {
        this.itemWithQuantity = itemWithQuantity;
    }

    public void Render()
    {
        ItemImage.sprite = itemWithQuantity.Item.Icon;
        Quantity.text = itemWithQuantity.Quantity.ToString();
        if (itemWithQuantity.Item.Rarity > 0)
        {
            Rarity.gameObject.SetActive(true);
            Rarity.color = Constant.itemRarityColor[itemWithQuantity.Item.Rarity];
        }
        else
        {
            Rarity.gameObject.SetActive(false);
        }
        ItemName.text = itemWithQuantity.Item.Name;
    }
    
    
}
