using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverHighlight : MonoBehaviour
{
    public Renderer rend;
    public Color hoverColor = Color.white;
    Color originalColor;

    void Start()
    {
        originalColor = rend.material.color;
    }

    public void OnHoverEnter()
    {
        rend.material.color = hoverColor;
    }

    public void OnHoverExit()
    {
        rend.material.color = originalColor;
    }
}