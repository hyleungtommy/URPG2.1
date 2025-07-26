using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScene : MonoBehaviour{
    public QuestButton QuestButtonPrefab;
    public Transform QuestButtonContainer;
    public QuestCompleteDialog QuestCompleteDialog;
    public QuestRequirementRow[] RequirementRows;
    public Text QuestName;
    public Text AcceptedQuestCount;
    public Text QuestType;
    public Text QuestDescription;
    public Text QuestRewardMoney;
    public Text QuestRewardExp;
    public Button AcceptButton;
    public Button CompleteButton;
    public Button AbandonButton;
    private List<QuestButton> questButtons = new List<QuestButton>();
    private List<Quest> questList = new List<Quest>();
    private int currentQuestId = -1;

    public void Start(){
        QuestCompleteDialog.Hide();
        ClearInfoPanel();
        
        Render();
    }

    public void ClearInfoPanel(){
        QuestName.text = "";
        QuestDescription.text = "";
        QuestRewardMoney.text = "";
        QuestRewardExp.text = "";
        QuestType.text = "";
        AcceptedQuestCount.text = "";
        AcceptButton.gameObject.SetActive(false);
        CompleteButton.gameObject.SetActive(false);
        AbandonButton.gameObject.SetActive(false);
        AcceptedQuestCount.text = $"Accepted Quest: {Game.QuestManager.GetAcceptedQuests().Count}/{Constant.MaxAcceptedQuest}";
        for(int i = 0; i < 5; i++){
            RequirementRows[i].gameObject.SetActive(false);
        }
    }

    public void Render(){
        foreach (Transform child in QuestButtonContainer)
        {
            Destroy(child.gameObject);
        }
        questButtons.Clear();
        questList.Clear();
        
        if(Game.QuestBoardMode == QuestBoardMode.Available){
            questList = Game.QuestManager.GetAvailableQuests();
            for (int i = 0; i < questList.Count; i++)
            {
                int j = i;
                QuestButton box = Instantiate(QuestButtonPrefab, QuestButtonContainer);
                box.Setup(questList[j]);
                box.GetComponent<Button>().onClick.AddListener(() => this.OnClickQuestButton(j));
                box.Render();
                questButtons.Add(box);
            }
        }else if(Game.QuestBoardMode == QuestBoardMode.Accepted){
            questList = Game.QuestManager.GetAcceptedQuests();
            for (int i = 0; i < questList.Count; i++)
            {
                int j = i;
                QuestButton box = Instantiate(QuestButtonPrefab, QuestButtonContainer);
                box.Setup(questList[j]);
                box.GetComponent<Button>().onClick.AddListener(() => this.OnClickQuestButton(j));
                box.Render();
                questButtons.Add(box);
            }
        }
    }

    public void OnClickQuestButton(int id){
        Quest quest = questList[id];
        currentQuestId = quest.Id;
        QuestName.text = quest.QuestName;
        QuestDescription.text = quest.Description;
        QuestRewardMoney.text = quest.RewardMoney.ToString();
        QuestRewardExp.text = quest.RewardExp.ToString();
        QuestType.text = quest.QuestType.ToString();
        AcceptButton.gameObject.SetActive(quest.State == Quest.QuestState.Available);
        CompleteButton.gameObject.SetActive(quest.State == Quest.QuestState.Accepted && quest.IsCompleted());
        AbandonButton.gameObject.SetActive(quest.State == Quest.QuestState.Accepted);
        RenderRequirementRows(quest);
    }

    public void RenderRequirementRows(Quest quest){
        for(int i = 0; i < 5; i++){
            if(i < quest.Requirements.Length){
                RequirementRows[i].gameObject.SetActive(true);
                RequirementRows[i].Render(quest.Requirements[i]);
            }else{
                RequirementRows[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnClickAcceptButton(){
        Game.QuestManager.AcceptQuest(currentQuestId);
        ClearInfoPanel();
        Render();
    }

    public void OnClickCompleteButton(){
        QuestReward reward = Game.QuestManager.CompleteQuest(currentQuestId);
        if(reward != null){
            QuestCompleteDialog.Setup(reward);
            QuestCompleteDialog.Show();
        }
        ClearInfoPanel();
        Render();
    }

    public void OnClickAbandonButton(){
        Game.QuestManager.AbandonQuest(currentQuestId);
        ClearInfoPanel();
        Render();
    }
}