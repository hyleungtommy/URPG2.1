using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    [SerializeField] Animator SceneTransitAnimator;
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
        StartCoroutine(TransitWithAnimation(tileMap));
    }

    IEnumerator TransitWithAnimation(SceneList.TileMap tileMap){
        SceneTransitAnimator.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        CurrentTileMap = tileMap;
        SceneManager.LoadSceneAsync(tileMap.ToString());
        SceneTransitAnimator.SetTrigger("Start");
    }
}
