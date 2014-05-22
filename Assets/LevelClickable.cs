using UnityEngine;
using System.Collections;

public class LevelClickable : MonoBehaviour {

	void Start () {
		this.renderer.enabled = LevelSelection.CanPlay (name);
	}
}
