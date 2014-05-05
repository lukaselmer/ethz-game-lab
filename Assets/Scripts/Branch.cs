using UnityEngine;
using System.Collections;

public class Branch : MonoBehaviour, Selectable {

	public Treee tree;
	private Branch parent;
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
			float sum = size / Mathf.Pow (tree.numBranches, depth-1);
			foreach (Branch branch in branches) {
				sum += branch.TotalSize;
			}
			return sum;
		}
	}

	public static Branch InstantiateRoot (Treee tree, Branch parentBranch, Transform parent) {
		return Branch.Instantiate (tree, 
		                           parentBranch,
		                           parent, 
		                           0, 
		                           0, 
		                           Random.Range(-5, 5), 
		                           0);
	}

	public static Branch InstantiateChild (Treee tree, Branch parentBranch, Transform parent, int parentDepth, float yAngle) {	
		return Branch.Instantiate (tree, 
		                           parentBranch,
		                           parent, 
		                           parentDepth, 
		                           yAngle, 
		                           tree.zAngle + Random.Range(-15, 15), 
		                           Random.Range(tree.yOffsetMin, tree.yOffsetMax)); 
	}
	
	public static Branch Instantiate (Treee tree, Branch parentBranch, Transform parent, int parentDepth, float yAngle, float zAngle, int yOffset) {
		GameObject obj = (GameObject)Instantiate (tree.branchPrefab);
		Branch branch = obj.AddComponent<Branch> ();
		branch.tree = tree;
		branch.parent = parentBranch;
		branch.depth = parentDepth+1;
		obj.layer = LayerMask.NameToLayer("Branch");
		obj.transform.parent = parent;
		obj.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		obj.transform.localPosition = new Vector3 (0, yOffset, 0);
		obj.transform.localRotation = Quaternion.Euler (0, yAngle, zAngle);
		return branch;
	}

	public void Grow(float delta) {
		
		if (depth < tree.maxDepth) {
			if (Size - (branches.Count*0.1f) > (1.0f - tree.numBranches*0.1f)) {
				AddBranch();
			}
		}
		
		foreach (Branch branch in branches) {
			branch.Grow (delta);
		}
		
		Size += delta;
		transform.localScale = Vector3.one * (Size * 0.66f);
	}
	
	private void AddBranch() {
		int sector = 360 / tree.numBranches;
		int rotation = branches.Count * sector + (int)Random.Range (0, sector/1.5f);
		
		Branch branch = Branch.InstantiateChild (this.tree, this, this.transform, depth, rotation); //new Branch (mesh.transform, depth, rotation);
		branches.Add (branch);
	}

	public void Attach(Treee tree) {
		var oldDepth = depth;
		parent.branches.Remove (this);

		AdjustChildren (depth - 1, tree);
		tree.Root = this;
		this.transform.parent = tree.transform;
		this.Size = Mathf.Pow (0.66f, oldDepth-1); 
		this.Grow (0);
		this.transform.localPosition = Vector3.zero;
		this.transform.localRotation = Quaternion.identity;
	}

	public void AdjustChildren (int n, Treee tree) {
		
		Size *= Mathf.Pow (0.66f, n);
		depth -= n;
		this.tree = tree;
		foreach (Branch branch in branches) {
			branch.AdjustChildren(n, tree);
		}
	}

	public void Select() {
		Transform saw = this.gameObject.transform.FindChild ("Saw");
		saw.renderer.enabled = true;
	}
	public void UnSelect() {
		Transform saw = this.gameObject.transform.FindChild ("Saw");
		saw.renderer.enabled = false;
	}
}
