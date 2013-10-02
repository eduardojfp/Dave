using UnityEngine;
using System.Collections;

[RequireComponent (typeof( MeshRenderer))]
[RequireComponent (typeof (MeshFilter))]
public class CropPlant : MonoBehaviour {
	
	private int cropState;
	
	public Mesh seedMesh;
	public Material seedMaterial;
	
	public Mesh firstGrowMesh;
	public Material firstGrowMaterial;
	
	public Mesh secondGrowMesh;
	public Material secondGrowMaterial;
	
	public Mesh tomatoMesh;
	public Material tomatoMaterial;
	
	public Mesh cornMesh;
	public Mesh therestmesh;
	
	MeshRenderer meshRenderer;
	MeshFilter meshFilter;
	
	// Use this for initialization
	void Start () {
		this.meshRenderer = GetComponent<MeshRenderer>();
		this.meshFilter = GetComponent<MeshFilter>();
		this.cropState = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void plantSeeds () {
		if (this.cropState == 0) {
			this.meshFilter.mesh = this.seedMesh;
			this.renderer.material = this.seedMaterial;
			this.cropState = 1;
		}
	}
	
	public void grow() {
		//Growing from seeds
		if (this.cropState == 1) {
			this.meshFilter.mesh = this.firstGrowMesh;
			this.renderer.material = this.firstGrowMaterial;
			this.cropState = 2;
		}
		//growing from first growth
		else if (this.cropState == 2) {
			this.meshFilter.mesh = this.secondGrowMesh;
			this.renderer.material = this.secondGrowMaterial;
			this.cropState = 3;
		}
		//growing from second growth into final form
		else if (this.cropState == 3) {
			//NOTE: Should grow into what the plant actually is, not just tomatoes
			
			if (true /*is growing into tomatoes*/) {
				this.meshFilter.mesh = this.tomatoMesh;
				this.renderer.material = this.tomatoMaterial;
			}
			
			this.cropState = 4;
		}
		else {
			this.kill();
		}
		
	}
	
	public void kill() {
		this.meshFilter.mesh = null;
		this.renderer.material = null;
		this.cropState = 0;
	}
}
