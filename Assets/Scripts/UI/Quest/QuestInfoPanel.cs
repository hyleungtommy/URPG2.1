using UnityEngine.UI;
using UnityEngine;

public class QuestInfoPanel:InfoPanel{
    [SerializeField] Text questNameText;
    [SerializeField] Text questDescriptionText;
    [SerializeField] Text questTypeText;
    [SerializeField] Text questRewardMoneyText;
    [SerializeField] Text questRewardExpText;
    [SerializeField] Button acceptButton;
    [SerializeField] Button completeButton;
    [SerializeField] Button abandonButton;
    [SerializeField] QuestRequirementRow[] requirementRows;
    [SerializeField] QuestCompleteDialog questCompleteDialog;
    [SerializeField] QuestScene questScene;

    public override void Render(){
        Quest quest = obj as Quest;
        questNameText.text = quest.QuestName;
        questDescriptionText.text = quest.Description;
        questRewardMoneyText.text = quest.RewardMoney.ToString();
        questRewardExpText.text = quest.RewardExp.ToString();
        questTypeText.text = quest.QuestType.ToString();
        acceptButton.gameObject.SetActive(quest.State == Quest.QuestState.Available);
        completeButton.gameObject.SetActive(quest.State == Quest.QuestState.Accepted && quest.IsCompleted());
        abandonButton.gameObject.SetActive(quest.State == Quest.QuestState.Accepted);
        RenderRequirementRows(quest);
    }

    public void RenderRequirementRows(Quest quest){
        for(int i = 0; i < 5; i++){
            if(i < quest.Requirements.Length){
                requirementRows[i].gameObject.SetActive(true);
                requirementRows[i].Render(quest.Requirements[i]);
            }else{
                requirementRows[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnClickAcceptButton(){
        Quest quest = obj as Quest;
        Game.QuestManager.AcceptQuest(quest.Id);
        questScene.UpdateScene();
    }

    public void OnClickCompleteButton(){
        Quest quest = obj as Quest;
        QuestReward reward = Game.QuestManager.CompleteQuest(quest.Id);
        if(reward != null){
            questCompleteDialog.Setup(reward);
            questCompleteDialog.Show();
        }
        questScene.UpdateScene();
    }

    public void OnClickAbandonButton(){
        Quest quest = obj as Quest;
        Game.QuestManager.AbandonQuest(quest.Id);
        questScene.UpdateScene();
    }
}