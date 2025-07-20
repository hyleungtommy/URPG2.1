using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapPanel : MonoBehaviour
{
    [Header("Map Info UI")]
    [SerializeField] private Text mapNameText;
    [SerializeField] private Text recommendedLevelText;
    [SerializeField] private Image previewImage;
    [SerializeField] private Text zoneProgressText;

    private MapTemplate currentTemplate;

    void Awake(){
        SetMap(GameController.Instance.CurrentMap);
    }

    public void SetMap(MapTemplate template)
    {
        currentTemplate = template;

        mapNameText.text = template.MapName;
        recommendedLevelText.text = $"Lv {template.RecommendedMinLevel} - {template.RecommendedMaxLevel} \n\n {template.Description}";
        previewImage.sprite = template.PreviewImage;

        // Show total zone count (but no current zone yet)
        if (template.NumberOfZones > 0)
        {
            zoneProgressText.text = $"1 / {template.NumberOfZones}";
        }
    }

    public void OnZoneModeClicked()
    {
        if (currentTemplate == null) return;

        Debug.Log("Zone Mode Selected for: " + currentTemplate.MapName);
        UIController.Instance.CloseAllUIScenes();

        Map map = new Map(currentTemplate, Map.MapMode.Zone);
        BattleSceneLoader.LoadBattleScene(map);
        //MapManager.Instance.StartMap(map);
    }

    public void OnExploreModeClicked()
    {
        if (currentTemplate == null) return;

        Debug.Log("Explore Mode Selected for: " + currentTemplate.MapName);
        UIController.Instance.CloseAllUIScenes();

        Map map = new Map(currentTemplate, Map.MapMode.Explore);
        BattleSceneLoader.LoadBattleScene(map);
        //MapManager.Instance.StartMap(map);
    }

    public void OnClickX(){
        UIController.Instance.CloseUIScene("Map");
        GameController.Instance.state = GameController.State.Idle;
    }
}
