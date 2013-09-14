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
	
	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
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
	
}
