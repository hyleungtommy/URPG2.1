using System;
using NUnit.Framework;
using UnityEngine;

public abstract class ListBox:MonoBehaviour{
    protected System.Object obj;
    public virtual void SetUp(System.Object obj){
        this.obj = obj;
    }
    public abstract void Render();
}