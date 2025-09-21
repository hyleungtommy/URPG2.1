using UnityEngine;

[CreateAssetMenu(fileName = "NewBattleCharacter", menuName = "RPG/Battle Character")]
public class BattleCharacterTemplate : ScriptableObject
{
    public string CharacterName;  // Character name
    public int StartingLv;  // Starting level
    public Sprite Portrait;  // Character portrait image
    public Sprite Face;  // Character face image
    public Sprite BattlePortrait;  // Character battle portrait image
    public int StartingSkillPoints;  // Initial skill points
    public int StartingUpgradePoints;  // Initial upgrade points
    public bool UnlockedAtStart;  // If the character is available from the start
    public CharacterClassTemplate Class;  // Character class template
}