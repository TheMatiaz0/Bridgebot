using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[SerializeField]
	private FreezeMenu pauseManager = null;

	protected void OnEnable()
	{
		Player.Instance.LockMovement = true;
	}

	protected void OnDisable()
	{
		Player.Instance.LockMovement = false;
	}

	public void ResumeBtn ()
	{
		pauseManager.EnableMenuWithPause(false);
	}

	public void QuitBtn ()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
