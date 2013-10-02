using UnityEngine;
using System.Collections;

public class GrassField : MonoBehaviour {
	
	public int xSize;
	public int ySize;
	
	public Grass grass;
	private Grass[,] meadow;

	// Use this for initialization
	void Start () {
		
		meadow = new Grass[xSize, ySize];
		
		for( int i = 0; i < xSize; i++){
			for(int j = 0; j< ySize; j++){
				createGrassTile(i,j);
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void createGrassTile(int x, int y) {
		if (meadow[x,y] == null) {
			Vector3 tilePosition = this.transform.position + (this.transform.forward*x*1) - (this.transform.right*y*2);
			Quaternion tileRotation = this.transform.rotation * grass.transform.rotation;
			meadow[x,y] = (Grass)Instantiate(grass, tilePosition, tileRotation);
			
			meadow[x,y].transform.parent = this.transform;
		}
	}
}
