
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    [SerializeField] SceneList.TileMap sceneToLoad;
    
    public void Interact(){
        SceneController.Instance.AnimateSceneTransit(sceneToLoad);
    }
}
