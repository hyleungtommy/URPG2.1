public class CraftSkill
{
    public CraftSkillType Type { get; set; }
    public int Level { get; set; }
    public int CurrentExp { get; set; }
    public int RequiredExp { get { 
        if(Type == CraftSkillType.Alchemy){
            return Level >= Constant.AlchemyExpForEachLevel.Length ? 0 : Constant.AlchemyExpForEachLevel[Level - 1];
        }
        return Level >= Constant.CraftSkillRequiredExp.Length ? 0 : Constant.CraftSkillRequiredExp[Level - 1];
    } }

    public CraftSkill(CraftSkillType type){
        Type = type;
        Level = 1;
        CurrentExp = 0;
    }

    public void GainExp(int exp){
        if(Level >= Constant.CraftSkillRequiredExp.Length){
            return;
        }
        CurrentExp += exp;
        if(CurrentExp >= RequiredExp){
            Level++;
            CurrentExp = 0;
        }
    }
}

public enum CraftSkillType
{
    Mining,
    Forging,
    Hunting,
    Cooking,
    Alchemy,
    Smithing,
    ArcaneCrafting,
    ArcherCrafting,
    JewelryCrafting,
    Reinforcing,
    Enchanting
}