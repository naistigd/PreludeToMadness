using UnityEngine;
using System.Collections;

public class InteractableObserver : MonoBehaviour 
{
	private InteractableManager manager;
	public GUISkin guiSkin; 	
	
	private bool isRatKillable = false;
	private string guiMsg = "";
	
	void Awake() 
	{
		manager = GetComponent<InteractableManager>();
		manager.Register(this);
	}
	
	public void Notify(int selected, int clicked)
	{
		
		// put chocolate on the ground
		if (selected == 3 && clicked == 5)
		{
			manager.SetIsInventory(3, false);
			manager.SetPosition(3, new Vector2(-2.6f, - 2.0f));
			manager.SetIsActive(5, false);
			manager.SetPosition(2, new Vector2(-3.5f, - 2.0f));
			isRatKillable = true;
		}
		
		// kills the rat
		if (selected == 0 && clicked == 2 && isRatKillable == true)
		{
			manager.SetIsActive(2, false);
			manager.SetIsActive(1, true);
			manager.SetIsInventory(1, false);
			manager.SetPosition (1, new Vector2(-3.5f, - 2.0f));
			manager.Unselect();
		}
		
		// opens the door
		if (selected == 1 && clicked == 4)
		{
			guiMsg = "You win!!!!";
			manager.Unselect();
			Application.LoadLevel(2);
		}
	}
	
	void Update()
	{
		Vector2 posGuard = manager.GetPosition(6);
		
		// if the guard is visible
		if (posGuard.x < 8.0f && posGuard.x > -3.0f)
		{
			// and chocolate is not in the inventory
			if (!manager.IsInInventory(3) && manager.IsActive(3))
			{
				Application.LoadLevel(3);
			}
			
			// and rat bones is not in the inventory
			if (!manager.IsInInventory(1) && manager.IsActive(1))
			{
				Application.LoadLevel(3);
			}			
		}
	}
	
	void OnGUI()
	{
		GUI.skin = guiSkin;
		Rect rect = new Rect(500.0f, 10.0f, 120.0f, 50.0f);
		
		GUI.Label(rect, guiMsg);
	}
}
