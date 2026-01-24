using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonAudio : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    [Header("Audio Sources")]
    public AudioSource hoverSource; // latido (loop)
    public AudioSource clickSource; // click

    void Awake()
    {
        if (hoverSource != null)
        {
            hoverSource.loop = true;
            hoverSource.playOnAwake = false;
        }

        if (clickSource != null)
        {
            clickSource.playOnAwake = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSource != null && !hoverSource.isPlaying)
            hoverSource.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverSource != null && hoverSource.isPlaying)
            hoverSource.Stop();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSource != null)
            clickSource.PlayOneShot(clickSource.clip);
    }
}