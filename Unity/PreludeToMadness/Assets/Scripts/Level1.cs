using UnityEngine;
using System.Collections;

public class Level1 : MonoBehaviour 
{	
	//-------------------------------------------------------------------------
	//					CLASS MEMBER DECLARATIONS/DEFINITIONS
	//-------------------------------------------------------------------------
	public DialogField DialogField;
	public Patrolling Patrolling;
	public BackgroundMusic BackgroundMusic;
	public Move RatMove;
	public SpriteAnimation RatAnimation;
	public Sound CellDoorSoundSound;
	
	public GameObject IntroBackground; // ugly
	
	private InteractableManager manager;
	private bool isRatKillable = false;
	
	
	delegate void UpdateHandler();
	private UpdateHandler updateHandler_;
	private float time_ = 0.0f;
	
	
	private Fader bgmFader_;
	private Fader introBgFader_;
	
	// game logic stuff
	private bool hasIntroDialogStarted_ = false;
	private bool guardTextStarted_ = false;
	private bool lockAlreadyClicked_ = false;
	private bool guardAlreadyClicked_ = false;
	private bool stoneAlreadyClicked_ = false;
	
	//-------------------------------------------------------------------------
	//						CLASS METHOD DEFINITIONS
	//-------------------------------------------------------------------------
	void Awake() 
	{
		manager = GetComponent<InteractableManager>();
		manager.Register(this);
		updateHandler_ = sceneIntro;
		
		// play the cell door sound
		CellDoorSoundSound.Play();
		
		// play background music but set volume to 0.
		// also set the fader for the bgm		
		BackgroundMusic.Play();
		BackgroundMusic.Loop(true);
		BackgroundMusic.SetVolume(0.0f);
		
		bgmFader_ = new Fader(0.0f, 30.0f, 0.0f);
		
		//
		introBgFader_ = new Fader(1.0f, 0.0f, 5.0f);
	}
	//-------------------------------------------------------------------------
	public void Notify(int selected, int clicked)
	{		
		// HANDLE HINTS.
		if (clicked == 4 && !lockAlreadyClicked_)
		{
			// if lock was clicked first time ...
			lockAlreadyClicked_ = true;
			Color ye = new Color(1.0f, 1.0f, 0.0f, 1.0f);
			
			string text0 = "The keyhole of this lock is unusual in that it seems to aim both ways, inside and out.";
			string text1 = "Why anyone would make a lock that allows the prisoner access to it is beyond you, but you are grateful for it.";
			string text2 = "Perhaps if you could find something to pick the lock...";
			DialogField.Display(text0, ye, 0.0f, 3.0f);
			DialogField.Display(text1, ye, 3.0f, 7.0f);
			DialogField.Display(text2, ye, 7.0f, 10.0f);
		}
		
		if (clicked == 6 && !guardAlreadyClicked_)
		{
			// if guard was clicked first time ...
			guardAlreadyClicked_ = true;
			Color ye = new Color(1.0f, 1.0f, 0.0f, 1.0f);
			
			string text0 = "The soldier appears to be in his mid-20's but despite his youth, you sense a coiled lethality.";
			string text1 = "You judge rather early on that he will make good on his threats, and he is a competent soldier.";
			string text2 = "You doubt it would be an easy one-on-one head on fight if you were to engage him.";
			DialogField.Display(text0, ye, 0.0f, 3.0f);
			DialogField.Display(text1, ye, 3.0f, 7.0f);
			DialogField.Display(text2, ye, 7.0f, 10.0f);
		}
		
		if (clicked == 0 && !stoneAlreadyClicked_)
		{
			// if stone was clicked first time ...
			stoneAlreadyClicked_ = true;
			Color ye = new Color(1.0f, 1.0f, 0.0f, 1.0f);
			
			string text0 = "Probing the floor with your hands, you happen upon a stone in the wall that seems to be loose.";
			string text1 = "A few palm-pounds later and you manage to successfully dislodge the stone from the wall.";
			string text2 = "You discover that it has a conveniently sharp point on the opposite end. ";
			string text3 = "This should come in handy somehow...";
			DialogField.Display(text0, ye, 0.0f, 3.0f);
			DialogField.Display(text1, ye, 3.0f, 7.0f);
			DialogField.Display(text2, ye, 7.0f, 10.0f);
			DialogField.Display(text3, ye, 10.0f, 12.0f);
		}
		
		
		// put chocolate on the ground
		if (selected == 3 && clicked == 5)
		{
			manager.SetIsInventory(3, false);
			manager.SetPosition(3, new Vector2(-2.2f, - 2.2f));
			manager.SetIsActive(5, false);
			RatAnimation.Play(0.5f);
			RatMove.MoveTo(new Vector2(-3.5f, - 2.0f), 4.0f);
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
			manager.Unselect();
			Application.LoadLevel(2);
		}
	}
	//-------------------------------------------------------------------------
	void Update()
	{
		// STATE INDEPENDENT UPDATES
		
		// update time
		time_ += Time.deltaTime;
		
		// update the bgm fader
		bgmFader_.Update(Time.deltaTime);
		
		// update vol of the bgm according to its fader
		BackgroundMusic.SetVolume(bgmFader_.GetAlpha());
			
		
		// STATE SPECIFIC UPDATES
		updateHandler_();
	}
	//-------------------------------------------------------------------------	
	void OnGUI()
	{
		
	}
	//-------------------------------------------------------------------------
	private void sceneIntro()
	{
		
		// update intro bg fader and fade the bg accordingly; UGLY
		introBgFader_.Update(Time.deltaTime);
		IntroBackground.renderer.material.color = 
			new Color(0.0f, 0.0f, 0.0f, introBgFader_.GetAlpha());
		
		// if the time is greater then 8.0 sec start playing the intro
		// dialog
		if (time_ > 8.0f && !hasIntroDialogStarted_)
		{
			hasIntroDialogStarted_ = true;
			Color yellow = new Color(1.0f, 1.0f, 0.0f, 1.0f);
			
			string text1 = "You awaken with a start as the guard slams the cold iron bars shut.";
			string text2 = " He regards you with a look of pity and disgust.";
			
			DialogField.Display(text1, yellow, 0.0f, 5.0f);
			DialogField.Display(text2, yellow, 5.5f, 8.5f);
			
			// fade in bgm
			bgmFader_.FadeIn();
		}
		
		// fade in the actaul scene (fade out intro background)
		if (time_ > 14.0f)
		{
			introBgFader_.FadeOut();
		}
		
		// after 17 sec switch to guard dialog state
		if (time_ > 20.0f)
		{
			IntroBackground.SetActive(false);	
			updateHandler_ = guardDialog;
		}
	}	
	//-------------------------------------------------------------------------
	private void guardDialog()
	{
		// THIS FUNCTION HANDLES THE BEGINNING OF THE SCENE WHERE THE GUARD
		// IS TALKING TO THE PLAYER
		
		// while talking the guard is not patrolling 
		Patrolling.isPatrolling = false;
		
		// the guard says the following ...
		string text = "Don't move from that spot.";
		string text2 = "If you do, you will regret it. I promise you that.";
		
		if (!guardTextStarted_)
		{
			manager.Enable(false);
			BackgroundMusic.SetVolume(0.5f);
			guardTextStarted_ = true;
			Color red = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			DialogField.Display(text, red, 0.0f, 2.0f);	
			DialogField.Display(text2, red, 2.5f, 4.5f);	
		}
		
		// switch to patrolling state
		if (time_ >= 24.7f)
		{
			manager.Enable(true);
			BackgroundMusic.SetVolume(1.0f);
			Patrolling.isPatrolling = true;
			updateHandler_ = guardPatrolling;
		}
	}
	//-------------------------------------------------------------------------
	private void guardPatrolling()
	{
		Vector2 posGuard = manager.GetPosition(6);
		
		// if the guard is visible
		if (posGuard.x < 8.0f && posGuard.x > -3.0f)
		{
			// and chocolate is not in the inventory
			if (!manager.IsInInventory(3) && manager.IsActive(3))
			{
				Application.LoadLevel(4);
			}
			
			// and rat bones is not in the inventory
			if (!manager.IsInInventory(1) && manager.IsActive(1))
			{
				Application.LoadLevel(4);
			}			
		}
		
		
		// stop the rat animations when it arrived at its destination
		if (isRatKillable && !RatMove.IsMoving())
		{
			RatAnimation.Stop();
		}
		
	}
	//-------------------------------------------------------------------------
}
