using UnityEngine;

public class MouseOverObject : MonoBehaviour
{
    [Tooltip("Das GO mit dem PNC-PlayerController hier rein ziehen")]
    public PNC_PlayerController pncPlayerController;
    [Tooltip("wenn kein extra Cursor verwendet werden soll")]
    public Texture2D cursorOverObjectTexture;
    
    CursorMode cursorMode = CursorMode.Auto;
    Vector2 hotSpot = Vector2.zero;

    void OnMouseEnter()
    {
        if(!pncPlayerController.mouseDraggingFromObject) Cursor.SetCursor(cursorOverObjectTexture, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseOver()
    {
        pncPlayerController.MouseOverObject(true);
    }

    void OnMouseExit()
    {
       pncPlayerController.MouseOverObject(false);
    }

    void OnMouseDrag()
    {
        pncPlayerController.MouseDragOverObject(true);
    }

    void OnMouseUp()
    {
        pncPlayerController.MouseDragOverObject(false);
    }
}
