using UnityEngine;
using System.Collections;


public class RootBranch : Branch {
	
	public RootBranch (Transform parent) : base (parent, 0, 0) {
	}
	
	protected override GameObject initMesh (Transform parent, float rot)
	{
		GameObject obj = (GameObject) GameObject.Instantiate (TreeConfig.branchPrefab);
		obj.layer = TreeConfig.branchLayer;
		obj.transform.parent = parent;
		obj.transform.localScale = new Vector3 (1, 1, 1);
		obj.transform.localPosition = new Vector3 (0, 0, 0);
		obj.transform.localRotation = Quaternion.Euler (0, 0, 0);
		return obj;
	}
}


public class Branch {
	
	private GameObject mesh;
	private ArrayList branches = new ArrayList();
	
	public int depth;
	
	private float size = 0.1f;
	public float Size {
		get {
			return Mathf.Min (size, 1);
		}
		set {
			size = value;
		}
	}
	
	public float TotalSize {
		get {
			float sum = 0.0f;
			foreach (Branch branch in branches) {
				sum += branch.TotalSize;
			}
			return sum;
		}
	}
	
	public Branch (Transform parent, int parentDepth, int rotation) {
		depth = parentDepth + 1;
		mesh = initMesh (parent, rotation);
	}
	
	public void Grow(float delta) {
		
		if (depth < TreeConfig.maxDepth) {
			if (Size - (branches.Count*0.1f) > (1.0f - TreeConfig.numBranches*0.1f)) {
				AddBranch();
			}
		}
		
		foreach (Branch branch in branches) {
			branch.Grow (delta);
		}
		
		Size += delta;
		mesh.transform.localScale = Vector3.one * (Size * 0.66f);
	}
	
	private void AddBranch() {
		int sector = 360 / TreeConfig.numBranches;
		int rotation = branches.Count * sector + (int)Random.Range (0, sector/1.5f);
		
		Branch branch = new Branch (mesh.transform, depth, rotation);
		branches.Add (branch);
	}
	
	protected virtual GameObject initMesh (Transform parent, float rot) {
		
		GameObject obj = (GameObject) GameObject.Instantiate (TreeConfig.branchPrefab);
		obj.layer = TreeConfig.branchLayer;
		obj.transform.parent = parent;
		obj.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		obj.transform.localPosition = new Vector3 (0, 10, 0);
		obj.transform.localRotation = Quaternion.Euler (0, rot, TreeConfig.z_angle + Random.Range(0, 30));
		return obj;
	}
}
