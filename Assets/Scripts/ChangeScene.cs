using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Dumb lil shit to change the scene.
/// </summary>
public class ChangeScene : MonoBehaviour
{
    public void ChangeTheDamnSceneAsync(int sceneID)
    {
        SceneManager.LoadSceneAsync(sceneID);
    }
}
