using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmDisposeDialog : MonoBehaviour{
    [SerializeField] Text itemNameText;
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] InventoryScene inventoryScene;
    private StorageSlot slot;
    public void Setup(StorageSlot slot)
    {
        if (slot.Item != null){
            this.slot = slot;
            itemBox.Render(slot.Item);
            itemNameText.text = "Confirm to dispose " + slot.Item.Name + "?";
        }
    }

    public void OnClickDispose(){
        slot.Clear();
        gameObject.SetActive(false);
        inventoryScene.Render();
    }

    public void OnClickCancel(){
        gameObject.SetActive(false);
    }
}