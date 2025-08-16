using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCenterListBox : ListBox
{
    [SerializeField] Image skillIcon;
    [SerializeField] Text skillName;
    [SerializeField] Text price;

    public override void Render(){
        Skill skill = obj as Skill;
        skillIcon.sprite = skill.Icon;
        skillName.text = skill.Name + "\nLv." + skill.SkillLv;
        price.text = skill.Price.ToString();
    }
}
