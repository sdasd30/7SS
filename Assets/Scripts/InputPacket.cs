using UnityEngine;

public class InputPacket {
    public Vector2 movementInput = new Vector2();
    public Vector2 MousePointWorld = new Vector2();
    public bool jump = false;
    public bool fire1 = false;
    public bool fire1Press = false;


    public void Combine(InputPacket ip)
    {
        movementInput = new Vector2(Mathf.Max(ip.movementInput.x, movementInput.x),
            Mathf.Max(ip.movementInput.y, movementInput.y));
        MousePointWorld = ip.MousePointWorld;
        jump = jump || ip.jump;
        fire1 = fire1 || ip.fire1;
    }
}