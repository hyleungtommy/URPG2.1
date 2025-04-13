using UnityEngine;
[CreateAssetMenu(fileName = "ItemHPPotionTemplate", menuName = "Item/HP Potion")]
public class ItemHPPotionTemplate : ItemTemplate
{
    public int HPRestorePercentage;
    public int MinHPRestore;
    public bool AOE;
}
