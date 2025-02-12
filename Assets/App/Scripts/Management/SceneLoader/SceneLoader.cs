using UnityEngine;
using UnityEngine.SceneManagement;
using static System.String;

public class SceneLoader : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private RSE_LoadScene rseLoadScene;
    [SerializeField] private RSE_LoadSceneAdditive rseLoadSceneAdditive;
    
    [Header("Output")]
    [SerializeField] private RSE_SceneLoaded rseSceneLoaded;

    private string sceneAdditiveLoaded;
    
    private void OnEnable()
    {
        rseLoadScene.action += LoadScene;
        rseLoadSceneAdditive.action += LoadSceneAdditive;
    }

    private void OnDisable()
    {
        rseLoadScene.action -= LoadScene;
        rseLoadSceneAdditive.action -= LoadSceneAdditive;
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    
    private void LoadSceneAdditive(string sceneName)
    {
        if (!IsNullOrEmpty(sceneAdditiveLoaded))SceneManager.UnloadSceneAsync(sceneAdditiveLoaded);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        sceneAdditiveLoaded = sceneName;
        rseSceneLoaded.Call();
    }
    
}