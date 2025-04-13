using UnityEngine;

[CreateAssetMenu(fileName = "ItemMPPotionTemplate", menuName = "Item/MP Potion")]
public class ItemMPPotionTemplate : ItemTemplate
{
    public int MPRestorePercentage;
    public int MinMPRestore;
    public bool AOE;
}


