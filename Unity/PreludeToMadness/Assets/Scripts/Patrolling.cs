using UnityEngine;
using System.Collections;

public class Patrolling : MonoBehaviour 
{
	public float speed = 0.2f;
	public float xMax = 10.0f;
	public float xMin = -10.0f;
	public bool isPatrolling = false;
	
	private CharacterController controller;
	private Vector3 velocity = new Vector3(1.0f, 0.0f, 0.0f);
	private float time = 0.0f;
	
	//-------------------------------------------------------------------------
	void Start () 
	{
		controller = GetComponent<CharacterController>();
	}
	//-------------------------------------------------------------------------
	void Update () 
	{
		
		if (!isPatrolling)
		{
			return;
		}
		
		float dt = Time.deltaTime*speed;
		controller.Move(dt*velocity);
		
		time += Time.deltaTime;
		
		
		float x = controller.transform.position.x;
		
		if (x > xMax)
		{
			velocity.x = -1.0f;
			return;
		}
		
		if (x < xMin)
		{
			velocity.x = 1.0f;
			return;
		}
		
	}
	//-------------------------------------------------------------------------
	public bool IsGoingLeft()
	{
		return velocity.x == -1;
	}
	//-------------------------------------------------------------------------
}
