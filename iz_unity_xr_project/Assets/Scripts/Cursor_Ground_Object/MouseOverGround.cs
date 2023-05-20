using UnityEngine;

public class MouseOverGround : MonoBehaviour
{
    public PNC_PlayerController player;

    void OnMouseOver()
    {
        player.MouseOverGround(true);
    }

    void OnMouseExit()
    {
       player.MouseOverGround(false);
    }
}
