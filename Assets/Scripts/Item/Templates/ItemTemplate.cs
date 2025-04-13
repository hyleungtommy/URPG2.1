using UnityEngine;

public class ItemTemplate : ScriptableObject
{
    public int id;
    public string Name;
    [TextArea(3, 10)]
    public string Description;
    public int Price;
    public int SellPrice;
    [Range(0, 4)]
    public int Rarity;
    public Sprite Icon;
}
