using UnityEngine;

public class BattlePlayerEntity : BattleEntity
{
    public Sprite BattlePortrait { get; private set; }

    public BattlePlayerEntity(BattleCharacter character, BattleManager manager) : base(character.Name, character.Face, character.BaseStat, manager)
    {
        BattlePortrait = character.BattlePortrait;
    }

    public override void TakeTurn()
    {
        // TODO: Display player action UI (Normal Attack / Escape)
        Debug.Log($"{Name}'s turn - waiting for player input...");
    }
}