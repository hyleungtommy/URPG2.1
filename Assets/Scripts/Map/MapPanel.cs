using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        SetMap(Game.MapPanelSelectedMap);
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

        UIController.Instance.CloseAllUIScenes();

        Game.CurrentMap = new Map(currentTemplate, Map.MapMode.Zone);
        SceneManager.LoadScene("Battle");
        //MapManager.Instance.StartMap(map);
    }

    public void OnExploreModeClicked()
    {
        if (currentTemplate == null) return;

        UIController.Instance.CloseAllUIScenes();

        Game.CurrentMap = new Map(currentTemplate, Map.MapMode.Explore);
        SceneManager.LoadScene("Battle");
        //MapManager.Instance.StartMap(map);
    }

    public void OnClickX(){
        UIController.Instance.CloseUIScene("Map");
        Game.State = GameState.Idle;
    }
}
