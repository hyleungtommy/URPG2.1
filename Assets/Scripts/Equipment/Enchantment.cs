public class Enchantment
{
    public int Id {get; private set;}
    public string Name {get; private set;}
    public EnchantmentType Type {get; private set;}
    public int Level {get; private set;}
    public int Value {get; private set;}
    public string Description {get; private set;}
    public Enchantment(int id, int level){
        this.Id = id;
        this.Name = DBManager.Instance.GetEnchantmentTemplate(id).EnchantmentName;
        this.Type = DBManager.Instance.GetEnchantmentTemplate(id).type;
        this.Level = level;
        this.Value = DBManager.Instance.GetEnchantmentTemplate(id).startValue + (level - 1) * DBManager.Instance.GetEnchantmentTemplate(id).valueIncreasePerLevel;
        this.Description = DBManager.Instance.GetEnchantmentTemplate(id).description.Replace("%value%", Value.ToString());
    }

    public EnchantmentStat GetStat()
    {
        if(Type == EnchantmentType.HP){
            return new EnchantmentStat((float)Value/100f, 0, 0, 0, 0, 0, 0, 0);
        }
        else if(Type == EnchantmentType.MP){
            return new EnchantmentStat(0, (float)Value/100f, 0, 0, 0, 0, 0, 0);
        }
        else if(Type == EnchantmentType.ATK){
            return new EnchantmentStat(0, 0, (float)Value/100f, 0, 0, 0, 0, 0);
        }
        else if(Type == EnchantmentType.DEF){
            return new EnchantmentStat(0, 0, 0, (float)Value/100f, 0, 0, 0, 0);
        }
        else if(Type == EnchantmentType.MATK){
            return new EnchantmentStat(0, 0, 0, 0, (float)Value/100f, 0, 0, 0);
        }
        else if(Type == EnchantmentType.MDEF){
            return new EnchantmentStat(0, 0, 0, 0, 0, (float)Value/100f, 0, 0);
        }
        else if(Type == EnchantmentType.AGI){
            return new EnchantmentStat(0, 0, 0, 0, 0, 0, (float)Value/100f, 0);
        }
        else if(Type == EnchantmentType.DEX){
            return new EnchantmentStat(0, 0, 0, 0, 0, 0, 0, (float)Value/100f);
        }
        return new EnchantmentStat(0, 0, 0, 0, 0, 0, 0, 0);
    }

}