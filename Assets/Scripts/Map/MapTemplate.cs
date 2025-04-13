using UnityEngine;

[CreateAssetMenu(fileName = "NewMapTemplate", menuName = "RPG/Map")]
public class MapTemplate : ScriptableObject
{
    [Header("Map Info")]
    public string MapName;
    [TextArea(2, 5)]
    public string Description;
    public Sprite PreviewImage;
    public Sprite BattleBackground;

    [Header("Recommended Level Range")]
    public int RecommendedMinLevel;
    public int RecommendedMaxLevel;

    [Header("Enemy Settings")]
    public EnemyTemplate[] Enemies;             // Enemies for regular zones
    public int[] AppearanceWeights;             // Matching spawn weights
    public EnemyTemplate Boss;                  // Boss enemy for final zone
    public int NumberOfZones;                   // For Zone Mode only
}