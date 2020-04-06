using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject optionsMenu = null;

	[SerializeField]
	private GameObject mainMainMenu = null;

	public void StartGameBtn ()
	{
		SceneManager.LoadScene("SampleScene");
	}

	public void Options ()
	{
		optionsMenu.SetActive(true);
		mainMainMenu.SetActive(false);
	}

	public void Quit ()
	{
		Application.Quit(0);
	}
}
