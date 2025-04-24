using UnityEngine;
[CreateAssetMenu(fileName = "ItemResourceTemplate", menuName = "Item/Resource")]
public class ItemResourceTemplate : ItemTemplate
{
    public override Item GetItem()
    {
        return new ItemResource(this);
    }
}



