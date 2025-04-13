public class BattleCharacterStat
{
    public int Strength { get; set; }
    public int Mana { get; set; }
    public int Stamina { get; set; }
    public int Agility { get; set; }
    public int Dexterity { get; set; }

    public BattleCharacterStat(int strength, int mana, int stamina, int agility, int dexterity)
    {
        Strength = strength;
        Mana = mana;
        Stamina = stamina;
        Agility = agility;
        Dexterity = dexterity;
    }

    public BaseStat ToBaseStat()
    {
        int hp = (Strength * 5) + (Stamina * 20);
        int mp = Mana * 5;
        int atk = Strength * 4;
        int def = (Strength * 2) + (Stamina * 4);
        int matk = Mana * 4;
        int mdef = (Mana * 2) + (Stamina * 4);
        int agi = Agility;
        int dex = Dexterity;

        return new BaseStat(hp, mp, atk, def, matk, mdef, agi, dex);
    }
}