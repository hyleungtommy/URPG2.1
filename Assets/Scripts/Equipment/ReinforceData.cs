public class ReinforceData{
    public ReinforceEquipmentType equipmentType;
    public int equipmentLv;//equipment's required level
    public int maxReinforceLevel; // max level an equipment can be reinforced
    public CraftRequirement[] requirements; // requirements to reinforce, including starting cost
    public int costIncreaseEveryXLevel; // cost increase every x level
    public int costIncrement; // every x level, cost will increase by costIncrement
    public ReinforceData(ReinforceDataTemplate template){
        this.equipmentType = template.equipmentType;
        this.equipmentLv = template.equipmentLv;
        this.maxReinforceLevel = template.maxReinforceLevel;
        this.requirements = new CraftRequirement[template.requirements.Length];
        for(int i = 0; i < template.requirements.Length; i++){
            this.requirements[i] = new CraftRequirement(template.requirements[i]);
        }
        this.costIncreaseEveryXLevel = template.costIncreaseEveryXLevel;
        this.costIncrement = template.costIncrement;
    }
}