using System.Linq;
using UnityEngine;

public class BattleCharacter
{
    public string Name { get; private set; }
    public int Lv { get; private set; }
    public int CurrentEXP { get; private set; }
    public int RequiredEXP { get { return Util.GetExpForLevel(Lv); } }

    public Sprite Portrait { get; private set; }
    public Sprite Face { get; private set; }
    public Sprite BattlePortrait { get; private set; }

    public int SkillPointEarned { get; private set; }
    public int SkillPointSpent { get; private set; }
    public int SkillPointAvailable { get { return SkillPointEarned - SkillPointSpent; } }

    public int UpgradePointEarned { get; private set; }
    public int[] UpgradePointAllocation { get; private set; } // [Str, Mana, Stamina, Agi, Dex]
    public int UpgradePointAvailable { get { return UpgradePointEarned - UpgradePointAllocation.Sum(); } }
    public CharacterClass Class { get; private set; }
    public bool Unlocked {get; set;} = false;

    public BattleCharacterStat BattleStat
    {
        get
        {
            int[] total = new int[5];
            for (int i = 0; i < 5; i++)
            {
                total[i] = Class.StartingStats[i] + UpgradePointAllocation[i];
            }

            return new BattleCharacterStat(
                total[0], total[1], total[2], total[3], total[4]
            );
        }
    }

    public BaseStat BaseStat => BattleStat.ToBaseStat();

    public BattleCharacter(BattleCharacterTemplate template)
    {
        Name = template.CharacterName;
        Lv = template.StartingLv;
        CurrentEXP = 0;
        Portrait = template.Portrait;
        Face = template.Face;
        BattlePortrait = template.BattlePortrait;

        SkillPointEarned = template.StartingSkillPoints;
        SkillPointSpent = 0;

        UpgradePointEarned = template.StartingUpgradePoints;
        UpgradePointAllocation = new int[5]; // [Str, Mana, Stamina, Agi, Dex]

        Class = new CharacterClass(template.Class);
    }

    public bool GainEXP(int exp)
    {
        bool isLevelUp = false;
        // Check if the character is already at max level, if so, do not gain EXP
        if (Lv <= Constant.MaxLevel)
        {
            CurrentEXP += exp;
            // loop through level until the current EXP is less than the required EXP for the next level
            while (CurrentEXP >= Util.GetExpForLevel(Lv))
            {
                CurrentEXP -= Util.GetExpForLevel(Lv);
                LevelUp();
                isLevelUp = true;
            }
            if (CurrentEXP < 0)
            {
                CurrentEXP = 0;
            }
        }
        else
        {
            CurrentEXP = 0;
        }
        return isLevelUp;
    }

    public void LevelUp()
    {
        Lv++;
        UpgradePointEarned += 5;
        SkillPointEarned += 2;
    }

    public void AllocateUpgradePoint(int statIndex, int amount)
    {
        if (statIndex < 0 || statIndex >= 5) return;
        if (UpgradePointAvailable < amount) return;

        UpgradePointAllocation[statIndex] += amount;
    }

    public void SpendSkillPoint(int amount)
    {
        if (amount <= 0 || SkillPointSpent + amount > SkillPointEarned) return;
        SkillPointSpent += amount;
    }

    public void LearnSkill(Skill skill){
        if(skill.Price > GameController.Instance.money){
            return;
        }
        GameController.Instance.money -= skill.Price;
        skill.SkillLv++;
        SkillPointSpent++;
    }

}
