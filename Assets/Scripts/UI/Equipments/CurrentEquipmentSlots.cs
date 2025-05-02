using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentEquipmentSlots : MonoBehaviour
{
    [SerializeField] Image equipmentDefaultIcon;
    [SerializeField] Image equipmentIcon;
    [SerializeField] Image rarity;

    public void Render(Equipment equipment)
    {
        if (equipment == null)
        {
            equipmentDefaultIcon.gameObject.SetActive(true);
            equipmentIcon.gameObject.SetActive(false);
            rarity.gameObject.SetActive(false);
        }
        else
        {
            equipmentDefaultIcon.gameObject.SetActive(false);
            equipmentIcon.gameObject.SetActive(true);
            rarity.gameObject.SetActive(true);
            equipmentIcon.sprite = equipment.Icon;
            if (equipment.Rarity > 0)
            {
                rarity.gameObject.SetActive(true);
                rarity.color = Constant.itemRarityColor[equipment.Rarity];
            }
            else
            {
                rarity.gameObject.SetActive(false);
            }
        }
    }
}
