using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopBox : MonoBehaviour
{
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] Text ItemName;
    [SerializeField] Text ItemPrice;
    private ItemTemplate itemTemplate;

    public void Setup(ItemTemplate itemTemplate)
    {
        this.itemTemplate = itemTemplate;
    }

    public void Render(){
        itemBox.Render(itemTemplate);
        ItemName.text = itemTemplate.Name;
        ItemPrice.text = itemTemplate.Price.ToString();
    }
}
