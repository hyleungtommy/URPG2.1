using UnityEngine;

// Ensure this script runs earlier in the execution order
[DefaultExecutionOrder(-1000)]
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Setup Templates")]
    public PartyTemplate StartingPartyTemplate;
    [Header("Map UI")]
    [SerializeField] MapTemplate testMap;
    

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
        if (Game.State == GameState.Battle)
        {
            return; // Don't allow input during battle
        }
        else if (Game.State == GameState.Dialog)
        {
            return; // Don't allow input during dialog
        }
        else if (Game.State == GameState.OpenUI)
        {
            // Can only press escape when opened a UI
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIController.Instance.CloseAllUIScenes();
            }
        }
        else if (Game.State == GameState.Idle)
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
            if (Input.GetKeyDown(KeyCode.L))
            {
                UIController.Instance.ToggleUIScene("Shop");
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                UIController.Instance.ToggleUIScene("SkillCenter");
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                UIController.Instance.ToggleUIScene("Blacksmith");
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Game.QuestBoardMode = QuestBoardMode.Available;
                UIController.Instance.ToggleUIScene("QuestBoard");
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Game.QuestBoardMode = QuestBoardMode.Accepted;
                UIController.Instance.ToggleUIScene("QuestBoard");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                UIController.Instance.ToggleUIScene("ExploreCamp");
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                UIController.Instance.ToggleUIScene("Alchemy");
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                UIController.Instance.ToggleUIScene("Smithing");
            }
            if (Input.GetKeyDown(KeyCode.Space)){
                FindObjectOfType<PlayerController>()?.Interact();
            }
        }
    }

    private void InitializeGame()
    {
        // Initialize party
        if (StartingPartyTemplate != null)
        {
            Game.Initialize(StartingPartyTemplate);
        }
        else
        {
            Debug.LogError("No StartingPartyTemplate assigned to GameController!");
        }

        // Starting money (optional)
        Game.Money = Constant.StartMoney; // or load from save data
        Game.Inventory = new StorageSystem(Constant.InventorySize);

    }

    public void OpenMapPanel(MapTemplate map)
    {
        UIController.Instance.OpenUIScene("Map");
        Game.MapPanelSelectedMap = map;
        Game.State = GameState.OpenUI;
    }
}
