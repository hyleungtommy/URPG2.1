public class ApplyBuff
{
    public BuffTemplate buffTemplate { get; private set; }
    public int duration { get; private set; }
    public int chance { get; private set; }
    public ApplyBuff(BuffTemplate buffTemplate, int duration, int chance)
    {
        this.buffTemplate = buffTemplate;
        this.duration = duration;
        this.chance = chance;
    }
}