using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public interface MouseWrapper
{
    public Vector2 MousePosition { get; set; }
    public  Vector2 GetWorldMousePos();
    public void SetMousePosition(Vector2 value);
}

public class MouseWrapperGamepad : MouseWrapper
{
    public Vector2 MousePosition { get; set; } = new ((float)Screen.width / 2, (float)Screen.height / 2);

    private const float Sensitivity = 2f;

    
    public void SetMousePosition(Vector2 value)
    {
        var mousePosition = MousePosition + value * Sensitivity;
        mousePosition.x = Mathf.Clamp(mousePosition.x,0,Screen.width);
        mousePosition.y = Mathf.Clamp(mousePosition.y,0,Screen.height);
        MousePosition = mousePosition;
    }

    public Vector2 GetWorldMousePos()
    {
        return Macro2D.MousePosWorld(MousePosition);
    }
}

public class MouseWrapperDevice : MouseWrapper
{
    public Vector2 MousePosition { get; set; } =  new ((float)Screen.width / 2, (float)Screen.height / 2);

    public Vector2 GetWorldMousePos()
    {
        return Macro2D.MousePosWorld(MousePosition);
    }

    public void SetMousePosition(Vector2 value)
    {
        MousePosition = value;
    }
}