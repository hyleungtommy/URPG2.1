using UnityEngine;
using UnityEngine.UI;

public class ItemWithQuantityBox : MonoBehaviour{
    public BasicItemBox itemBox;
    public Text itemQuantity;

    public void SetItemWithQuantity(ItemWithQuantity itemWithQuantity){
        if(itemWithQuantity.Item == null){
            itemBox.RenderNull();
        }else{
            itemBox.Render(itemWithQuantity.Item);
            itemQuantity.text = itemWithQuantity.Quantity.ToString();
        }
        
    }
}