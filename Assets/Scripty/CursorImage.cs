using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursorUI : MonoBehaviour
{
    public RectTransform cursorImage;
    public Vector2 offset;
    public float scale = 1.0f; 

    void Start()
    {
        Cursor.visible = false; 
        if (cursorImage != null)
        {
            cursorImage.localScale = new Vector3(scale, scale, 1);
        }
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        cursorImage.position = mousePosition + offset;
    }
}
