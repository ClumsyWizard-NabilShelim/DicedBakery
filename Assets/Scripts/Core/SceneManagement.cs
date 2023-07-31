using System.Collections;
using UnityEngine;
using ClumsyWizard.Utilities;
using UnityEngine.SceneManagement;
using System;

public class SceneManagement : Singleton<SceneManagement>
{
	private Animator animator;

	protected override void Awake()
	{
		base.Awake();
		animator = GetComponent<Animator>();
	}

	public static void Reload()
	{
		Instance.StartCoroutine(LoadScene(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex).name));
	}

	/// <param name="sceneName">If sceneName is empty, the next scene will be loaded from the scene build index</param>
	public static void Load(string sceneName = "")
	{
		Instance.StartCoroutine(LoadScene(sceneName));
	}

	private static IEnumerator LoadScene(string sceneName)
	{
		string sceneToLoad = string.Empty;

		if (sceneName == "")
		{
			int buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
			sceneToLoad = SceneManager.GetSceneByBuildIndex(buildIndex).name;
		}
		else
		{
			sceneToLoad = sceneName;
		}

		Instance.animator.SetTrigger("Transition");
		yield return new WaitForSeconds(1.0f);

		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

		operation.completed += (AsyncOperation o) =>
		{
			Instance.animator.SetBool("Transition", false);
		};
	}
}