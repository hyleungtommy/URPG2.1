using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : CommonListScene<SkillListBox>, MemberListScene
{

    [SerializeField] Text characterNameText;
    private Skill[] skills;
    private BattleCharacter character;
    public void Setup(Skill[] skills, BattleCharacter character)
    {
        this.skills = skills;
        this.character = character;
    }

    public void OnMemberSelected(BattleCharacter member)
    {
        this.character = member;
        Debug.Log("SkillPanel: OnMemberSelected: " + character.Name);
        Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ClearDisplayList();
        characterNameText.text = character.Name;
        AddDisplayList(skills.ToList().Cast<System.Object>().ToList());
        Render();
    }

}