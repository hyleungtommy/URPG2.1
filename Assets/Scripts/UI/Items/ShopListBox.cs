using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopListBox : ListBox
{
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] Text ItemName;
    [SerializeField] Text ItemPrice;
    private ItemTemplate itemTemplate;

    public override void Render()
    {
        itemTemplate = obj as ItemTemplate;
        if (itemTemplate != null)
        {
            itemBox.Render(itemTemplate);
            ItemName.text = itemTemplate.Name;
            ItemPrice.text = itemTemplate.Price.ToString();
        }
    }
}
