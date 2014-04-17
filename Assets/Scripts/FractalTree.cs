using UnityEngine;
using System.Collections;

public class FractalTree : MonoBehaviour {


	public GameObject prefab;
	public float z_angle = 45;
	public int branches = 3;
	public int dimensions = 5;
	public float scaling = 0.66f; 

	// Use this for initialization
	void Start () {
		var root = (GameObject) Instantiate (prefab);
		root.transform.parent = transform;
		root.transform.localPosition = Vector3.zero;
		branch (root, 0);
	}

	void branch (GameObject parent, float d) {
		if (d > dimensions)
			return;

		for (int i = 0; i < branches; ++i) {
			var child1 = init (parent, i * (360/branches) + Random.Range(0, 360/branches/1.5f));
			branch (child1, d + 1);
		}
	}

	GameObject init(GameObject parent, float rot) {
		GameObject obj = (GameObject) Instantiate (prefab);
		obj.transform.parent = parent.transform;
		obj.transform.localScale = new Vector3 (scaling, scaling, scaling);
		obj.transform.localPosition = new Vector3 (0, 10, 0);
		obj.transform.localRotation = Quaternion.Euler (0, rot, z_angle + Random.Range(0, 30));
		return obj;
	}
}
