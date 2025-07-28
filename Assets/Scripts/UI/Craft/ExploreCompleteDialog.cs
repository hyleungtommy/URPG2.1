using System.Collections.Generic;
using UnityEngine;

public class ExploreCompleteDialog:MonoBehaviour{
    public ItemWithQuantityBox[] itemWithQuantityBoxes;

    public void SetExploreCompleteDialog(List<ItemWithQuantity> rewardItemList){
        for(int i = 0; i < itemWithQuantityBoxes.Length; i++){
            if(i < rewardItemList.Count){
                itemWithQuantityBoxes[i].gameObject.SetActive(true);
                Debug.Log(rewardItemList[i]);
                itemWithQuantityBoxes[i].SetItemWithQuantity(rewardItemList[i]);
            }else{
                itemWithQuantityBoxes[i].gameObject.SetActive(false);
            }
        }
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
    }
}