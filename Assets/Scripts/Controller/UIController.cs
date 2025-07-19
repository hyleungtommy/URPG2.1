using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    private HashSet<string> loadedUIScenes = new HashSet<string>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Press Escape to close all open UI scenes
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAllUIScenes();
        }
    }

    /// <summary>
    /// Loads a UI scene additively if it’s not already loaded.
    /// </summary>
    public void OpenUIScene(string sceneName)
    {
        if (loadedUIScenes.Contains(sceneName))
            return;

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadedUIScenes.Add(sceneName);
    }

    /// <summary>
    /// Unloads a UI scene if it’s currently loaded.
    /// </summary>
    public void CloseUIScene(string sceneName)
    {
        if (!loadedUIScenes.Contains(sceneName))
            return;

        SceneManager.UnloadSceneAsync(sceneName);
        loadedUIScenes.Remove(sceneName);
    }

    /// <summary>
    /// Toggle UI scene on/off like a popup menu.
    /// </summary>
    public void ToggleUIScene(string sceneName)
    {
        if (loadedUIScenes.Contains(sceneName))
        {
            CloseUIScene(sceneName);
            if(GameController.Instance != null){
                GameController.Instance.state = GameController.State.Idle;
            }
        }
        else
        {
            OpenUIScene(sceneName);
            if(GameController.Instance != null){
                GameController.Instance.state = GameController.State.OpenUI;
            }
        }

    }

    /// <summary>
    /// Check if a UI scene is currently loaded.
    /// </summary>
    public bool IsUISceneOpen(string sceneName)
    {
        return loadedUIScenes.Contains(sceneName);
    }

    /// <summary>
    /// Closes all currently loaded UI scenes.
    /// </summary>
    public void CloseAllUIScenes()
    {
        foreach (var sceneName in new List<string>(loadedUIScenes))
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
        if(GameController.Instance != null){
            GameController.Instance.state = GameController.State.Idle;
        }
        loadedUIScenes.Clear();
    }
}
