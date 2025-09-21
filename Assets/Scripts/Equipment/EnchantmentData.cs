public class EnchantmentData
{
    public int EquipmentRequireLevel {get; private set;}
    public CraftRequirement[] Requirements {get; private set;}
    public EnchantmentData(EnchantmentDataTemplate template){
        this.EquipmentRequireLevel = template.equipmentRequireLevel;
        this.Requirements = new CraftRequirement[template.requirements.Length];
        for(int i = 0; i < template.requirements.Length; i++){
            this.Requirements[i] = new CraftRequirement(template.requirements[i]);
        }
    }
}