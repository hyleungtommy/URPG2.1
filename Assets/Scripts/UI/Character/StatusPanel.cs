using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
    [SerializeField] Image portrait;
    [SerializeField] Text characterName;
    [SerializeField] Text baseStats;
    [SerializeField] Text characterStats;
    [SerializeField] Text upgradePointsAvailable;
    [SerializeField] Text skillPointsAvailable;
    [SerializeField] Text exp;
    [SerializeField] Text title;
    [SerializeField] CurrentEquipmentGroup currentEquipmentGroup;

    private BattleCharacter character;

    public void Setup(BattleCharacter character){
        this.character = character;
    }

    public void Open(){
        gameObject.SetActive(true);
        // Update the UI elements with the character's information
        portrait.sprite = character.Portrait;
        characterName.text = character.Name;
        title.text = FormatTitle(character);
        baseStats.text = FormatBaseStat(character);
        characterStats.text = FormatCharacterStat(character);
        upgradePointsAvailable.text = character.UpgradePointAvailable.ToString();
        skillPointsAvailable.text = character.SkillPointAvailable.ToString();
        exp.text = "Exp: " + character.CurrentEXP + "/" + character.RequiredEXP;
        currentEquipmentGroup.Render(character.CharacterClass.EquipmentManager);
    }

    private string FormatTitle(BattleCharacter character)
    {
        return "Lv. " + character.Lv + 
                " " + character.CharacterClass.ClassName;
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