using UnityEngine;
public static class Macro2D 
{
    public static Vector3 MousePosWorld => Camera.main.ScreenToWorldPoint(Input.mousePosition - new Vector3(0,0,Camera.main.transform.position.z));
}