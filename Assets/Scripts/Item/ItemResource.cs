public class ItemResource : Item
{
    public override int MaxStackSize { get { return 99; } }
    public ItemResource(ItemResourceTemplate template) : base(template)
    {
    }
}

