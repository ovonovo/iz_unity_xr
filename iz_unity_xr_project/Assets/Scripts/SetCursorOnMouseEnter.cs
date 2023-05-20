using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursorOnMouseEnter : MonoBehaviour
{
    public Texture2D cursorTexture;
    [Tooltip("Keep empty if standard cursor is used")]
    public Texture2D cursorOverObjectTexture;
    
    CursorMode cursorMode = CursorMode.Auto;
    Vector2 hotSpot = Vector2.zero;

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorOverObjectTexture, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        if(cursorTexture != null){
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode); 
        }else{
            Cursor.SetCursor(null, hotSpot, cursorMode); 
        }
    }
}
