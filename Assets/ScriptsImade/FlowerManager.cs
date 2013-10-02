using UnityEngine;
using System.Collections;

public class FlowerManager : MonoBehaviour {
	
	public Mesh normalFlower;
	public Mesh rareFlower;
	
	public float TimePicked;
	public bool FlowerIsGone = false;
	public bool awesome = false;
	
	MeshFilter mesh;
	// Use this for initialization
	void Start () {
		
		//MeshFilter mesh = GetComponent<MeshFilter>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if(FlowerIsGone == true) {
			if((Timemanager.time - TimePicked) > 1080) {
				int rare = Random.Range(1 ,15);
				if(rare > 1)
				{
					awesome = true;
					Debug.Log ("worked");
				}
				renderer.enabled = true;
				this.collider.enabled = true;
				FlowerIsGone = false;
				TimePicked = 0;
				
				if(awesome == true)
					this.turnRare();
			}
		}
		
	}
	
	public  void DestroyFlower () {
		//i need to add an if to player class
		renderer.enabled = false;
		this.collider.enabled = false;
		TimePicked = Timemanager.time;
		FlowerIsGone = true;	
	}
	
	void turnRare()
	{
		MeshFilter mesh = this.GetComponent<MeshFilter>();
		mesh.mesh = rareFlower;
	}
	

}