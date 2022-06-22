using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MenuFunctions
{
    public List<string> strings;

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void OnStartButtonPressed()
    {
        StartCoroutine(LoadMainGame());
    }

    private IEnumerator LoadMainGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Chapter1", LoadSceneMode.Single);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
