using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
	public Text tutMessage;
	public string[] messages;
	private int num;

    void UpdateMessage()
    {
		tutMessage.text = messages[num];
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
