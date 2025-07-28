using UnityEngine;
using System;
[CreateAssetMenu(fileName = "ExploreSiteTemplate", menuName = "Craft/ExploreSite")]
public class ExploreSiteTemplate : ScriptableObject{
    public int id;
    public string siteName;
    public string description;
    public int requiredLevel;
    public ExploreSiteType type;
    public int requireTime;
    public ObtainableItemTemplate[] obtainableItems;
    public int price;
}

[Serializable]
public class ObtainableItemTemplate{
    public ItemTemplate item;
    public int minAmount;
    public int maxAmount;
    public int chance;
    public ItemWithQuantity GetReward(){
        int appearChance = Mathf.FloorToInt(UnityEngine.Random.Range(0, 101));
        if (appearChance < chance)
        {
            int amount = Mathf.FloorToInt(UnityEngine.Random.Range(minAmount, maxAmount + 1));
            return new ItemWithQuantity(item.GetItem(), amount);
        }
        return null;
    }
}