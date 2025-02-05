using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SSO_LocalMultiplayerSettings", menuName = "SSO/LocalMultiplayer/LocalMultiplayerSettings")]
public class LocalMultiplayerSettings : ScriptableObject
{
    [Range(1,8)]public int maxPlayers = 1;
    public bool splitScreen;
}