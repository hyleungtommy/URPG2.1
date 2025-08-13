using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : CommonListScene<SkillListBox>
{

    private Skill[] skills;
    private BattleCharacter character;
    public void Setup(Skill[] skills, BattleCharacter character)
    {
        this.skills = skills;
        this.character = character;
    }

    public void Show(){
        gameObject.SetActive(true);
        AddDisplayList(skills.ToList().Cast<System.Object>().ToList());
        Render();
    }

}