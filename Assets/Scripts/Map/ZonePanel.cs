﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZonePanel : MonoBehaviour
{
    [SerializeField] Text zoneText;
    public void Render(Map map)
    {
        zoneText.text = $"{map.CurrentZone}/{map.Template.NumberOfZones}";
    }
}
