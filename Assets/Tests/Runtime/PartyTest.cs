using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PartyTests
{
    // Helper to create a dummy BattleCharacterTemplate with class
    BattleCharacterTemplate CreateCharacterTemplate(string name, bool unlockedAtStart)
    {
        var template = ScriptableObject.CreateInstance<BattleCharacterTemplate>();
        template.CharacterName = name;
        template.UnlockedAtStart = unlockedAtStart;

        // Add a dummy class template
        var classTemplate = ScriptableObject.CreateInstance<CharacterClassTemplate>();
        classTemplate.ClassName = "Warrior";
        classTemplate.StartingStats = new int[5];
        classTemplate.StatGrowthPerLevel = new int[5];
        classTemplate.AutoAllocationPatternPerLevel = new int[5];

        template.Class = classTemplate;
        return template;
    }

    [Test]
    public void Party_InitializesWithCorrectUnlockedMembers()
    {
        var partyTemplate = ScriptableObject.CreateInstance<PartyTemplate>();
        partyTemplate.Members = new BattleCharacterTemplate[8];

        // Add 3 members, 2 unlocked
        partyTemplate.Members[0] = CreateCharacterTemplate("Alice", true);
        partyTemplate.Members[1] = CreateCharacterTemplate("Bob", false);
        partyTemplate.Members[2] = CreateCharacterTemplate("Cecil", true);

        var party = new Party(partyTemplate);

        var unlocked = party.GetUnlockedMembers();
        Assert.AreEqual(2, unlocked.Count);
        Assert.IsTrue(unlocked.Exists(c => c.Name == "Alice"));
        Assert.IsTrue(unlocked.Exists(c => c.Name == "Cecil"));
    }

    [Test]
    public void Party_CanUnlockSpecificMember()
    {
        var partyTemplate = ScriptableObject.CreateInstance<PartyTemplate>();
        partyTemplate.Members = new BattleCharacterTemplate[8];

        partyTemplate.Members[0] = CreateCharacterTemplate("Alice", false);
        var party = new Party(partyTemplate);

        Assert.AreEqual(0, party.GetUnlockedMembers().Count);

        party.UnlockMember(0);
        var unlocked = party.GetUnlockedMembers();
        Assert.AreEqual(1, unlocked.Count);
        Assert.AreEqual("Alice", unlocked[0].Name);
    }

    [Test]
    public void Party_UnlockAllMembers_WorksCorrectly()
    {
        var partyTemplate = ScriptableObject.CreateInstance<PartyTemplate>();
        partyTemplate.Members = new BattleCharacterTemplate[3];
        for (int i = 0; i < 3; i++)
            partyTemplate.Members[i] = CreateCharacterTemplate($"Char{i}", false);

        var party = new Party(partyTemplate);
        Assert.AreEqual(0, party.GetUnlockedMembers().Count);

        party.UnlockAllMembers();
        var unlocked = party.GetUnlockedMembers();
        Assert.AreEqual(3, unlocked.Count);
    }

    [Test]
    public void Party_GetAllMembers_ReturnsCorrectCount()
    {
        var partyTemplate = ScriptableObject.CreateInstance<PartyTemplate>();
        partyTemplate.Members = new BattleCharacterTemplate[8];
        partyTemplate.Members[0] = CreateCharacterTemplate("Alice", true);
        partyTemplate.Members[3] = CreateCharacterTemplate("Bob", false);

        var party = new Party(partyTemplate);
        var all = party.GetAllMembers();

        Assert.AreEqual(8, all.Count);
        Assert.IsNotNull(all[0]);
        Assert.IsNotNull(all[3]);
        Assert.IsNull(all[1]); // skipped slots
        Assert.IsNull(all[2]);
    }
}