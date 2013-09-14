using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour 
{
	public bool isCollectable = false;
	public bool isInInventory = false;
	public bool isActive = true;
	
	private InteractableManager manager = null;
	private int id = -1;
	
	public void Start()
	{	
		if (!isActive)
		{
			gameObject.SetActive(false);
		}
	}
	
	public void Update() 
	{
	}
	
	public void OnMouseDown()
	{
		manager.Notify(id);
	}

	public void Register(InteractableManager manager)
	{
		this.manager = manager;
	}

	public int GetId()
	{
		return this.id;
	}

	public void SetId(int id)
	{
		this.id = id;
	}
}
