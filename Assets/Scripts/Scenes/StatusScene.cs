using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusScene : MonoBehaviour, MemberListScene
{
    [SerializeField] SkillPanel skillPanel;
    [SerializeField] StatusPanel statusPanel;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] MemberList memberList;

    private int selectedIndex = 0;

    void Start(){
        memberList.memberListScene = this;
        OnMemberSelected(Game.Party.GetAllMembers()[0]);
        //AddTestEquipment();
    }

    public void OnMemberSelected(BattleCharacter character)
    {
        if (character == null)
        {
            return;
        }

        statusPanel.Setup(character);
        skillPanel.Setup(character.CharacterClass.LearntSkillsList, character);
        equipmentPanel.Setup(character);

        CloseOtherPanels();
        if(selectedIndex == 0){
            statusPanel.Open();
        }else if(selectedIndex == 2){
            skillPanel.Show();
        }else if(selectedIndex == 3){
            equipmentPanel.Show();
        }
    }

    public void OnClickSkill(){
        selectedIndex = 2;
        CloseOtherPanels();
        memberList.memberListScene = skillPanel;
        skillPanel.Show();
    }

    public void OnClickStatus(){
        selectedIndex = 0;
        CloseOtherPanels();
        memberList.memberListScene = this;
        statusPanel.Open();
    }

    public void OnClickEquipment(){
        selectedIndex = 3;
        CloseOtherPanels();
        //memberList.memberListScene = equipmentPanel;
        equipmentPanel.Show();
    }

    private void CloseOtherPanels(){
        statusPanel.gameObject.SetActive(false);
        skillPanel.gameObject.SetActive(false);
        equipmentPanel.gameObject.SetActive(false);
    }

    public void OnClickBack(){
        selectedIndex = 0;
        CloseOtherPanels();
        memberList.memberListScene = this;
        statusPanel.Open();
    }


}
