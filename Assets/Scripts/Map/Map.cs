using System.Linq;
using System.Collections.Generic;
using UnityEngine;
public class Map
{
    public enum MapMode
    {
        Zone,
        Explore
    }

    public MapTemplate Template { get; private set; }
    public MapMode Mode { get; private set; }
    public int CurrentZone { get; private set; } = 1;
    public bool HasBeenCompleted { get; private set; } = false;
    public bool IsLastZone { get { return CurrentZone >= Template.NumberOfZones; } }
    public Sprite Background { get; private set; }
    public Map(MapTemplate template, MapMode mode)
    {
        Template = template;
        Mode = mode;
        Background = template.BattleBackground;
    }

    /// <summary>
    /// Advance to the next zone. Returns true if player reached the boss zone.
    /// </summary>
    public void ProgressZone()
    {
        if (CurrentZone < Template.NumberOfZones)
        {
            CurrentZone++;
        }
    }

    public void ResetZoneProgress(){
        //todo: save HasBeenCompleted so that when next time this map is loaded, it can be used to determine if the player has completed the map
        HasBeenCompleted = true;
        CurrentZone = 1;
    }

    /// <summary>
    /// Returns true if this is a save zone (every 5 zones)
    /// </summary>
    public bool IsSaveZone()
    {
        return Mode == MapMode.Zone && CurrentZone % 5 == 0;
    }


    public List<EnemyTemplate> GenerateEnemies()
    {
        Debug.Log("Mode: " + Mode + " CurrentZone: " + CurrentZone + " NumberOfZones: " + Template.NumberOfZones);
        if (Mode == MapMode.Zone && CurrentZone == Template.NumberOfZones)
        {
            
            return new List<EnemyTemplate> { Template.Boss };
        }
        else
        {
            return GenerateRandomEnemies();
        }
    }

    private List<EnemyTemplate> GenerateRandomEnemies()
    {
        List<EnemyTemplate> enemies = new List<EnemyTemplate>();

        if (Template.Enemies == null || Template.Enemies.Length == 0)
            return enemies;

        int count = Random.Range(1, 6); // 1 to 5 enemies

        for (int i = 0; i < count; i++)
        {
            enemies.Add(PickEnemyByWeight());
        }

        return enemies;
    }

    /// <summary>
    /// Gets a random enemy based on weights.
    /// </summary>
    private EnemyTemplate PickEnemyByWeight()
    {
        int totalWeight = 0;
        foreach (int weight in Template.AppearanceWeights)
            totalWeight += weight;

        int roll = Random.Range(0, totalWeight);
        int cumulative = 0;

        for (int i = 0; i < Template.Enemies.Length; i++)
        {
            cumulative += Template.AppearanceWeights[i];
            if (roll < cumulative)
                return Template.Enemies[i];
        }

        return Template.Enemies[Template.Enemies.Length - 1];
    }

    /// <summary>
    /// Gets the stat multiplier for current zone (only in Zone mode).
    /// </summary>
    public float GetZoneStatMultiplier()
    {
        if (Mode != MapMode.Zone || CurrentZone == Template.NumberOfZones)
            return 1f; // No boost for boss zone

        // Example: each zone adds 10% power → 1.0, 1.1, 1.2, ...
        return 1f + (CurrentZone - 1) * 0.1f;
    }

    public void TestBossBattle(){
        Mode = MapMode.Zone;
        CurrentZone = Template.NumberOfZones;
    }
}
