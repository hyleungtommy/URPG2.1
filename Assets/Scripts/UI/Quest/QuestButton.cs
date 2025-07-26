using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour{
    private Quest quest;
    public Text QuestName;
    public Text RequireLevel;

    public void Setup(Quest quest){
        this.quest = quest;
    }

    public void Render(){
        QuestName.text = quest.QuestName;
        RequireLevel.text = $"Lv.{quest.RequireLevel}";
    }
    
}