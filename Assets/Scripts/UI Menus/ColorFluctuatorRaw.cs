using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ColorFluctuatorRaw : MonoBehaviour
{
    private RawImage rawImg;

    [Header("Ajustes de Animación")]
    public float speed = 2.0f; // Subí la velocidad para que se note más el cambio

    [Header("Colores")]
    public Color colorBlanco = Color.white; // FFFFFF
    public Color colorObjetivo = new Color(1f, 0.76f, 0.76f, 1f); // FFC2C2

    void Awake()
    {
        rawImg = GetComponent<RawImage>();
    }

    void Update()
    {
        // Calculamos el factor de mezcla (va de 0 a 1 constantemente)
        float t = Mathf.PingPong(Time.time * speed, 1f);
        
        // Aplicamos el color linealmente
        rawImg.color = Color.Lerp(colorBlanco, colorObjetivo, t);
    }
}