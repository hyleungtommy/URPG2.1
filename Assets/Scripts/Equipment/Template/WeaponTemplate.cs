using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Equipment/Weapon")]
public class WeaponTemplate: ItemTemplate
{
    public WeaponType WeaponType;
    public int Damage;
    public int MagicDamage;
    public int requireLv;

    public override Item GetItem()
    {
        return new Weapon(this);
    }

    public override string GetItemType()
    {
        return "Weapon";
    }
}
