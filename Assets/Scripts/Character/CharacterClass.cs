using System.Linq;
using UnityEngine;

public class CharacterClass
{
    public string ClassName { get; private set; }
    public int[] StartingStats { get; private set; } // [Str, Mana, Stamina, Agi, Dex]
    public int[] StatGrowthPerLevel { get; private set; } // [Str, Mana, Stamina, Agi, Dex]
    public int[] AutoAllocationPatternPerLevel { get; private set; } // [Str, Mana, Stamina, Agi, Dex]
    public Skill[] FullSkillList { get; private set; }
    public Skill[] LearntSkillsList {get {return FullSkillList.Where(skill => skill.SkillLv > 0).ToArray();}}
    public Skill[] LearnableSkillsList {get {return FullSkillList.Where(skill => skill.SkillLv < skill.MaxSkillLv).ToArray();}}
    public EquipmentManager EquipmentManager { get; private set; }
    public CharacterClass(CharacterClassTemplate template)
    {
        ClassName = template.ClassName;
        StartingStats = new int[5];
        StatGrowthPerLevel = new int[5];
        AutoAllocationPatternPerLevel = new int[5];
        FullSkillList = new Skill[template.AvailableSkills.Length];
        for (int i = 0; i < template.AvailableSkills.Length; i++)
        {
            FullSkillList[i] = CreateSkillObject(template.AvailableSkills[i]);
        }
        for (int i = 0; i < 5; i++)
        {
            StartingStats[i] = template.StartingStats[i];
            StatGrowthPerLevel[i] = template.StatGrowthPerLevel[i];
            AutoAllocationPatternPerLevel[i] = template.AutoAllocationPatternPerLevel[i];
        }
        EquipmentManager = new EquipmentManager();
    }

    // Returns stat gain array for given level increase
    public int[] GetStatGain(int levelGained)
    {
        int[] gain = new int[5];
        for (int i = 0; i < 5; i++)
        {
            gain[i] = StatGrowthPerLevel[i] * levelGained;
        }
        return gain;
    }

    // Returns a copy of auto allocation pattern, scaled to point amount
    public int[] GetAutoAllocation(int totalPoints)
    {
        int[] result = new int[5];
        int patternTotal = AutoAllocationPatternPerLevel.Sum();
        if (patternTotal <= 0) return result;

        for (int i = 0; i < 5; i++)
        {
            result[i] = Mathf.FloorToInt((float)AutoAllocationPatternPerLevel[i] / patternTotal * totalPoints);
        }

        // Distribute any leftover points (due to rounding)
        int allocated = result.Sum();
        int remaining = totalPoints - allocated;
        for (int i = 0; remaining > 0 && i < 5; i++)
        {
            result[i]++;
            remaining--;
        }

        return result;
    }

    public Skill CreateSkillObject(SkillTemplate template){
        if(template.type == SkillType.Attack || template.type == SkillType.AttackAOE){
            return new SkillAttack(template);
        }
        if(template.type == SkillType.Buff || template.type == SkillType.BuffSelf || template.type == SkillType.BuffAOE){
            return new SkillBuff(template);
        }
        if(template.type == SkillType.Heal || template.type == SkillType.HealAOE){
            return new SkillHeal(template);
        }
        if(template.type == SkillType.Magic || template.type == SkillType.MagicAOE){
            return new SkillMagic(template);
        }
        Debug.LogError("Skill type not found for skill: " + template.name + " type: " + template.type);
        return null;
    }
}