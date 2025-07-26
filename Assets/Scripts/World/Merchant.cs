using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour, Interactable
{
    [SerializeField] SceneList.UI sceneToLoad;
    public void Interact(){
        if(sceneToLoad == SceneList.UI.QuestBoard){
            Game.QuestBoardMode = QuestBoardMode.Available;
        }
        UIController.Instance.OpenUIScene(sceneToLoad.ToString());
    }
}
