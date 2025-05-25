using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BasicItemBox : MonoBehaviour
{
    [SerializeField] Image ItemImage;
    [SerializeField] Image Rarity;
    public void Render(ItemTemplate itemTemplate)
    {
        if (itemTemplate == null)
        {
            RenderNull();
            return;
        }
        else
        {
            RenderItem(itemTemplate.Icon, itemTemplate.Rarity);
        }
    }

    public void Render(WeaponTemplate weaponTemplate)
    {
        if (weaponTemplate == null)
        {
            RenderNull();
            return;
        }
        else
        {
            RenderItem(weaponTemplate.Icon, 0);
        }
    }

    public void Render(ArmorTemplate armorTemplate)
    {
        if (armorTemplate == null)
        {
            RenderNull();
            return;
        }
        else
        {
            RenderItem(armorTemplate.Icon, 0);
        }
    }

    public void Render(Item item) {
        if (item == null)
        {
            RenderNull();
            return;
        }
        else
        {
            RenderItem(item.Icon, item.Rarity);
        }
    }

    private void RenderItem(Sprite icon, int rarity){
        ItemImage.gameObject.SetActive(true);
        ItemImage.sprite = icon;
        if (rarity > 0)
        {
            Rarity.gameObject.SetActive(true);
            Rarity.color = Constant.itemRarityColor[rarity];
        }
        else
        {
            Rarity.gameObject.SetActive(false);
        }
    }

    public void RenderNull(){
        Rarity.gameObject.SetActive(false);
        ItemImage.gameObject.SetActive(false);
    }
}
