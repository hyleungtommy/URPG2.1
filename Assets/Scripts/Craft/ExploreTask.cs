using System.Collections.Generic;
using UnityEngine;

public class ExploreTask : TimedTask
{
    public ExploreSite exploreSite { set; get; }
    public ExploreTask(ExploreSite exploreSite) : base(exploreSite.requireTime)
    {
        this.exploreSite = exploreSite;
    }

    public override TaskCompleteMessage CompleteTask()
    {
        List<ItemWithQuantity> rewardItemList = new List<ItemWithQuantity>();
        for (int i = 0; i < exploreSite.obtainableItems.Length; i++)
        {
            ItemWithQuantity reward = exploreSite.obtainableItems[i].GetReward();
            if (reward != null)
            {
                rewardItemList.Add(reward);
                Game.Inventory.InsertItem(reward.Item, reward.Quantity);
            }
        }
        Game.CraftSkillManager.AvailableExploreTeam++;
        if (exploreSite.type == ExploreSiteType.Mining)
        {
            Game.CraftSkillManager.AddExperience(CraftSkillType.Mining, 1);
        }
        else if (exploreSite.type == ExploreSiteType.Forging)
        {
            Game.CraftSkillManager.AddExperience(CraftSkillType.Forging, 1);
        }
        else
        {
            Game.CraftSkillManager.AddExperience(CraftSkillType.Hunting, 1);
        }
        return new TaskCompleteMessage(rewardItemList);
    }
}