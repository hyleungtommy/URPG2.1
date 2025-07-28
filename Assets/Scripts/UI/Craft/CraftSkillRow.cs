using System;
using UnityEngine;
using UnityEngine.UI;

public class CraftSkillRow : MonoBehaviour{
    [SerializeField] Bar expBar;
    [SerializeField] Text skillName;

    public void Render(CraftSkill craftSkill){
        skillName.text = craftSkill.Type.ToString() + " Lv." + craftSkill.Level;
        expBar.Render(craftSkill.CurrentExp, craftSkill.RequiredExp);
    }
}