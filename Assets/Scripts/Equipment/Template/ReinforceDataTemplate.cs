using UnityEngine;
using System;
[CreateAssetMenu(fileName = "New ReinforceData", menuName = "Equipment/ReinforceData")]
public class ReinforceDataTemplate : ScriptableObject
{
    //If an equipment matches the criteria, it will use the following reinforce rule
    [Header("Criteria")]
    public ReinforceEquipmentType equipmentType;
    public int equipmentLv;//equipment's required level
    [Header("Reinforce Rule")]
    public int maxReinforceLevel; // max level an equipment can be reinforced
    public CraftRequirementTemplate[] requirements; // requirements to reinforce, including starting cost
    public int costIncreaseEveryXLevel = 1; // cost increase every x level
    public int costIncrement = 1; // every x level, cost will increase by costIncrement

}


[Serializable]
public enum ReinforceEquipmentType
{
    WeaponMelee, WeaponRanged, WeaponMagic, LightArmor, MediumArmor, HeavyArmor
}