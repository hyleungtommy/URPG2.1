using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour, Interactable
{
    [SerializeField] MapTemplate Map;

    public void Interact(){
        GameController.Instance.OpenMapPanel(Map);
    }

}