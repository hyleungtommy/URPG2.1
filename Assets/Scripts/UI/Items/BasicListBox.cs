using UnityEngine;
using UnityEngine.UI;

public class BasicListBox : ListBox
{
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] Text itemName;
    public override void Render()
    {
        if (obj != null && obj is Equipment)
        {
            itemBox.Render(obj as Equipment);
            itemName.text = (obj as Equipment).FullName;
        }
        else if (obj != null && obj is Item)
        {
            itemBox.Render(obj as Item);
            itemName.text = (obj as Item).Name;
        }
    }
}