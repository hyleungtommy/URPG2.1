using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitController : MonoBehaviour
{
    [SerializeField] SceneList.TileMap tileMap;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("SceneTransitController: OnTriggerEnter2D " + other.name);
        if (other.CompareTag("Player"))
        {
            SceneController.Instance.AnimateSceneTransit(tileMap);
        }
    }
}
