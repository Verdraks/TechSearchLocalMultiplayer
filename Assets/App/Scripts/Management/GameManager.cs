using System;
using System.Threading.Tasks;
using BT.LocalMultiplayer;
using UnityEngine;
public class GameManager : MonoBehaviour
{
   [Header("References")] 
   [RequireInterface(typeof(IInputReader))]
   [SerializeField] private MonoBehaviour[] controllers;
   [Space(10)]
   [SerializeField] private SpawningStrategy spawningSpawnPoint;
   [SerializeField] private RSO_DevicesRegistered rsoDeviceRegistered;

   [Header("Input")] 
   [SerializeField] private RSE_PlayerDie rsePlayerDie;
   [SerializeField] private RSE_SceneLoaded rseSceneLoaded;


   private void OnEnable()
   {
      rseSceneLoaded.action += LevelFinished;
      rsePlayerDie.action += RespawnPlayer;
   }

   private void OnDisable()
   {
      rseSceneLoaded.action -= LevelFinished;
      rsePlayerDie.action -= RespawnPlayer;
   }
   
   private void Start()
   {
      foreach (var controller in controllers)
      {
         var inputReader = controller.GetComponent<IInputReader>();
         inputReader.AssignDevice();
         inputReader.EnableInputReader();
      }
   }

   private void RespawnPlayer(GameObject player)
   {
      player.GetComponent<IInputReader>().GetControlledGameObject().transform.position = spawningSpawnPoint.GetSpawnPosition();
   }
   
   private async void LevelFinished()
   {
      await Task.Delay(10);   
      foreach (var deviceData in rsoDeviceRegistered.Value)
      {
         deviceData.MonitoredInputReader.GetControlledGameObject().SetActive(true);
         deviceData.MonitoredInputReader.GetControlledGameObject().transform.position =
            spawningSpawnPoint.GetSpawnPosition();
      }
   }
   
}