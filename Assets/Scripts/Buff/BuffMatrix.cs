public class BuffMatrix{
    public float ATK { get; set; } = 1;   // Physical Attack
    public float DEF { get; set; } = 1;  // Physical Defense
    public float MATK { get; set; } = 1;  // Magical Attack
    public float MDEF { get; set; } = 1;  // Magical Defense
    public float AGI { get; set; } = 1;   // Agility
    public float DEX { get; set; } = 1;   // Dexterity

    public BuffMatrix(Buff[] buffs){
        foreach (Buff buff in buffs){
            switch (buff.buffType){
                case BuffType.ATK:
                    ATK += buff.value / 100;
                    break;
                case BuffType.DEF:
                    DEF += buff.value / 100;
                    break;
                case BuffType.MATK:
                    MATK += buff.value / 100;
                    break;
                case BuffType.MDEF:
                    MDEF += buff.value / 100;
                    break;
                case BuffType.AGI:
                    AGI += buff.value / 100;
                    break;
                case BuffType.DEX:
                    DEX += buff.value / 100;
                    break;
            }
        }
    }
}