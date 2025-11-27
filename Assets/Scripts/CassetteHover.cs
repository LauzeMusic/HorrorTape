using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassetteHover : MonoBehaviour

{
    private Color originalColor;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
    }

    public void Highlight()
    {
        if (rend != null)
            rend.material.color = originalColor * 1.4f; // más brillante
    }

    public void Unhighlight()
    {
        if (rend != null)
            rend.material.color = originalColor;
    }
}