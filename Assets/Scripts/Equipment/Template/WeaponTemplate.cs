using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Equipment/Weapon")]
public class WeaponTemplate: ScriptableObject
{
    public string WeaponName;
    public WeaponType WeaponType;
    public string Description;
    public Sprite Icon;
    public int Price;
    public int Damage;
    public int MagicDamage;
    public int requireLv;
}
