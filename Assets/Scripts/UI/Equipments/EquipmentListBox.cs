using UnityEngine;
using UnityEngine.UI;

public class EquipmentListBox : ListBox
{
    [SerializeField] BasicItemBox itemBox;
    public override void Render()
    {
        StorageSlot slot = obj as StorageSlot;
        if (slot.Item != null)
        {
            itemBox.Render(slot.Item);
        }
    }
}