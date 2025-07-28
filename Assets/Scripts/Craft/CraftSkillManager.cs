public class CraftSkillManager{
    public CraftSkill Mining { get; set; }
    public CraftSkill Forging { get; set; }
    public CraftSkill Hunting { get; set; }
    public CraftSkill Cooking { get; set; }
    public CraftSkill Alchemy { get; set; }
    public CraftSkill Smithing { get; set; }
    public CraftSkill ArcaneCrafting { get; set; }
    public CraftSkill ArcherCrafting { get; set; }
    public CraftSkill JewelryCrafting { get; set; }
    public CraftSkill Reinforcing { get; set; }
    public CraftSkill Enchanting { get; set; }
    public int AvailableExploreTeam { get; set; }

    public CraftSkillManager(){
        Mining = new CraftSkill(CraftSkillType.Mining);
        Forging = new CraftSkill(CraftSkillType.Forging);
        Hunting = new CraftSkill(CraftSkillType.Hunting);
        Cooking = new CraftSkill(CraftSkillType.Cooking);
        Alchemy = new CraftSkill(CraftSkillType.Alchemy);
        Smithing = new CraftSkill(CraftSkillType.Smithing);
        ArcaneCrafting = new CraftSkill(CraftSkillType.ArcaneCrafting);
        ArcherCrafting = new CraftSkill(CraftSkillType.ArcherCrafting);
        JewelryCrafting = new CraftSkill(CraftSkillType.JewelryCrafting);
        Reinforcing = new CraftSkill(CraftSkillType.Reinforcing);
        Enchanting = new CraftSkill(CraftSkillType.Enchanting);
        AvailableExploreTeam = 0;
    }

    public void AddExperience(CraftSkillType skillType, int amount){
        switch(skillType){
            case CraftSkillType.Mining:
                Mining.GainExp(amount);
                break;
            case CraftSkillType.Forging:
                Forging.GainExp(amount);
                break;
            case CraftSkillType.Hunting:
                Hunting.GainExp(amount);
                break;
            case CraftSkillType.Cooking:
                Cooking.GainExp(amount);
                break;
            case CraftSkillType.Alchemy:
                Alchemy.GainExp(amount);
                break;
            case CraftSkillType.Smithing:
                Smithing.GainExp(amount);
                break;
            case CraftSkillType.ArcaneCrafting:
                ArcaneCrafting.GainExp(amount);
                break;
            case CraftSkillType.ArcherCrafting:
                ArcherCrafting.GainExp(amount);
                break;
            case CraftSkillType.JewelryCrafting:
                JewelryCrafting.GainExp(amount);
                break;
            case CraftSkillType.Reinforcing:
                Reinforcing.GainExp(amount);
                break;
            case CraftSkillType.Enchanting:
                Enchanting.GainExp(amount);
                break;
        }
    }
}