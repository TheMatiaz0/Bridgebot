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
	// public GameObject tutTrigger;
	private bool tutAllowed;


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
			case 4:
				arrowObj.SetActive(false);
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
				arrowObj.SetActive(false);
				tutorialObj.SetActive(false);
				break;


		}
	}

	protected void OnEnable()
	{
		BridgeSelection.OnBridgeSelected += BridgeSelection_OnBridgeSelected;
	}

	private void BridgeSelection_OnBridgeSelected(object sender, Cyberevolver.SimpleArgs<GameObject> e)
	{

	}

	protected void OnDisable()
	{
		BridgeSelection.OnBridgeSelected -= BridgeSelection_OnBridgeSelected;
	}

	private void Start()
	{
		UpdateMessage();
	}

	void Update()
    {
		if (Input.GetKeyUp(KeyCode.Space))
		{
			num++;
			UpdateMessage();
		}
    }
}
