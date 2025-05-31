using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Equipment/Armor")]
public class ArmorTemplate: ScriptableObject
{
    public string ArmorName;
    public ArmorType ArmorType;
    public ArmorCategory ArmorCategory;
    public string Description;
    public Sprite Icon;
    public int Price;
    public int Defense;
    public int MagicDefense;
    public int requireLv;
    
    
}