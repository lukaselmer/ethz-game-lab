﻿using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {

	void Update () {
		transform.RotateAround(transform.position, Vector3.up, 50 * Time.deltaTime);
	}
}