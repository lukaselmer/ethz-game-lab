using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
	public float PlayTime{ get; private set; }

	void Update ()
	{
		PlayTime += Time.deltaTime;
	}
}
