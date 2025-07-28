using System.Collections.Generic;

public class TaskCompleteMessage{
    public List<ItemWithQuantity> RewardItemList { get; set; }

    public TaskCompleteMessage(List<ItemWithQuantity> rewardItemList){
        this.RewardItemList = rewardItemList;
    }

    public TaskCompleteMessage(ItemWithQuantity rewardItem){
        this.RewardItemList = new List<ItemWithQuantity>();
        this.RewardItemList.Add(rewardItem);
    }
}