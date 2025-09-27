public class ApplyBuff
{
    Buff buff;
    int duration;
    int chance;
    public ApplyBuff(BuffTemplate buffTemplate, int duration, int chance)
    {
        this.buff = new Buff(buffTemplate, duration);
        this.duration = duration;
        this.chance = chance;
    }
}