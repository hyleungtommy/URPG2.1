using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Static utility class responsible for loading and preparing battle scenes.
/// Manages the transition from world map to battle scene, including party and enemy setup.
/// </summary>
public static class BattleSceneLoader
{
    /// <summary>
    /// The current party of players that will participate in the battle.
    /// Limited to a maximum of 4 unlocked characters.
    /// </summary>
    public static List<BattleCharacter> PlayerParty { get; set; }
    
    /// <summary>
    /// The list of enemy templates that will be spawned in the current battle.
    /// Generated based on the current map and zone.
    /// </summary>
    public static List<EnemyTemplate> EnemiesToSpawn { get; private set; }
    
    /// <summary>
    /// The current map instance containing battle configuration and zone information.
    /// </summary>
    public static Map CurrentMap { get; set; }
    
    /// <summary>
    /// Determines if the current battle is a boss battle.
    /// Returns true if in Zone mode and on the final zone of the map.
    /// </summary>
    public static bool IsBossBattle 
    { 
        get 
        { 
            return CurrentMap?.Mode == Map.MapMode.Zone && 
                   CurrentMap?.CurrentZone == CurrentMap?.Template?.NumberOfZones; 
        } 
    }

    /// <summary>
    /// Loads a battle scene with the specified map configuration.
    /// Sets up the current map, generates enemies, and prepares the player party.
    /// </summary>
    /// <param name="map">The map configuration containing enemy and zone information</param>
    /// <exception cref="System.ArgumentNullException">Thrown when map is null</exception>
    public static void LoadBattleScene(Map map)
    {
        if (map == null)
        {
            throw new System.ArgumentNullException(nameof(map), "Map cannot be null");
        }

        if (GameController.Instance?.party == null)
        {
            Debug.LogError("GameController or party is null. Cannot load battle scene.");
            return;
        }

        CurrentMap = map;
        EnemiesToSpawn = CurrentMap.GenerateEnemies();

        // Get up to 4 unlocked characters
        PlayerParty = GameController.Instance.party.GetUnlockedMembers();
        if (PlayerParty.Count > 4)
            PlayerParty = PlayerParty.GetRange(0, 4);

        SceneManager.LoadScene("Battle");
    }

    /// <summary>
    /// Loads a test battle scene with specific map and party configurations.
    /// This method is intended for testing and debugging purposes only.
    /// </summary>
    /// <param name="testMap">The map template to use for the test battle</param>
    /// <param name="testPartyTemplate">The party template to use for the test battle</param>
    /// <param name="testBossBattle">If true, forces the battle to be a boss battle regardless of zone</param>
    /// <exception cref="System.ArgumentNullException">Thrown when testMap or testPartyTemplate is null</exception>
    public static void LoadTestBattleScene(MapTemplate testMap, PartyTemplate testPartyTemplate, bool testBossBattle)
    {
        if (testMap == null)
        {
            throw new System.ArgumentNullException(nameof(testMap), "Test map cannot be null");
        }

        if (testPartyTemplate == null)
        {
            throw new System.ArgumentNullException(nameof(testPartyTemplate), "Test party template cannot be null");
        }

        CurrentMap = new Map(testMap, Map.MapMode.Zone);
        if (testBossBattle)
        {
            CurrentMap.TestBossBattle();
        }
        EnemiesToSpawn = CurrentMap.GenerateEnemies();

        PlayerParty = new Party(testPartyTemplate).GetUnlockedMembers();
        if (PlayerParty.Count > 4)
            PlayerParty = PlayerParty.GetRange(0, 4);
    }

    /// <summary>
    /// Clears all static data to prevent memory leaks and ensure clean state for next battle.
    /// Should be called when transitioning away from battle scenes.
    /// </summary>
    public static void ClearBattleData()
    {
        PlayerParty?.Clear();
        PlayerParty = null;
        EnemiesToSpawn?.Clear();
        EnemiesToSpawn = null;
        CurrentMap = null;
    }
}
