using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class QuestScene : CommonListScene<QuestListBox>{
    [SerializeField] Text acceptedQuestCountText;

    public void Start(){
        UpdateScene();
    }

    public void UpdateScene(){
        ClearDisplayList();
        infoPanel.Hide();
        acceptedQuestCountText.text = $"Accepted Quest: {Game.QuestManager.GetAcceptedQuests().Count}/{Constant.MaxAcceptedQuest}";
        if(Game.QuestBoardMode == QuestBoardMode.Available){
            AddDisplayList(Game.QuestManager.GetAvailableQuests().Cast<System.Object>().ToList());
        }else if(Game.QuestBoardMode == QuestBoardMode.Accepted){
            AddDisplayList(Game.QuestManager.GetAcceptedQuests().Cast<System.Object>().ToList());
        }
        Render();
    }

}