using UnityEngine;

[CreateAssetMenu(fileName = "NewPartyTemplate", menuName = "RPG/Party")]
public class PartyTemplate : ScriptableObject
{
    // Array to hold up to 8 battle character templates
    public BattleCharacterTemplate[] Members = new BattleCharacterTemplate[8];
}