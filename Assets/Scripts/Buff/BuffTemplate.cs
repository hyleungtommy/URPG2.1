using UnityEngine;

[CreateAssetMenu(fileName = "NewBuff", menuName = "Buff")]
public class BuffTemplate : ScriptableObject
{
    public int id;
    public string buffName;
    public BuffType type = BuffType.None;
    public bool isDebuff = false;
    public Sprite icon;
    public int value;
    public int[] replaceBuffIds;
    
}
