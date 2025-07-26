using System.Collections.Generic;
using System.Linq;

public class QuestManager{
    public List<Quest> Quests {get; set;}

    public QuestManager(){
        Quests = new List<Quest>();
    }

    public void Initialize(List<Quest> questList){
        Quests = questList;
    }

    public void AcceptQuest(int questId){
        if(GetAcceptedQuests().Count >= Constant.MaxAcceptedQuest){
            return;
        }
        var quest = Quests.FirstOrDefault(q => q.Id == questId);
        if(quest != null){
            quest.State = Quest.QuestState.Accepted;
        }
    }

    public void UpdateAllQuests(int[] enemyIds){
        foreach(var quest in Quests){
            if(quest.State == Quest.QuestState.Accepted){
                quest.Update(enemyIds);
            }
        }
    }

    public void AbandonQuest(int questId){
        var quest = Quests.FirstOrDefault(q => q.Id == questId);
        if(quest.State == Quest.QuestState.Accepted){
            quest.State = Quest.QuestState.Available;
        }
    }
    
    public QuestReward CompleteQuest(int questId){
        var quest = Quests.FirstOrDefault(q => q.Id == questId);
        if(quest.State == Quest.QuestState.Accepted){
            QuestReward reward = quest.Complete();
            Game.AddMoney(reward.Money);
            foreach(var player in Game.Party.GetAllMembers()){
                if(player != null && player.Unlocked){
                    player.GainEXP(reward.Exp);
                }
            }
            if(quest.QuestType == QuestType.Repetable){
                quest.State = Quest.QuestState.Available;
            }else{
                quest.State = Quest.QuestState.Completed;
            }
            return reward;
        }
        return null;
    }

    public List<Quest> GetAvailableQuests(){
        //TODO: add level check
        return Quests.Where(q => q.State == Quest.QuestState.Available).ToList();
    }

    public List<Quest> GetAcceptedQuests(){
        return Quests.Where(q => q.State == Quest.QuestState.Accepted).ToList();
    }

}