using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BT.LocalMultiplayer
{
    [CreateAssetMenu(fileName = "SSO_InputReaderFactoryUniqueInstances", menuName = "SSO/LocalMultiplayer/InputReaderFactory/InputReaderFactoryUniqueInstances")]
    public class InputReaderFactoryUniqueInstances : InputReaderFactory
    {
        public RSO_DevicesRegistered rsoDeviceRegistered;

        public GameObject[] prefabsPlayerUniques;

        public override GameObject CreateLocalMultiplayerInputReader(InputDevice device)
        {
            foreach (var playerUnique in prefabsPlayerUniques)
            {
                var find = false;
                foreach (var deviceData in rsoDeviceRegistered.Value)
                {
                    if (playerUnique.CompareTag(deviceData.MonitoredInputReader.GetControlledGameObject().tag))
                    {
                        find = true;
                    }
                }
                if (!find) return Instantiate(playerUnique);
            }
            return null;
        }

    }
}