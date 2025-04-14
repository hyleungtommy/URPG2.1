public class ItemResource : Item
{
    public override int MaxStackSize { get { return 99; } }
    public override string ItemType { get { return "Resource"; } }
    public ItemResource(ItemResourceTemplate template) : base(template)
    {
    }
}

