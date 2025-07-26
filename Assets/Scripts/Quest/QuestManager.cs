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
    
    public void CompleteQuest(int questId){
        var quest = Quests.FirstOrDefault(q => q.Id == questId);
        if(quest.State == Quest.QuestState.Accepted){
            QuestReward reward = quest.Complete();
            Game.AddMoney(reward.Money);
            foreach(var player in Game.Party.GetAllMembers()){
                player.GainEXP(reward.Exp);
            }
            quest.State = Quest.QuestState.Completed;
        }
    }

    public List<Quest> GetAvailableQuests(){
        return Quests.Where(q => q.State == Quest.QuestState.Available).ToList();
    }

    public List<Quest> GetAcceptedQuests(){
        return Quests.Where(q => q.State == Quest.QuestState.Accepted).ToList();
    }

}