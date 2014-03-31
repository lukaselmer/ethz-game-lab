using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		float xAxisValue = Input.GetAxis("Horizontal");
		float yAxisValue = Input.GetAxis("Vertical");
		if(gameObject != null)
		{
			gameObject.transform.Translate(new Vector3(xAxisValue, 0, yAxisValue));
		}
	}
}
