using UnityEngine;
using System.Collections;


public class Treee : MonoBehaviour {
	public LayerMask branchLayer;	
	public GameObject branchPrefab;
	public float zAngle = 30;
	public int numBranches = 4;
	public int maxDepth = 5;
	public int yOffsetMin = 5;
	public int yOffsetMax = 10;

	public Branch Root {
		get; set;
	}

	public float Size {
		get {
			return Root.TotalSize;
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Root.Grow (Time.deltaTime * 0.1f);
		//print (Size);
	}

}
