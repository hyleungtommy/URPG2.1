public class ElementResistance
{
    public int Fire { get; set; }
    public int Ice { get; set; }
    public int Lightning { get; set; }
    public int Earth { get; set; }
    public int Wind { get; set; }
    public int Light { get; set; }
    public int Dark { get; set; }

    public ElementResistance(int fire, int ice, int lightning, int earth, int wind, int light, int dark){
        Fire = fire;
        Ice = ice;
        Lightning = lightning;
        Earth = earth;
        Wind = wind;
        Light = light;
        Dark = dark;
    }

    public ElementResistance(){
        Fire = 0;
        Ice = 0;
        Lightning = 0;
        Earth = 0;
        Wind = 0;
        Light = 0;
        Dark = 0;
    }
}