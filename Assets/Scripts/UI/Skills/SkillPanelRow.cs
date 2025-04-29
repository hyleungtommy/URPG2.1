using UnityEngine;
using UnityEngine.UI;

public class SkillPanelRow : MonoBehaviour
{
    [SerializeField] Image skillIcon;
    [SerializeField] Text skillName;
    [SerializeField] Text skillMPCost;

    public void Render(Skill skill){
        skillIcon.sprite = skill.Icon;
        skillName.text = skill.Name + "\nLv." + skill.SkillLv;
        skillMPCost.text = skill.MpCost.ToString();
    }

    public void Render(Skill skill, BattleEntity user){
        skillIcon.sprite = skill.Icon;
        skillName.text = skill.Name + "\nLv." + skill.SkillLv;
        skillMPCost.text = skill.MpCost.ToString();
        if(user.CurrentMP < skill.MpCost){
            skillMPCost.color = Color.red;
        }else{
            skillMPCost.color = Color.gray;
        }
    }

}