using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusScene : MemberListScene
{
    [SerializeField] Image portrait;
    [SerializeField] Text characterName;
    [SerializeField] Text baseStats;
    [SerializeField] Text characterStats;
    [SerializeField] Text upgradePointsAvailable;
    [SerializeField] Text skillPointsAvailable;
    [SerializeField] Text exp;
    [SerializeField] Text title;

    public override void OnMemberSelected(BattleCharacter character)
    {
        if (character == null)
        {
            return;
        }

        // Update the UI elements with the character's information
        portrait.sprite = character.Portrait;
        characterName.text = character.Name;
        title.text = FormatTitle(character);
        baseStats.text = FormatBaseStat(character);
        characterStats.text = FormatCharacterStat(character);
        upgradePointsAvailable.text = character.UpgradePointAvailable.ToString();
        skillPointsAvailable.text = character.SkillPointAvailable.ToString();
        exp.text = "Exp: " + character.CurrentEXP + "/" + character.RequiredEXP;
    }

    private string FormatTitle(BattleCharacter character)
    {
        return "Lv. " + character.Lv + 
                " " + character.Class.ClassName;
    }

    private string FormatBaseStat(BattleCharacter character)
    {
        BaseStat baseStat = character.BaseStat;
        return baseStat.HP + "\n" +
                baseStat.MP + "\n" +
                baseStat.ATK + "\n" +
                baseStat.DEF + "\n" +
                baseStat.MATK + "\n" +
                baseStat.MDEF + "\n" +
                baseStat.AGI + "\n" +
                baseStat.DEX;
    }

    private string FormatCharacterStat(BattleCharacter character)
    {
        BattleCharacterStat battleStat = character.BattleStat;
        return battleStat.Strength + "\n" +
                battleStat.Mana + "\n" +
                battleStat.Stamina + "\n" +
                battleStat.Agility + "\n" +
                battleStat.Dexterity;
    }
}
