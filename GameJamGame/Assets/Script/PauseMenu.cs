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
	private bool wasActivated = false;

	protected void OnEnable()
	{
		if (Player.Instance.LockMovement == true)
		{
			wasActivated = true;
			return;
		}

		Player.Instance.LockMovement = true;
	}

	protected void OnDisable()
	{
		if (wasActivated == true)
		{
			return;
		}

		Player.Instance.LockMovement = false;
	}

	public void ResumeBtn ()
	{
		pauseManager.EnableMenuWithPause(false);
	}

	public void QuitBtn ()
	{
		pauseManager.EnableMenuWithPause(false);
		SceneManager.LoadScene("MainMenu");
	}
}
