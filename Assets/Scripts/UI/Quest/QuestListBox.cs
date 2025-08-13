using UnityEngine;
using UnityEngine.UI;

public class QuestListBox : ListBox{
    private Quest quest;
    public Text QuestName;
    public Text RequireLevel;

    public override void Render(){
        quest = obj as Quest;
        QuestName.text = quest.QuestName;
        RequireLevel.text = $"Lv.{quest.RequireLevel}";
    }
    
}