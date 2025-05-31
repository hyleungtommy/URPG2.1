using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentEquipmentGroup : MonoBehaviour
{
    [SerializeField] CurrentEquipmentSlots mainHand;
    [SerializeField] CurrentEquipmentSlots offHand;
    [SerializeField] CurrentEquipmentSlots head;
    [SerializeField] CurrentEquipmentSlots body;
    [SerializeField] CurrentEquipmentSlots hands;
    [SerializeField] CurrentEquipmentSlots legs;
    [SerializeField] CurrentEquipmentSlots feet;
    [SerializeField] CurrentEquipmentSlots accessory1;
    [SerializeField] CurrentEquipmentSlots accessory2;

    public void Render(EquipmentManager equipmentManager){
        mainHand.Render(equipmentManager.MainHand);
        offHand.Render(equipmentManager.OffHand);
        head.Render(equipmentManager.Head);
        body.Render(equipmentManager.Body);
        hands.Render(equipmentManager.Hands);
        legs.Render(equipmentManager.Legs);
        feet.Render(equipmentManager.Feet);
    }
}
