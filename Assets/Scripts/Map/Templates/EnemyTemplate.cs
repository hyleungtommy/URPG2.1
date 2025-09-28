using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyTemplate", menuName = "RPG/Enemy")]
public class EnemyTemplate : ScriptableObject, IQuestRequirement
{
    [Header("Basic Info")]
    public string ID;
    public string EnemyName;
    public Sprite EnemyImage;

    [Header("Base Stats")]
    public int HP;
    public int MP;
    public int ATK;
    public int DEF;
    public int MATK;
    public int MDEF;
    public int AGI;
    public int DEX;

    [Header("Drop Rewards")]
    public int DropExp;
    public int DropMoney;

    [Header("Elemental Resistance (%)")]
    [Range(-100, 100)] public int Fire;
    [Range(-100, 100)] public int Ice;
    [Range(-100, 100)] public int Lightning;
    [Range(-100, 100)] public int Earth;
    [Range(-100, 100)] public int Wind;
    [Range(-100, 100)] public int Light;
    [Range(-100, 100)] public int Dark;

    [Header("Physical Resistance (%)")]
    [Range(-100, 100)] public int Hit;
    [Range(-100, 100)] public int Slash;

    public BaseStat ToBaseStat() => new BaseStat(HP, MP, ATK, DEF, MATK, MDEF, AGI, DEX);
    public ElementResistance ToElementResistance() => new ElementResistance(Fire, Ice, Lightning, Earth, Wind, Light, Dark);
}