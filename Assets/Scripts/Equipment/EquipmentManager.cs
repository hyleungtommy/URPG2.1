using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager
{
    public Weapon MainHand { get; private set; }
    public Weapon OffHand { get; private set; }
    public Armor Head { get; private set; }
    public Armor Body { get; private set; }
    public Armor Hands { get; private set; }
    public Armor Legs { get; private set; }
    public Armor Feet { get; private set; }

    public void Equip(Equipment equipment){
        if (equipment == null) return;
        if (equipment is Weapon){
            EquipWeapon(equipment as Weapon);
        }
        else if (equipment is Armor){
            EquipArmor(equipment as Armor);
        }
    }

    public void Unequip(Equipment equipment){
        if (equipment == null) return;
        if (equipment is Weapon){
            UnequipWeapon(equipment as Weapon);
        }
        else if (equipment is Armor){
            UnequipArmor(equipment as Armor);
        }
        RemoveCurrentlyEquipped(equipment);
    }

    private void EquipWeapon(Weapon weapon)
    {
        if (weapon != null)
        {
            if (IsSingleHandedWeapon(weapon))
            {
                if (weapon.WeaponType == WeaponType.Shield || weapon.WeaponType == WeaponType.Artifact)
                {
                    UnequipWeapon(OffHand);
                    OffHand = weapon;
                }
                else
                {
                    UnequipWeapon(MainHand);
                    MainHand = weapon;
                }
            }
            else
            {
                UnequipWeapon(MainHand);
                MainHand = weapon;
                if (OffHand != null)
                {
                    UnequipWeapon(OffHand);
                    OffHand = null;
                }
            }
        }
    }

    private void EquipArmor(Armor armor)
    {
        if (armor.ArmorType == ArmorType.Helmet)
        {
            UnequipArmor(Head);
            Head = armor;
        }
        else if (armor.ArmorType == ArmorType.Armor)
        {
            UnequipArmor(Body);
            Body = armor;
        }
        else if (armor.ArmorType == ArmorType.Gloves)
        {
            UnequipArmor(Hands);
            Hands = armor;
        }
        else if (armor.ArmorType == ArmorType.Boots)
        {
            UnequipArmor(Feet);
            Feet = armor;
        }
        else if (armor.ArmorType == ArmorType.Pants)
        {
            UnequipArmor(Legs);
            Legs = armor;
        }
    }

    private void UnequipWeapon(Weapon weapon)
    {
        if (weapon != null)
        {
            Debug.Log("Unequip " + weapon.Name);
            GameController.Instance.Inventory.InsertItem(weapon, 1);
        }
    }

    private void UnequipArmor(Armor armor)
    {
        if (armor != null)
        {
            Debug.Log("Unequip " + armor.Name);
            GameController.Instance.Inventory.InsertItem(armor, 1);
        }
    }

    public void RemoveCurrentlyEquipped(Equipment equipment){
        if (equipment is Weapon){
            Weapon weapon = equipment as Weapon;
            if (weapon.WeaponType == WeaponType.Shield || weapon.WeaponType == WeaponType.Artifact){
                OffHand = null;
            }
            else{
                MainHand = null;
            }
        }
        else if (equipment is Armor){
            Armor armor = equipment as Armor;
            if (armor.ArmorType == ArmorType.Helmet){
                Head = null;
            }
            else if (armor.ArmorType == ArmorType.Armor){
                Body = null;
            }
            else if (armor.ArmorType == ArmorType.Gloves){
                Hands = null;
            }
            else if (armor.ArmorType == ArmorType.Boots){
                Feet = null;
            }
            else if (armor.ArmorType == ArmorType.Pants){
                Legs = null;
            }
        }
        
    }

    public bool IsSingleHandedWeapon(Weapon weapon)
    {
        if (weapon.WeaponType == WeaponType.Sword || weapon.WeaponType == WeaponType.Shield || weapon.WeaponType == WeaponType.Wand)
        {
            return true;
        }
        return false;
    }

    public BaseStat GetEquippedStat(){
        BaseStat stat = new BaseStat(0, 0, 0, 0, 0, 0, 0, 0);
        if (MainHand != null){
            stat = stat.Add(MainHand.GetStat());
        }
        if (OffHand != null){
            stat = stat.Add(OffHand.GetStat());
        }
        if (Head != null){
            stat = stat.Add(Head.GetStat());
        }
        if (Body != null){
            stat = stat.Add(Body.GetStat());
        }
        if (Hands != null){
            stat = stat.Add(Hands.GetStat());
        }
        if (Feet != null){
            stat = stat.Add(Feet.GetStat());
        }
        if (Legs != null){
            stat = stat.Add(Legs.GetStat());
        }
        return stat;
    }
}