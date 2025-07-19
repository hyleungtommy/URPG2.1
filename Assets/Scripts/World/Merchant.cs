using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour, Interactable
{
    [SerializeField] SceneList.UI sceneToLoad;
    public void Interact(){
        UIController.Instance.OpenUIScene(sceneToLoad.ToString());
    }
}
