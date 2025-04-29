using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Skill")]
public class SkillTemplate : ScriptableObject
{
    public int id;
    public string skillName;
    public SkillType type;
    [TextArea(3, 10)]
    public string description;
    public Sprite icon;
    public float modifier;
    public int hitCount=1;
    public int mpCost;
    public int cooldown=0;
    public int requireLv;
    public int price;
    public int maxSkillLv;
    public int mpCostIncrease;
    public float modifierIncrease;
    public int requireLvIncrease;
    public int priceIncrease;

}
