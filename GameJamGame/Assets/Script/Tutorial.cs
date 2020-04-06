using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
	public Text tutMessage;
	public string[] messages;
	private int num;
	public GameObject arrowObj;
	public GameObject player;
	public GameObject carrier;

	void UpdateMessage()
	{
		tutMessage.text = messages[num];

		switch (num)
		{
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
		}
	}

	void Start()
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
