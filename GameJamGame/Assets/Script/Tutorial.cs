using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
	[SerializeField] private Text tutMessage = null;
	[SerializeField]
	private GameObject tutorialObj = null;

	[TextArea] [SerializeField] private string[] messages;
	private int num;
	[SerializeField] private GameObject arrowObj = null;
	[SerializeField] private GameObject player = null;
	[SerializeField] private GameObject carrier = null;
	[SerializeField] private PhaseController phase;
	// public GameObject tutTrigger;
	private int tutStatus;


	void UpdateMessage()
	{
		tutMessage.text = messages[num];

		switch (num)
		{
			case 0:
				Player.Instance.LockMovement = true;
				break;

			case 2:
				arrowObj.SetActive(true);
				arrowObj.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1);
				arrowObj.transform.parent = player.transform;
				break;
			case 3:
				Player.Instance.LockMovement = false;
				break;
			case 4:
				arrowObj.SetActive(false);
				break;
			case 5:
				Player.Instance.LockMovement = true;
				break;
			case 6:
				arrowObj.SetActive(true);
				arrowObj.transform.parent = null;
				arrowObj.transform.position = new Vector2(carrier.transform.position.x, carrier.transform.position.y + 1);
				break;
			case 8:
				arrowObj.transform.position = new Vector2(6f, 0.50f);
				arrowObj.transform.rotation = Quaternion.Euler(0, 0, 90);
				break;

			case 9:
				Player.Instance.LockMovement = false;
				arrowObj.transform.position = new Vector2(24f, 0.50f);
				arrowObj.transform.rotation = Quaternion.Euler(0, 0, -180);
				tutorialObj.SetActive(false);
				tutStatus = 1;
				break;
			case 10:
				arrowObj.SetActive(false);
				phase.enableUpdate = false;
				break;
			case 14:
				phase.enableUpdate = true;
				tutorialObj.SetActive(false);
				Destroy(this);
				break;
		}
	}

	protected void OnEnable()
	{
		BridgeSelection.OnBridgeSelected += BridgeSelection_OnBridgeSelected;
	}

	private void BridgeSelection_OnBridgeSelected(object sender, Cyberevolver.SimpleArgs<GameObject> e)
	{
		BattleTutorial();
	}

	protected void OnDisable()
	{
		BridgeSelection.OnBridgeSelected -= BridgeSelection_OnBridgeSelected;
	}

	private void BattleTutorial()
	{
		Debug.LogError("BATTLE TUTORIAL");
		tutorialObj.SetActive(true);
		tutStatus = 2;
		num = 10;
		UpdateMessage();
	}

	private void SkipTutorial()
	{
		Player.Instance.LockMovement = false;
		tutorialObj.SetActive(false);
		Destroy(this);
	}

	private void Start()
	{
		tutStatus = 0;
		UpdateMessage();
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space) && tutStatus != 1)
		{
			num++;
			UpdateMessage();
		}
		if (Input.GetKeyUp(KeyCode.LeftControl) && tutStatus != 1)
		{
			SkipTutorial();
		}
	}
}