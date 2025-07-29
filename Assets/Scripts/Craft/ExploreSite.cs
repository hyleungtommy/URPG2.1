public class ExploreSite
{
    public int id;
    public string siteName;
    public string description;
    public int requiredLevel;
    public int price;
    public ExploreSiteType type;
    public int requireTime;
    public ObtainableItemTemplate[] obtainableItems;
    public ExploreTask exploreTask;
    public ExploreSite(ExploreSiteTemplate template)
    {
        id = template.id;
        siteName = template.siteName;
        description = template.description;
        requiredLevel = template.requiredLevel;
        type = template.type;
        requireTime = template.requireTime;
        obtainableItems = template.obtainableItems;
        price = template.price;
    }

    public void StartExploreTask()
    {
        exploreTask = new ExploreTask(this);
    }
}

public enum ExploreSiteType
{
    Mining, Forging, Hunting
}