using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectionRow : MonoBehaviour
{
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] Text Quantity;
    [SerializeField] Text ItemName;
    private ItemWithQuantity itemWithQuantity;

    public void Setup(ItemWithQuantity itemWithQuantity)
    {
        this.itemWithQuantity = itemWithQuantity;
    }

    public void Render()
    {
        itemBox.Render(itemWithQuantity.Item);
        Quantity.text = itemWithQuantity.Quantity.ToString();
        ItemName.text = itemWithQuantity.Item.Name;
    }
    
    
}
