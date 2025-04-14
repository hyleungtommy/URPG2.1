using UnityEngine;

// Ensure this script runs earlier in the execution order
[DefaultExecutionOrder(-1000)]
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Global Game State")]
    public int money;
    public Party party;
    public StorageSystem Inventory {get;private set;}

    [Header("Setup Templates")]
    public PartyTemplate StartingPartyTemplate;
    [Header("Map UI")]
    [SerializeField] MapTemplate testMap;
    [SerializeField] MapPanel mapPanel;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        InitializeGame();
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            UIController.Instance.ToggleUIScene("Status");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            OpenMapPanel(testMap);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIController.Instance.ToggleUIScene("Inventory");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            UIController.Instance.ToggleUIScene("Shop");
        }
    }

    private void InitializeGame()
    {
        // Initialize party
        if (StartingPartyTemplate != null)
        {
            party = new Party(StartingPartyTemplate);
        }
        else
        {
            Debug.LogError("No StartingPartyTemplate assigned to GameController!");
        }

        // Starting money (optional)
        money = Constant.StartMoney; // or load from save data
        mapPanel?.gameObject.SetActive(false);
        Inventory = new StorageSystem(Constant.InventorySize);
    }

    // Utility methods for money
    public void AddMoney(int amount)
    {
        money += amount;
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        return false;
    }

    public void OpenMapPanel(MapTemplate map)
    {
        if (mapPanel != null)
        {
            mapPanel.gameObject.SetActive(true);
            mapPanel.SetMap(map);
        }
        else
        {
            Debug.LogWarning("MapPanel is not assigned in GameController.");
        }
    }
}
