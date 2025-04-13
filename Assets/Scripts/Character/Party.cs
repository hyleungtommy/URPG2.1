using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Party
{
    private List<BattleCharacter> allMembers;

    public Party(PartyTemplate template)
    {
        allMembers = new List<BattleCharacter>();

        for (int i = 0; i < template.Members.Length; i++)
        {
            var memberTemplate = template.Members[i];
            if (memberTemplate == null)
            {
                allMembers.Add(null);
                continue;
            }

            // Ensure a matching class is provided

            if (memberTemplate.Class == null)
            {
                Debug.LogWarning($"Missing class template for member index {i}");
                allMembers.Add(null);
                continue;
            }

            var character = new BattleCharacter(memberTemplate);
            allMembers.Add(character);

            if (memberTemplate.UnlockedAtStart)
            {
                character.Unlocked = true;
            }

        }
    }

    /// <summary> Returns only characters that are unlocked </summary>
    public List<BattleCharacter> GetUnlockedMembers()
    {
        return allMembers
            .Where(member => member != null && member.Unlocked)
            .ToList();
    }

    /// <summary> Returns all character slots (some may be null or locked) </summary>
    public List<BattleCharacter> GetAllMembers()
    {
        return allMembers;
    }

    /// <summary> Unlocks a character by index (if not already unlocked) </summary>
    public void UnlockMember(int index)
    {
        if (index < 0 || index >= allMembers.Count)
        {
            Debug.LogWarning($"Invalid index {index} for unlocking member.");
            return;
        }

        var character = allMembers[index];
        if (character != null && !character.Unlocked)
        {
            character.Unlocked = true;
        }
    }

    /// <summary> Unlocks all characters (for debugging or cheat modes) </summary>
    public void UnlockAllMembers()
    {
        foreach (var member in allMembers)
        {
            if (member != null)
                member.Unlocked = true;
        }
    }
}