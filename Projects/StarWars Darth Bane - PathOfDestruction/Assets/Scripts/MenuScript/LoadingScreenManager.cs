using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour {

	public GameObject loadingScreen;
	public Slider LoadBar;

    public void LoadLevel(int sceneIndex)
	{
		StartCoroutine(LoadAsynchronously(sceneIndex));
	}

	IEnumerator LoadAsynchronously(int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
		loadingScreen.SetActive(true);
		while (!operation.isDone) 
		{
            float progress = Mathf.Clamp01(operation.progress / .9f);
			LoadBar.value = progress;
			yield return null;
		}
	}
}