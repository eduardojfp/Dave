using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class Cow : MonoBehaviour {
	
	CharacterController _controller;
  	Transform _transform;
	
	float speed = .1f;
	float gravity = 20f;
	Vector3 moveDirection;
	
	Vector3 target;
	float maxRotSpeed = 200.0f;
	float minTime = 0.1f;
	float velocity;
	
	bool change;
	float range;
	
	//int turn = 0;
	Vector3 _rotation;
	
	public int happiness = 0;
	
	bool isFed = false;
	float timeFed;
	float canMove;
	int numMilk;
	int noMoreMilk;
	
	public int maxHappiness;
	
	public Rain rainCheck;

	// Use this for initialization
	void Start () {
		
		_controller = GetComponent<CharacterController>();
      	_transform = GetComponent<Transform>();
		
		range = 2f;
	    target = GetTarget();
		
		timeFed = Timemanager.time;
		canMove = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		NewTarget();
		if(change)
		{
       		target = GetTarget ();
		}
		
		if(Currency.isSwamp == false)
		{
			if(rainCheck.ItsRaining == true)
				maxHappiness = 5;
			else
				maxHappiness = 6;
		}
		else
		{
			if(rainCheck.ItsRaining == false)
				maxHappiness = 2;
			else
				maxHappiness = 3;
		}
		
   		if(Vector3.Distance(_transform.position,target)>range)
		{
      			Move();
				canMove = 0;
   		}

		if(isFed == true)
		{
			if(Timemanager.time - timeFed > 1080)
			{
				numMilk = happiness;
				isFed = false;
			}
			if(Timemanager.time - timeFed > 1440)
			{
				happiness--;
				isFed = false;
			}
		}
		
		if(Timemanager.time - timeFed > 2880)
		{
			renderer.enabled = false;
			this.collider.enabled = false;	
			Destroy(this);
		}
		
		canMove += Time.deltaTime;
	
	}
	
	Vector3 GetTarget()
	{
   		return new Vector3(Random.Range (-50000,50000),0,Random.Range (-50000,50000));
	}
	
	void Move() 
	{	
		moveDirection = _transform.forward;
	    moveDirection *= speed;
	    moveDirection.y -= gravity;
	    _controller.Move(moveDirection * Time.deltaTime);
		
		
		var newRotation = Quaternion.LookRotation(target - _transform.position).eulerAngles;
    	var angles = _transform.rotation.eulerAngles;
    	_transform.rotation = Quaternion.Euler(angles.x, Mathf.SmoothDampAngle(angles.y, newRotation.y, ref velocity, minTime, maxRotSpeed), angles.z);
		
	}
	
	void NewTarget()
	{
	  	int choice = Random.Range (0,3);
	   	switch(choice)
		{
	      case 0: 
	         change = true;
	         break;
	      case 1: 
	         change = false;
	         break;
	      case 2:
	         target = _transform.position;
	         break; 
		} 
	}
	
	public void feed() 
	{
		isFed = true;
		timeFed = Timemanager.time;
				
		if(happiness < maxHappiness)
		{
			happiness++;
		}
		
		Debug.Log(happiness);
	}
	
	public int milk()
	{
		noMoreMilk = numMilk;
		happiness = 0;
		numMilk = 0;
		return noMoreMilk;
	}
}
