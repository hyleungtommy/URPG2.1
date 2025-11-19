using UnityEngine;

public class BattlePlayerEntity : BattleEntity
{
    public Sprite BattlePortrait { get; private set; }
    public Skill[] SkillList { get; private set; }

    //TODO: element resistance from equipment
    public BattlePlayerEntity(BattleCharacter character, BattleManager manager) : base(character.Name, character.Face, character.BaseStat, manager, new ElementResistance())
    {
        BattlePortrait = character.BattlePortrait;
        SkillList = character.CharacterClass.LearntSkillsList;
    }

    public override void TakeTurn()
    {
        // TODO: Display player action UI (Normal Attack / Escape)
        Debug.Log($"{Name}'s turn - waiting for player input...");
    }
}