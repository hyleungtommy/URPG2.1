using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerList : MonoBehaviour
{
    [SerializeField] BattlePlayerView[] playerViews = new BattlePlayerView[5];
    public void Setup(List<BattlePlayerEntity> players)
    {
        for (int i = 0; i < playerViews.Length; i++)
        {
            if (i < players.Count)
            {
                var player = players[i];
                if (player != null)
                {
                    playerViews[i].gameObject.SetActive(true);
                    playerViews[i].Setup(player);
                }
                else
                {
                    playerViews[i].gameObject.SetActive(false);
                }
            }
            else
            {
                playerViews[i].gameObject.SetActive(false);
            }
        }

    }

    public void Render()
    {
        foreach (var playerView in playerViews)
        {
            if (playerView.gameObject.activeSelf)
                playerView.Render();
        }
    }
}
