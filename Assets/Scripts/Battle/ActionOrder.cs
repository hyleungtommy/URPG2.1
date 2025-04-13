using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionOrder : MonoBehaviour
{
    [SerializeField] Image[] entityIcons;

    public void Render(List<BattleEntity> turnOrder)
    {
        for (int i = 0; i < entityIcons.Length; i++)
        {
            if (i < turnOrder.Count)
            {
                var entity = turnOrder[i];
                if (entity != null && entity.IsAlive)
                {
                    entityIcons[i].gameObject.SetActive(true);
                    entityIcons[i].sprite = entity.Portrait;
                }
                else
                {
                    entityIcons[i].gameObject.SetActive(false);
                }
            }
            else
            {
                entityIcons[i].gameObject.SetActive(false);
            }
        }
    }
}
