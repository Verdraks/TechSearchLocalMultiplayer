using UnityEngine;
public static class Macro2D 
{
    public static Vector3 MousePosWorld(Vector3 position) => Camera.main.ScreenToWorldPoint(position - new Vector3(0,0,Camera.main.transform.position.z));
}