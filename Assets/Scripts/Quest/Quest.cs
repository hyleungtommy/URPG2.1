using System.Linq;

public class Quest{
    public int Id {get; set;}
    public string QuestName {get; set;}
    public QuestType QuestType {get; set;}
    public string Description {get; set;}
    public int RequireLevel {get; set;}
    public int MaxLevel {get; set;}
    public int RewardExp {get; set;}
    public int RewardMoney {get; set;}
    public QuestRequirement[] Requirements {get; set;}
    public QuestState State {get; set;}
    public Quest(QuestTemplate questTemplate){
        Id = questTemplate.Id;
        QuestName = questTemplate.QuestName;
        QuestType = questTemplate.QuestType;
        Description = questTemplate.Description;
        RequireLevel = questTemplate.RequireLevel;
        MaxLevel = questTemplate.MaxLevel;
        Requirements = questTemplate.Requirements.Select(r => new QuestRequirement(r)).ToArray();
        State = QuestState.Available;
        RewardExp = questTemplate.RewardExp;
        RewardMoney = questTemplate.RewardMoney;
    }

    public void Update(int[] enemyIds){
        foreach(var requirement in Requirements){
            requirement.Update(enemyIds);
        }
    }

    public enum QuestState{
        Available,
        Accepted,
        Completed
    }

    public bool IsCompleted(){
        foreach(var requirement in Requirements){
            if(requirement.CurrentAmount < requirement.Amount){
                return false;
            }
        }
        return true;
    }

    public QuestReward Complete(){
        State = QuestState.Completed;
        foreach(var requirement in Requirements){
            if(requirement.Requirement is EnemyTemplate){
                requirement.Reset();
            }
            if(requirement.Requirement is ItemTemplate){
                Game.Inventory.RemoveItem((requirement.Requirement as ItemTemplate).GetItem(), requirement.Amount);
            }
        }
        return new QuestReward{
            QuestName = QuestName,
            Money = RewardMoney,
            Exp = RewardExp
        };
    }
}
