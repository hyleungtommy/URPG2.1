using UnityEngine;

[CreateAssetMenu(fileName = "QuestTemplate", menuName = "Quest/New Quest")]
public class QuestTemplate : ScriptableObject{
    [Header("Basic Info")]
    public int Id;
    public string QuestName;
    public QuestType QuestType;
    [TextArea(3, 10)]
    public string Description;
    public int RequireLevel;
    public int MaxLevel;
    [Header("Reward")]
    public int RewardExp;
    public int RewardMoney;

    [Header("Requirements")]
    public QuestRequirementTemplate[] Requirements = new QuestRequirementTemplate[1];
}