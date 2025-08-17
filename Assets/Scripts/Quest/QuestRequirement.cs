using System;

public class QuestRequirement{
    public IQuestRequirement Requirement {get; set;}
    public int Amount {get; set;}
    public int CurrentAmount {get{
        if(Requirement is EnemyTemplate){
            return _currentAmount;
        }
        if(Requirement is ItemTemplate){
            return Math.Min(Game.Inventory.GetTotalItemQuantity((Requirement as ItemTemplate).GetItem()), Amount);
        }
        return 0;
    }}
    private int _currentAmount = 0;

    public bool IsCompleted => CurrentAmount >= Amount;

    public void Update(int[] enemyIds){
        if(Requirement is EnemyTemplate){
            if(_currentAmount < Amount){
            foreach(var enemyId in enemyIds){
                if(enemyId == Int32.Parse((Requirement as EnemyTemplate).ID)){
                        _currentAmount++;
                    }
                }
            }
        }
    }

    public QuestRequirement(QuestRequirementTemplate questRequirementTemplate){
        if(questRequirementTemplate.Type == QuestRequirementTemplate.RequirementType.Enemy){
            Requirement = questRequirementTemplate.EnemyRequirement;
        }
        if(questRequirementTemplate.Type == QuestRequirementTemplate.RequirementType.Item){
            Requirement = questRequirementTemplate.ItemRequirement;
        }
        Amount = questRequirementTemplate.Amount;
    }

    public void Reset(){
        _currentAmount = 0;
    }
}