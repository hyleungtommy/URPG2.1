using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class BattleSceneLoader
{
    public static List<BattleCharacter> PlayerParty { get; set; }
    public static List<EnemyTemplate> EnemiesToSpawn { get; private set; }
    public static Map CurrentMap { get; set; }
    public static bool IsBossBattle { get { return CurrentMap.Mode == Map.MapMode.Zone && CurrentMap.CurrentZone == CurrentMap.Template.NumberOfZones; } }

    public static void LoadBattleScene(Map map)
    {
        CurrentMap = map;
        EnemiesToSpawn = CurrentMap.GenerateEnemies();

        // Get up to 4 unlocked characters
        PlayerParty = GameController.Instance.party.GetUnlockedMembers();
        if (PlayerParty.Count > 4)
            PlayerParty = PlayerParty.GetRange(0, 4);

        SceneManager.LoadScene("Battle");
    }

    // This method is used for testing purposes only
    // It allows you to load a specific map and party template without going through the UI
    public static void LoadTestBattleScene(MapTemplate testMap, PartyTemplate testPartyTemplate, bool testBossBattle){
        CurrentMap = new Map(testMap, Map.MapMode.Zone);
        if (testBossBattle){
            CurrentMap.TestBossBattle();
        }
        EnemiesToSpawn = CurrentMap.GenerateEnemies();

        PlayerParty = new Party(testPartyTemplate).GetUnlockedMembers();
        if (PlayerParty.Count > 4)
            PlayerParty = PlayerParty.GetRange(0, 4);
    }
}
