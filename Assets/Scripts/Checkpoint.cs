using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	private Vector3? _position;

	public Vector3 Position {
		get{return _position.HasValue ? _position.Value : this.transform.position;} 
		set{_position = value;}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
