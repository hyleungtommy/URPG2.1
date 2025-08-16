using UnityEngine;
using UnityEngine.UI;

public class SkillListBox : ListBox
{
    [SerializeField] Image skillIcon;
    [SerializeField] Text skillName;
    [SerializeField] Text skillMPCost;

    public override void Render(){
        Skill skill = obj as Skill;
        skillIcon.sprite = skill.Icon;
        skillName.text = skill.Name + "\nLv." + skill.SkillLv;
        skillMPCost.text = skill.MpCost.ToString();
    }

    public void RenderBattleSkill(Skill skill, BattleEntity user){
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