using UnityEngine;
using System.Collections;

public class ScoreBehaviour : MonoBehaviour {

	public GameLogic gameLogic;

	void Update () {
		this.guiText.text = "Play Time: " + gameLogic.PlayTime;
	}
}
