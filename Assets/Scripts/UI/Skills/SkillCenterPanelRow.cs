using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCenterPanelRow : MonoBehaviour
{
    [SerializeField] Image skillIcon;
    [SerializeField] Text skillName;
    [SerializeField] Text price;

    public void Render(Skill skill){
        skillIcon.sprite = skill.Icon;
        skillName.text = skill.Name + "\nLv." + skill.SkillLv;
        price.text = skill.Price.ToString();
    }
}
