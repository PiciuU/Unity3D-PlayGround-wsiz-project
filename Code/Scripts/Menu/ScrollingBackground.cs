using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    public float x, y;

    void Update()
    {
        _image.uvRect = new Rect(_image.uvRect.position + new Vector2(x, y) * Time.deltaTime, _image.uvRect.size);
    }
}