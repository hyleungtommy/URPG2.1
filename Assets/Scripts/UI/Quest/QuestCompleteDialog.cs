using UnityEngine;
using UnityEngine.UI;

public class QuestCompleteDialog : MonoBehaviour{
    public Text QuestName;
    public Text RewardMoney;
    public Text RewardExp;
    public void Setup(QuestReward reward){
        QuestName.text = reward.QuestName;
        RewardMoney.text = reward.Money.ToString();
        RewardExp.text = reward.Exp.ToString();
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
    }
}