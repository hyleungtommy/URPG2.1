public class CraftRecipe{
    public CraftSkillType craftSkillType;
    public CraftRequirement[] requirements;
    public int requireSkillLevel = 1;
    public int resultAmount = 1;
    public Item resultItem;

    public CraftRecipe(CraftRecipeTemplate template){
        this.requireSkillLevel = template.requireSkillLevel;
        this.resultAmount = template.resultAmount;
        this.resultItem = template.resultItem.GetItem();
        this.requirements = new CraftRequirement[template.requirements.Length];
        for(int i = 0; i < template.requirements.Length; i++){
            this.requirements[i] = new CraftRequirement(template.requirements[i]);
        }
        this.craftSkillType = GetCraftSkillType(template.type);
    }

    private CraftSkillType GetCraftSkillType(CraftTemplateType type){
        if(type == CraftTemplateType.Alchemy){
            return CraftSkillType.Alchemy;
        }
        if (this.resultItem is Weapon weapon) {
            switch (weapon.WeaponType) {
                case WeaponType.Wand:
                case WeaponType.Artifact:
                case WeaponType.Staff:
                    return CraftSkillType.ArcaneCrafting;
                case WeaponType.Bow:
                case WeaponType.Dagger:
                    return CraftSkillType.ArcherCrafting;
                case WeaponType.Sword:
                case WeaponType.Axe:
                case WeaponType.Shield:
                    return CraftSkillType.Smithing;
            }
        } else if (resultItem is Armor armor) {
            switch (armor.ArmorCategory) {
                case ArmorCategory.Light:
                    return CraftSkillType.ArcaneCrafting;
                case ArmorCategory.Heavy:
                    return CraftSkillType.Smithing;
                case ArmorCategory.Medium:
                    return CraftSkillType.ArcherCrafting;
            }
        }
        return CraftSkillType.Smithing;
    }

}

public class CraftRequirement{
    public Item item;
    public int amount;

    public CraftRequirement(CraftRequirementTemplate template){
        this.item = template.item.GetItem();
        this.amount = template.amount;
    }
}