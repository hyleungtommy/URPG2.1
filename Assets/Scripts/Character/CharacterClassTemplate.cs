using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterClass", menuName = "RPG/Character Class")]
public class CharacterClassTemplate : ScriptableObject
{
    public string ClassName;

    // Starting stats (applied at level 1 or class change)
    public int[] StartingStats = new int[5];

    // Stat gains per level up
    public int[] StatGrowthPerLevel = new int[5];

    // Auto allocation priority, sum should be 5
    // Determines how points are auto-spent: [Str, Mana, Stamina, Agi, Dex]
    public int[] AutoAllocationPatternPerLevel = new int[5];
}