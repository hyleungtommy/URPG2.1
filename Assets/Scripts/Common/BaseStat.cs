using System;

public class BaseStat
{
    public int HP { get; set; }    // Health Points
    public int MP { get; set; }    // Mana Points
    public int ATK { get; set; }   // Physical Attack
    public int DEF { get; set; }   // Physical Defense
    public int MATK { get; set; }  // Magical Attack
    public int MDEF { get; set; }  // Magical Defense
    public int AGI { get; set; }   // Agility
    public int DEX { get; set; }   // Dexterity

    // Constructor to initialize stats
    public BaseStat(int hp, int mp, int atk, int def, int matk, int mdef, int agi, int dex)
    {
        HP = hp;
        MP = mp;
        ATK = atk;
        DEF = def;
        MATK = matk;
        MDEF = mdef;
        AGI = agi;
        DEX = dex;
    }

    public BaseStat Add(BaseStat stat){
        return new BaseStat(HP + stat.HP, MP + stat.MP, ATK + stat.ATK, DEF + stat.DEF, MATK + stat.MATK, MDEF + stat.MDEF, AGI + stat.AGI, DEX + stat.DEX);
    }

    public BaseStat Multiply(BuffMatrix buffMatrix){
        return new BaseStat(HP , MP , (int)Math.Round(ATK * buffMatrix.ATK), (int)Math.Round(DEF * buffMatrix.DEF), (int)Math.Round(MATK * buffMatrix.MATK), (int)Math.Round(MDEF * buffMatrix.MDEF), (int)Math.Round(AGI * buffMatrix.AGI), (int)Math.Round(DEX * buffMatrix.DEX));
    }

    public BaseStat Add(EnchantmentStat enchantmentStat)
    {
        return new BaseStat((int)(HP * (1 + enchantmentStat.HP)), (int)(MP * (1 + enchantmentStat.MP)), (int)(ATK * (1 + enchantmentStat.ATK)), (int)(DEF * (1 + enchantmentStat.DEF)), (int)(MATK * (1 + enchantmentStat.MATK)), (int)(MDEF * (1 + enchantmentStat.MDEF)), (int)(AGI * (1 + enchantmentStat.AGI)), (int)(DEX * (1 + enchantmentStat.DEX)));
    }
}