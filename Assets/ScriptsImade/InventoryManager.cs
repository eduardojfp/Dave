using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
	static int equipped {get;set;}
	public static List<Toolmanager> Inventory;
	public static Toolmanager Waterbucket = new Toolmanager("Waterbucket", 1, 6, 5, 0);
	public static Toolmanager Tomato = new Toolmanager ("Tomato",0,0,1, 0);
	public static Toolmanager T_Seeds = new Toolmanager ("Tomato Seed",1, 3, 3, 6);
	public static Toolmanager Hoe = new Toolmanager ("Hoe", 1, 10,1, 0);
	public static Toolmanager Hands = new Toolmanager("Hands", 1, 0, 1, 0);
	public static Toolmanager Flower = new Toolmanager("Flower",1,0,0, 0);
	public static Toolmanager Rare_Flower = new Toolmanager("Rare Flower", 0, 0, 0, 0);
	public static Toolmanager C_Seeds = new Toolmanager ("Corn Seed", 1, 3, 3, 4);
	public static Toolmanager Corn = new Toolmanager ("Corn", 0, 0, 0, 0);
	public static Toolmanager Feed = new Toolmanager("Feed", 1, 2, 5, 0);
	public static Toolmanager Egg = new Toolmanager("Egg", 0, 0, 0, 0);
	public static Toolmanager Waterbucket2 = new Toolmanager("Waterbucket Lvl 2", 2, 5, 17, 0);
	public static Toolmanager Hoe2 = new Toolmanager("Hoe Lvl 2", 2, 8, 0, 0);
	public static Toolmanager Milker = new Toolmanager("Milker", 0, 5, 1, 0);
	public static Toolmanager Milk = new Toolmanager("Milk", 0, 0, 0, 0);
	public static Toolmanager Rain_Tarp = new Toolmanager("Rain Tarp", 0, 2, 0, 0);
	public static Toolmanager Sickle = new Toolmanager("Sickle", 1, 8, 1, 0);
	
	// Use this for initialization
	void Start () {
		equipped = 0;
		Inventory = new List<Toolmanager> (100);
		Inventory.Add(Hands);
		Inventory.Add(Tomato);
		Inventory.Add(T_Seeds);
		Inventory.Add(Hoe);
		Inventory.Add(Waterbucket);
		Inventory.Add(Flower);
		Inventory.Add(C_Seeds);
		Inventory.Add(Feed);
		Inventory.Add(Corn);
		Inventory.Add(Egg);
		Inventory.Add(Waterbucket2);
		Inventory.Add(Hoe2);	
		Inventory.Add(Milker);
		Inventory.Add(Milk);
		Inventory.Add(Rain_Tarp);
		Inventory.Add(Sickle);
		Inventory.Add(Rare_Flower);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public static void equipnextitem () {
		if(equipped < (Inventory.Count - 1))
		{
			Player.updateVisibleTool();
			equipped++;
			if(Inventory[equipped].howmany == 0)
			{
				equipnextitem();
			}
		}
		else 
		{
			equipped = 0;
			if(Inventory[equipped].howmany == 0)
			{
				equipnextitem();
			}
		}
	}
	
	public static void equippreviousitem () {
		if(equipped > 0)
		{
			equipped--;
			if(Inventory[equipped].howmany == 0)
			{
				equippreviousitem();
			}
		}
		else 
		{
			equipped = (Inventory.Count - 1);
			if(Inventory[equipped].howmany == 0)
			{
				equippreviousitem();
			}
		}
	}
	
	public static string equippeditem () {
		return Inventory[equipped].Tooltype;
	}
	
	 void OnGUI (){
		int watercanLVL1cheat = Waterbucket.howmany - 1;
		int watercanLVL2cheat = Waterbucket2.howmany - 1;
		
		GUI.Box(new Rect(480, 10, 120, 50), "Item Equipped");
		if(Inventory[equipped].Tooltype == "Waterbucket")
			GUI.Label(new Rect( 480, 30, 120, 100), Inventory[equipped].Tooltype + " " + (watercanLVL1cheat));
		else if(Inventory[equipped].Tooltype == "Waterbucket2")
			GUI.Label(new Rect( 480, 30, 120, 100), Inventory[equipped].Tooltype + " " + (watercanLVL2cheat));
		else if(Inventory[equipped].howmany > 1 && (Inventory[equipped].Tooltype != "Waterbucket" && Inventory[equipped].Tooltype != "Waterbucket2"))
			GUI.Label(new Rect( 480, 30, 120, 100), Inventory[equipped].Tooltype + " " + Inventory[equipped].howmany);
		else		
			GUI.Label(new Rect( 480, 30, 120, 100), Inventory[equipped].Tooltype);
		
		/* this shows full inventory
		GUI.Box(new Rect(20, 130, 120, 200), "Inventory");
		for(int i = 0; i < Inventory.Count; i++)
		{
			GUI.Label(new Rect(30, (145 +i*20), 120, 100), Inventory[i].Tooltype + " " + Inventory[i].howmany);
		}
		*/
	}
	
	
	
}
