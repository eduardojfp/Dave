using UnityEngine;
using System.Collections;

public class CropTileClass : MonoBehaviour
{	//variables for dirt dry
	public Mesh dirtMesh;
	public Material dirtMaterial;
	
	//variables for tilled dry
	public Mesh tilledMesh;
	public Material tilledMaterial;
	
	//variable for wet dirt mesh 
	public Mesh wetdirtMesh; 
	public Material wetdirtMaterial;
	
	//variable for wet tiled mesh
	public Mesh wettilled;  
	public Material wettilledMaterial;
	
	
	public int howmanywater; // how many times the specific crop needs to be watered
	public string cropname;
	
	
	public bool hascroptarp; //tells if you bought tarp for the crop
	public int tarpstrength = 2; //strength of tarp
	
	public CropPlant crop; //lets us change models of the crop
	
	int Cropstage = 0; //0 is untilled. 1 is tilled. 2 is planted. 3 is watered. 4 is harvestable
	float timeLastWatered = 0; //the last time the crop was watered
	public int timesWatered = 999; //how many times the crop was correctly watered 
	int badwater = 0; //how many times you incorrectly watered the plant
	
	bool canbewatered; //bool if the crop can be watered
	public Rain DatRainHelper;  //used to see if its raining in order to water
	
	bool firstgrowstage = false; //bools that show if the crop has hit a paticular stage.
	bool secondgrowstage = false;
	
	
	
	
	// Use this for initialization
	void Start ()
	{
		this.DatRainHelper = (Rain)Object.FindObjectOfType(typeof(Rain));
		this.crop = (CropPlant)Instantiate((Object)this.crop,
		this.transform.position + this.crop.transform.position,
		this.transform.rotation * this.crop.transform.rotation);
		this.crop.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		
		// THIS KILLS A PLANT IF YOU DON'T WATER IT OFTEN ENOUGH
		if (Cropstage == 3 && (Timemanager.time - timeLastWatered) > 2880) //2 days
			killcrop();
			
		
		// IF CROPS ARE READY TO BE WATERED AGAIN, IT CHANGES TO DRY TILLED MODEL
		if (Cropstage == 3 && (Timemanager.time - timeLastWatered) > 480) { //8 hours
			
			//ready to be watered
			canbewatered = true; 
			
			//changes to dry
			MeshFilter mesh = this.GetComponent<MeshFilter>();
			mesh.mesh = this.tilledMesh;
			this.renderer.material = this.tilledMaterial;
			
			
			//if plant is 30%grown
			if(timesWatered >= (.3*(float)howmanywater) && timesWatered < (.5*(float)howmanywater) && firstgrowstage == false){
				crop.grow();
				firstgrowstage = true;
			}
			
			//if plant is 60% grown
			else if(timesWatered >= (.6*(float)howmanywater) && timesWatered < (.8*(float)howmanywater) && secondgrowstage == false) {
				crop.grow();
				secondgrowstage = true;
			}
			
			//crop is full grown
			else if(timesWatered == howmanywater) {
				crop.grow();
				Cropstage = 4;
				badwater = 0;
				timesWatered = 999;
				
				
			}
			//If its rainning, it waters the crop
			if(DatRainHelper.ItsRaining == true){ //checks to see if its raining, so that the crop is watered for the second half of the day
				watercrop();
				if(hascroptarp == false) //if you didn't buy crop tarp for plants
					watercrop();
				else if(hascroptarp ==true){ //decreases tarp strength when the rain would hurt crop
					tarpstrength--;
					if(tarpstrength == 0) //deletes tarp when it has no strength left
						hascroptarp = false;
				}
			}
			
			
		}
	}
	
	public bool plantseed (int numberofwaters, string acropname)
	{
		if (Cropstage == 1 ) {
		
			Cropstage = 2;
			howmanywater = numberofwaters;
			cropname = acropname;
			this.crop.plantSeeds(); //model changes to seed
			return true;
		}
		else
		{
			return false;
		}
	}
	public void killcrop () {
		//change mesh to dirt
		MeshFilter mesh = this.GetComponent<MeshFilter>();
		mesh.mesh = this.dirtMesh;	
		this.renderer.material = this.dirtMaterial;
			
			
			crop.kill(); 
			Cropstage = 0; //resets crop
			timesWatered =999;
			badwater = 0;
		
	}
	
	public void watercrop ()
		{
		// IF WATERING FOR THE FIRST TIME
		if (Cropstage == 2) 
		{	Cropstage = 3;
			timeLastWatered = Timemanager.time;
			timesWatered = 1;
			canbewatered = false; //now watered. dont water again
			maketilledwet();
		
			
		} // IF YOU WATER CORRECTLY
		else if (canbewatered == true) //Plants can be watered every 12 hours
		{ 
			
			timeLastWatered = Timemanager.time;
			timesWatered++;
			maketilledwet();
			
			
		
			
			
			
		}	
			 //IF YOU WATER A PLANT TOO MANY TIMES. IT DIES
		else if(Cropstage ==3)
		{
			badwater++;
			if(badwater > 4)
				killcrop();
			
		}
		/*//YOURE WATERING NOT SOILED DIRT. YOURE PROBABLY STUPID
		else {
			MeshFilter mesh = this.GetComponent<MeshFilter>();
			mesh.mesh = this.wetdirtMesh;
			this.renderer.material = this.wetdirtMaterial;
			
			
		}*/
	}
	
		
	
	
	public bool harvestcrop ()
	{
		if (Cropstage == 4) {
			Cropstage = 0;
			MeshFilter mesh = this.GetComponent<MeshFilter>();
			mesh.mesh = this.dirtMesh;
			this.renderer.material = this.dirtMaterial;
			crop.kill();
			
			return true;
		
			
		}
		else return false;
		
	}
	
	public void tillcrop ()
	{
		if (Cropstage == 0) {
			
			MeshFilter mesh = this.GetComponent<MeshFilter>();
			mesh.mesh = this.tilledMesh;
			this.renderer.material = this.tilledMaterial;
			
			Cropstage = 1;
		}
	}
	
	public void puttarponcrop () {
		hascroptarp = true;
		
	}
	
	public void maketilledwet() {
		//Change model to wet
			MeshFilter mesh = this.GetComponent<MeshFilter>();
			mesh.mesh = this.wettilled;
			this.renderer.material = this.wettilledMaterial;
			
		}
	public void die ()
	{
		int death = Random.Range (1,10);
		if(Cropstage == 3 && death == 1)
		{
			killcrop();
		}
	}
	
	void OnTriggerEnter(Collider other)
	{	
		//if(other.gameObject.name.Equals("CropCollider"))
		{	
			if(InventoryManager.equippeditem().Equals("Hoe Lvl 2")){
				tillcrop();
				
			}
			
			else if(InventoryManager.equippeditem().Equals ("Waterbucket Lvl 2"))
				watercrop();
		}	
	}		
}
