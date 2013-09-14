using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractableManager : MonoBehaviour 
{	

	public Interactable[] interactables;		// an array of all our..
												// .. interactables
	private int selectedInteractable = -1;		// id of the currently selected
												// interactable
	private InteractableObserver observer;		// an obsever we report to
	private List<Interactable> inventory;		// List to get the order in our
												// inventory right
	
	void Awake() 
	{
		inventory = new List<Interactable>();

		// register ourselve to each interactable object and set its id
		// which is used to reference it.
		for (int i = 0; i < interactables.Length; i++)
		{
			interactables[i].Register(this);
			interactables[i].SetId(i);

			if (interactables[i].isInInventory)
			{
				inventory.Add(interactables[i]);
			}	
		}
	}
	
	/*!
	** Sets interactable gameObjects either active or passive depending wether
	** they are in the inventory, selected, or not in the inventory and depending
	** on wether the inventar should be shown or not. 
	**
	** NOTE: (most/all) Logic in here can be delete until the state of the 
	**       class changes and does not need to be executed every frame.
	*/
	void Update()
	{
		// TODO: Make more generic (depending on the camera settings)
		float x = -2.0f;
		float y = -4.5f;	
		float xOffset = 1.0f;
		
		foreach (Interactable inter in inventory)
		{
			if (inter.isActive)
			{
				// compute the position of each interactable object in the
				// inventory and set it active
				inter.gameObject.transform.position = new Vector3(
						x, y, Constants.INV_ITEM_POS_Z
					);
				inter.gameObject.SetActive(true);
				x += xOffset;
			}
			else 
			{
				inter.gameObject.SetActive(false);
			}
		}

		TranslateSelectedAndSetActive();
		
		// unselect an interactable if the right mouse button was clicked
		if (Input.GetMouseButton(1)) 
		{
      		selectedInteractable = -1;
   		}
	}
	
	/*!
	** Allow one observer to register.
	*/
	public void Register(InteractableObserver observer)
	{
		this.observer = observer;
	}
		
	/*!
	**	Called by an [Interactable] if it was left-clicked.
	*/
	public void Notify(int id)
	{
		bool isCollectable = interactables[id].isCollectable;
		bool isInInventory = interactables[id].isInInventory;
		
		if (isCollectable && !isInInventory && selectedInteractable == -1)
		{
			interactables[id].isInInventory = true;
			inventory.Add(interactables[id]);
			return;
		}

		if (isCollectable && isInInventory && selectedInteractable == -1)
		{
			selectedInteractable = id;
			return;
		}
		
		if (selectedInteractable != -1)
		{
			observer.Notify(selectedInteractable, id);
			return;
		}
	}
	
	/*!
	**	Sets the [isActive] flag in an [Interactable].
	*/
	public void SetIsActive(int id, bool isActive)
	{
		// if the object is to be set inactive is selected: unselect it.
		if (!isActive && id == selectedInteractable)
		{
			selectedInteractable = -1;
		}

		interactables[id].isActive = isActive;
		interactables[id].gameObject.SetActive(isActive);
	}
	
	public void SetIsInventory(int id, bool isInInventory)
	{
		if (!isInInventory && id == selectedInteractable)
		{
			selectedInteractable = -1;
		}
		
		if (!isInInventory)
		{
			Vector3 pos = interactables[id].transform.position;
			pos.z = Constants.LAYER_ITEM_Z;
			interactables[id].transform.position = pos;
			inventory.Remove(interactables[id]);
		}
		else
		{
			Vector3 pos = interactables[id].transform.position;
			pos.z = Constants.LAYER_INVENTORY_ITEM_Z;
			interactables[id].transform.position = pos;			
		}
		
		interactables[id].isInInventory = isInInventory;
	}
	
	public void Unselect()
	{
		selectedInteractable = -1;
	}
	
	public void SetPosition(int id, Vector2 pos)
	{
		Vector3 pos3 = interactables[id].transform.position;
		pos3.x = pos.x;
		pos3.y = pos.y;
		interactables[id].transform.position = pos3;
	}
	
	public Vector2 GetPosition(int id)
	{
		Vector3 pos3D = interactables[id].transform.position;
		Vector2 pos2D = new Vector2();
		pos2D.x = pos3D.x;
		pos2D.y = pos3D.y;
		
		return pos2D;
	}
	
	public bool IsInInventory(int id)
	{
		return interactables[id].isInInventory;
	}
	
	public bool IsActive(int id)
	{
		return interactables[id].isActive;
	}
	
	/*!
	** Gets the name for an [Interactable].
	*/
	public string GetName(int id)
	{
		if (id >= interactables.Length)
		{
			return "";
		}

		return interactables[id].gameObject.name;	
	}

	/*!
	** Gets the [id] for an [Interactables] name.
	*/
	public int GetId(string name)
	{
		foreach (Interactable inter in interactables)
		{
			string interName = inter.gameObject.name;

			if (interName == name)
			{
				return inter.GetId();
			}
		}

		return -1;
	}
	
	private void TranslateSelectedAndSetActive()
	{
		// make the selected item follow the mouse
		if (selectedInteractable != -1)
		{
			GameObject obj = interactables[selectedInteractable].gameObject;
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			obj.SetActive(true);
			obj.transform.position = new Vector3(
					pos.x + Constants.MOUSE_OFFSET, 
					pos.y - Constants.MOUSE_OFFSET, 
					0.0f
				);
		}
	}
	
}
