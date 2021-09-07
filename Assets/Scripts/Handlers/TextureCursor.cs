using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCursor : MonoBehaviour
{

    void Awake()
    {
        Cursor.visible = false;

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }
}
