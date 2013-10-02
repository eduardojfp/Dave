using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour {
	
	public Mesh uncut;
	public Mesh cut;
	
	bool isCut = false;
	
	float timeCut;

	// Use this for initialization
	void Start () {
		MeshFilter mesh = this.GetComponent<MeshFilter>();
		mesh.mesh = this.uncut;
	}
	
	// Update is called once per frame
	void Update () {
		if(Timemanager.time - timeCut > 2880)
		{
			MeshFilter mesh = this.GetComponent<MeshFilter>();
			mesh.mesh = uncut;
			isCut = false;
		}
	
	}
	
	public bool cutGrass ()
	{
		
		if(isCut == false)
		{
			MeshFilter mesh = this.GetComponent<MeshFilter>();
			mesh.mesh = this.cut;
			timeCut = Timemanager.time;
			return true;
		}
		else
			return false;
	}
}
