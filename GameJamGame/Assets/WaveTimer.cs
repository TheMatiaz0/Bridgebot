using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTimer : MonoBehaviour
{
	[SerializeField]
	private Text timerText = null;

	public Text TimerText => timerText;
}
