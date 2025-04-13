// <summary> Object to store battle rewards and related data </summary>
public class BattleReward {
    public int Money { get; set; } = 0;
    public int EXP { get; set; } = 0;
    public bool IsVictory { get; set; } = false;
    public bool[] isLevelUp { get; set; } = {false,false,false,false}; // Assuming 4 players in the party
}