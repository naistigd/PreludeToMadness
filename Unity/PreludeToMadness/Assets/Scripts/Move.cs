using UnityEngine;
using System.Collections;


// Moves a game object from its current position to a specified 
// point along a straight line with a certain speed.
public class Move : MonoBehaviour 
{
	//-------------------------------------------------------------------------
	private Vector3 velocity_;	
	private Vector3 dest_;
	
	private bool isMoving_ = false;
	
	//-------------------------------------------------------------------------
	void Update () 
	{
		if (!isMoving_)
		{
			return;
		}
		
		Vector3 currPos = this.transform.position;
		
		Vector3 diff = currPos - dest_;
		Vector3 dv  = Time.deltaTime*velocity_;
		
		if (dv.magnitude >= diff.magnitude)
		{
			this.transform.position = dest_;
			isMoving_ = false;
			return;
		}
		
		this.transform.position = currPos + dv;
	}
	//-------------------------------------------------------------------------
	public void MoveTo(Vector2 pos, float speed)
	{
		Vector3 currPos = this.transform.position;
		velocity_ = new Vector3(pos.x, pos.y, currPos.z) - currPos;
		velocity_.Normalize();
		velocity_.Scale(new Vector3(speed, speed, speed));
		dest_ = new Vector3(pos.x, pos.y, currPos.z);
		isMoving_ = true;
	}
	//-------------------------------------------------------------------------
	public bool IsMoving()
	{
		return isMoving_;
	}
	//-------------------------------------------------------------------------
}
