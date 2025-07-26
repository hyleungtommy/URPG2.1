using UnityEngine;
using UnityEngine.UI;

public class QuestCompleteDialog : MonoBehaviour{
    public Text QuestName;
    public Text RewardMoney;
    public Text RewardExp;
    public void Render(Quest quest){
        QuestName.text = quest.QuestName;
        RewardMoney.text = quest.RewardMoney.ToString();
        RewardExp.text = quest.RewardExp.ToString();
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
    }
}