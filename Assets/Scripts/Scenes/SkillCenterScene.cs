using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillCenterScene : CommonListScene<SkillCenterPanelRow>, MemberListScene
{
    [SerializeField] Text moneyText;
    [SerializeField] MemberList memberList;
    public BattleCharacter selectedMember {get; private set;}

    void Start()
    {
        moneyText.text = Game.Money.ToString();
        memberList.memberListScene = this;
        OnMemberSelected(Game.Party.GetAllMembers()[0]);
    }

    public void OnMemberSelected(BattleCharacter member)
    {
        this.selectedMember = member;
        AddDisplayList(member.CharacterClass.LearnableSkillsList.Cast<System.Object>().ToList());
        Render();
    }

    public void UpdateMoneyText(){
        moneyText.text = Game.Money.ToString();
    }

}
