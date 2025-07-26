using System.Collections.Generic;
using System.Linq;
public static class Game
{
    public static int Money { get; set; }
    public static Party Party { get; set; }
    public static StorageSystem Inventory { get; set; }
    public static GameState State { get; set; } = GameState.Idle;
    public static MapTemplate MapPanelSelectedMap { get; set; }
    public static Map CurrentMap { get; set; }
    public static Map.MapMode CurrentMapMode { get; set; }
    public static QuestManager QuestManager { get; set; }
    public static QuestBoardMode QuestBoardMode { get; set; } = QuestBoardMode.Available;

    public static void Initialize(PartyTemplate partyTemplate)
    {
        Money = 0;
        Party = new Party(partyTemplate);
        Inventory = new StorageSystem(Constant.InventorySize);
        State = GameState.Idle;
        QuestManager = new QuestManager();
    }

    // Utility methods for money
    public static void AddMoney(int amount)
    {
        Money += amount;
    }

    public static bool SpendMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            return true;
        }
        return false;
    }


}

public enum GameState
{
    Dialog,
    OpenUI,
    Idle,
    Battle
}