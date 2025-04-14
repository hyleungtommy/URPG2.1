using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopBox : MonoBehaviour
{
    [SerializeField] Image ItemImage;
    [SerializeField] Image Rarity;
    [SerializeField] Text ItemName;
    [SerializeField] Text ItemPrice;
    private ItemTemplate itemTemplate;

    public void Setup(ItemTemplate itemTemplate)
    {
        this.itemTemplate = itemTemplate;
    }

    public void Render(){
        ItemImage.sprite = itemTemplate.Icon;
        ItemName.text = itemTemplate.Name;
        ItemPrice.text = itemTemplate.Price.ToString();
        if (itemTemplate.Rarity > 0)
        {
            Rarity.gameObject.SetActive(true);
            Rarity.color = Constant.itemRarityColor[itemTemplate.Rarity];
        }
        else
        {
            Rarity.gameObject.SetActive(false);
        }
    }
}
