using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour 
{
	public bool isCollectable = false;
	public bool isInInventory = false;
	public bool isActive = true;
	
	
	private Vector2 scale_;
	private Vector2 invScale_; // scale of the object in the inventory
	private InteractableManager manager = null;
	private int id = -1;
	//-------------------------------------------------------------------------
	public void Start()
	{	
		if (!isActive)
		{
			gameObject.SetActive(false);
		}
		
		float sx = this.transform.localScale.x;
		float sy = this.transform.localScale.y;
		scale_ = new Vector2(sx, sy);
		
		
		float isx = 0.5f;
		float isy = sy/sx*0.5f;
		
		invScale_ = new Vector2(isx, isy);
	}
	//-------------------------------------------------------------------------
	public void Update() 
	{
	}
	//-------------------------------------------------------------------------
	public void OnMouseDown()
	{
		manager.Notify(id);
	}
	//-------------------------------------------------------------------------
	public void Register(InteractableManager manager)
	{
		this.manager = manager;
	}
	//-------------------------------------------------------------------------
	public int GetId()
	{
		return this.id;
	}
	//-------------------------------------------------------------------------
	public void SetId(int id)
	{
		this.id = id;
	}
	//-------------------------------------------------------------------------
	public void SetScaleNormal()
	{
		this.transform.localScale = new Vector3(scale_.x, scale_.y, 0.1f);
	}
	//-------------------------------------------------------------------------
	public void SetScaleInventory()
	{
		this.transform.localScale = new Vector3(invScale_.x, invScale_.y, 0.1f);
	}
	//-------------------------------------------------------------------------
}
