public class EnchantmentStat
{
    public float HP {get; private set;}
    public float MP {get; private set;}
    public float ATK {get; private set;}
    public float DEF {get; private set;}
    public float MATK {get; private set;}
    public float MDEF {get; private set;}
    public float AGI {get; private set;}
    public float DEX {get; private set;}

    public EnchantmentStat(){
        this.HP = 0;
        this.MP = 0;
        this.ATK = 0;
        this.DEF = 0;
        this.MATK = 0;
        this.MDEF = 0;
        this.AGI = 0;
        this.DEX = 0;
    }

    public EnchantmentStat(float hp, float mp, float atk, float def, float matk, float mdef, float agi, float dex){
        this.HP = hp;
        this.MP = mp;
        this.ATK = atk;
        this.DEF = def;
        this.MATK = matk;
        this.MDEF = mdef;
        this.AGI = agi;
        this.DEX = dex;
    }

    public EnchantmentStat Add(EnchantmentStat stat)
    {
        return new EnchantmentStat(HP + stat.HP, MP + stat.MP, ATK + stat.ATK, DEF + stat.DEF, MATK + stat.MATK, MDEF + stat.MDEF, AGI + stat.AGI, DEX + stat.DEX);
    }
}