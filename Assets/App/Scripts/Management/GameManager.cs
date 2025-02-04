using System;
using UnityEngine;
public class GameManager : MonoBehaviour
{
   [Header("References")] 
   [RequireInterface(typeof(IInputReader))]
   [SerializeField] private MonoBehaviour[] _controllers;
   
   private void Start()
   {
      foreach (var controller in _controllers)
      {
         controller.GetComponent<IInputReader>().EnableInputReader();
      }
   }
}