using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public void SetColors(PlayerVisualData[] data)
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        for (int i = 0; i < data.Length; i++)
        {
            mr.materials[data[i].ColorIndex].SetColor("_BaseColor", data[i].VisualColor);
        }
    }
}

[Serializable]
public class PlayerVisualData
{
    private int colorIndex;

    public int ColorIndex
    {
        get { return colorIndex; }
        set { colorIndex = value; }
    }

    private Color color;

    public Color VisualColor
    {
        get { return color; }
        set { color = value; }
    }

    public PlayerVisualData(int colorIndex)
    {
        this.colorIndex = colorIndex;
    }
}
