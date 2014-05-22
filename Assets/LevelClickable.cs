using UnityEngine;
using System.Collections;

public class LevelClickable : MonoBehaviour {

	void Start () {
		if (!LevelSelection.CanPlay(name)){
			this.renderer.enabled = false;
		}
	}

	void AfterUpdate () {
	
	}
}
