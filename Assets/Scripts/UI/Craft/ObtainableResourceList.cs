using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObtainableResourceList:MonoBehaviour{
    public Image[] resourceImages;

    public void SetObtainableResourceList(List<ItemWithQuantity> rewardItemList){
        for(int i = 0; i < resourceImages.Length; i++){
            if(i < rewardItemList.Count){
                resourceImages[i].gameObject.SetActive(true);
                resourceImages[i].sprite = rewardItemList[i].Item.Icon;
            }else{
                resourceImages[i].gameObject.SetActive(false);
            }
        }
    }

}