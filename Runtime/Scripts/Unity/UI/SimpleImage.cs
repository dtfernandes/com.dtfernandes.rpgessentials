using System;
using UnityEngine;
using UnityEngine.UI;

public class SimpleImage: GUIObject
{
    Sprite sprite;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();     
    }

    public void Setup(Sprite sprite)
    {
        this.sprite = sprite;
        Display();
    }

    public override void Display()
    {
        image.sprite = sprite;
    }

    private void OnEnable()
    {
        image.enabled = true;
    }

    private void OnDisable()
    {
        image.enabled = false;
    }   
}
