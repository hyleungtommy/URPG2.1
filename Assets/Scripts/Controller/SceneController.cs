using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    [SerializeField] FadeController FadeController;
    public static SceneList.TileMap CurrentTileMap { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TransitScene(string sceneName){
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void AnimateSceneTransit(SceneList.TileMap tileMap){
        CurrentTileMap = tileMap;
        FadeController.FadeToScene(tileMap.ToString());
    }

}
