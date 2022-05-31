using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MenuFunctions
{
    public Scene mainScene;

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void OnStartButtonPressed()
    {
        StartCoroutine(LoadMainGame());
    }

    private IEnumerator LoadMainGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
