using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusScene : MemberListScene
{
    [SerializeField] SkillPanel skillPanel;
    [SerializeField] StatusPanel statusPanel;

    private int selectedIndex = 0;

    public override void OnMemberSelected(BattleCharacter character)
    {
        if (character == null)
        {
            return;
        }

        statusPanel.Setup(character);
        skillPanel.Setup(character.Class.LearntSkillsList);

        CloseOtherPanels();
        if(selectedIndex == 0){
            statusPanel.Open();
        }else if(selectedIndex == 2){
            skillPanel.Show();
        }
    }

    public void OnClickSkill(){
        selectedIndex = 2;
        CloseOtherPanels();
        skillPanel.Show();
    }

    public void OnClickStatus(){
        selectedIndex = 0;
        CloseOtherPanels();
        statusPanel.Open();
    }

    private void CloseOtherPanels(){
        statusPanel.gameObject.SetActive(false);
        skillPanel.gameObject.SetActive(false);
    }
}
