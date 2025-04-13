using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class BattleCharacterTests
{
    CharacterClassTemplate CreateMockClassTemplate()
    {
        var classTemplate = ScriptableObject.CreateInstance<CharacterClassTemplate>();
        classTemplate.ClassName = "Warrior";
        classTemplate.StartingStats = new int[] { 5, 3, 4, 2, 1 };  // Example
        classTemplate.StatGrowthPerLevel = new int[] { 1, 0, 2, 0, 1 };
        classTemplate.AutoAllocationPatternPerLevel = new int[] { 2, 0, 2, 1, 0 };
        return classTemplate;
    }

    BattleCharacterTemplate CreateMockCharacterTemplate()
    {
        var charTemplate = ScriptableObject.CreateInstance<BattleCharacterTemplate>();
        charTemplate.CharacterName = "TestHero";
        charTemplate.StartingLv = 1;
        charTemplate.Portrait = null;
        charTemplate.Face = null;
        charTemplate.StartingSkillPoints = 0;
        charTemplate.StartingUpgradePoints = 0;
        charTemplate.UnlockedAtStart = true;
        return charTemplate;
    }

    [Test]
    public void BattleCharacter_InitialStats_CorrectlyCalculated()
    {
        var classTemplate = CreateMockClassTemplate();
        var charTemplate = CreateMockCharacterTemplate();
        charTemplate.Class = classTemplate;

        var character = new BattleCharacter(charTemplate);

        var stat = character.BattleStat;

        Assert.AreEqual(5, stat.Strength);
        Assert.AreEqual(3, stat.Mana);
        Assert.AreEqual(4, stat.Stamina);
        Assert.AreEqual(2, stat.Agility);
        Assert.AreEqual(1, stat.Dexterity);
    }

    [Test]
    public void BattleCharacter_LevelUp_GivesUpgradeAndSkillPoints()
    {
        var classTemplate = CreateMockClassTemplate();
        var charTemplate = CreateMockCharacterTemplate();
        charTemplate.Class = classTemplate;
        
        var character = new BattleCharacter(charTemplate);
        character.GainEXP(1000); // force multiple level-ups

        Assert.Greater(character.Lv, 1);
        Assert.Greater(character.SkillPointEarned, 0);
        Assert.Greater(character.UpgradePointEarned, 0);
    }

    [Test]
    public void BattleCharacter_AutoAllocate_RespectsPattern()
    {
        var classTemplate = CreateMockClassTemplate();
        var classInstance = new CharacterClass(classTemplate);
        int[] allocation = classInstance.GetAutoAllocation(5);

        // Based on pattern {2,0,2,1,0} total 5 weights
        // Expect: Str=2, Stamina=2, Agi=1
        Assert.AreEqual(2, allocation[0]);
        Assert.AreEqual(2, allocation[2]);
        Assert.AreEqual(1, allocation[3]);
    }
}
