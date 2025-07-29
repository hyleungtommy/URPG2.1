using UnityEngine;

public abstract class InfoPanel : MonoBehaviour
{
    protected Object obj;
    public void SetUp(Object obj){
        this.obj = obj;
    }
    public abstract void Render();
    public void Show(){
        gameObject.SetActive(true);
    }
    public void Hide(){
        gameObject.SetActive(false);
    }
}