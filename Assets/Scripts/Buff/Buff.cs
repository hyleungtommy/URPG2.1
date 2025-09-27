using UnityEngine;

public class Buff
{
    public int id { get; private set; }
    public BuffType buffType { get; private set; }
    public int duration { get; set; }
    public Sprite icon { get; private set; }
    public int value { get; private set; }
    public bool isDebuff { get; private set; }

    public Buff(BuffTemplate template, int duration){
        id = template.id;
        buffType = template.type;
        this.duration = duration;
        icon = template.icon;
        value = template.value;
        isDebuff = template.isDebuff;
    }

}