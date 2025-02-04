using System.Collections.Generic;
using UnityEngine;

namespace BT.LocalMultiplayer
{
    public class SplitScreenManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private LocalMultiplayerSettings localMultiplayerSettings;
        
        [Header("References")]
        [SerializeField] private SplitScreenFactory splitScreenFactory;
        [Space(10)]
        [SerializeField] private RSO_DevicesRegistered rsoDevicesRegistered;

        private readonly List<Camera> _playersCamera = new();

        private void Awake()
        {
            if (!localMultiplayerSettings.splitScreen) Destroy(gameObject);
        }
        
        private void OnEnable()
        {
            rsoDevicesRegistered.OnChanged += UpdateSplitScreen;
            UpdateSplitScreen(rsoDevicesRegistered.Value);
        }

        private void OnDisable() => rsoDevicesRegistered.OnChanged -= UpdateSplitScreen;

        private void UpdateSplitScreen(List<DeviceData> devices)
        {
            ClearCameras();
            
            int playerCount = devices.Count;
            if (playerCount == 0) return;
            
            (int cols, int rows) = GetGridSize(playerCount);

            for (int i = 0; i < playerCount; i++)
            {
                CreateCameraForPlayer(i, cols, rows);
            }
        }

        private void ClearCameras()
        {
            foreach (var cam in _playersCamera)
            {
                if (cam != null) Destroy(cam.gameObject);
            }
            _playersCamera.Clear();
        }

        private void CreateCameraForPlayer(int index, int cols, int rows)
        {
            Camera cam = splitScreenFactory.CreateSplitScreenCamera();
            _playersCamera.Add(cam);

            int row = index / cols;
            int col = index % cols;

            float camWidth = 1f / cols;
            float camHeight = 1f / rows;
            
            cam.rect = new Rect(col * camWidth, 1f - (row + 1) * camHeight, camWidth, camHeight);
        }

        private (int cols, int rows) GetGridSize(int playerCount)
        {
            int cols = Mathf.CeilToInt(Mathf.Sqrt(playerCount));
            int rows = Mathf.CeilToInt((float)playerCount / cols);
            return (cols, rows);
        }
    }
}
