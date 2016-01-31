using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;
/// <summary>
/// This persistent singleton handles the inputs and sends commands to the player
/// </summary>
public class MyInputManager : PersistentSingleton<MyInputManager>
{

	public bool InDialogueZone;
	protected static CharacterBehavior _player;
	protected static CorgiController _controller;

	/// <summary>
	/// We get the player from its tag.
	/// </summary>
	protected virtual void Start()
	{
		InDialogueZone = false;
		if (MyGameManager.Instance.Player!=null)
		{
			_player = MyGameManager.Instance.Player;
			if (MyGameManager.Instance.Player.GetComponent<CorgiController>() != null)	
			{
				_controller = MyGameManager.Instance.Player.GetComponent<CorgiController>();
			}
		}
	}

	/// <summary>
	/// At update, we check the various commands and send them to the player.
	/// </summary>
	protected virtual void Update()
	{		

		// if we can't get the player, we do nothing
		if (_player == null) 
		{
			if (MyGameManager.Instance.Player != null) {
				if (MyGameManager.Instance.Player.GetComponent<CharacterBehavior> () != null)
					_player = MyGameManager.Instance.Player;
				_controller = MyGameManager.Instance.Player.GetComponent<CorgiController> ();
			} else {
				Debug.Log ("A");
				return;
			}
		}

		if ( CrossPlatformInputManager.GetButtonDown("Pause") )
			MyGameManager.Instance.Pause();

		if (MyGameManager.Instance.Paused)
			return;	

		// if the player is in a dialogue zone, we handle it
		if ((_player.BehaviorState.InDialogueZone)
			&&(_player.BehaviorState.CurrentDialogueZone!=null)
			&&(!_player.BehaviorState.IsDead)
			&&(_controller.State.IsGrounded)
			&&(!_player.BehaviorState.Dashing))
		{
			if (CrossPlatformInputManager.GetButtonDown ("Jump")) 
			{
				_player.BehaviorState.CurrentDialogueZone.StartDialogue();
			}
		}

		// if the player can't move for some reason, we do nothing else
		if (!MyGameManager.Instance.CanMove)
			return;

		_player.SetHorizontalMove(CrossPlatformInputManager.GetAxis ("Horizontal"));
		_player.SetVerticalMove(CrossPlatformInputManager.GetAxis ("Vertical"));

		if ((CrossPlatformInputManager.GetButtonDown("Run")||CrossPlatformInputManager.GetButton("Run")) )
			_player.RunStart();		

		if (CrossPlatformInputManager.GetButtonUp("Run"))
			_player.RunStop();		


		if (CrossPlatformInputManager.GetButtonDown ("Jump")) 
		{
			if (!_player.BehaviorState.InDialogueZone)
			{
				_player.JumpStart ();
			}
		}

		if (CrossPlatformInputManager.GetButtonUp("Jump"))
		{
			_player.JumpStop();
		}

		if ( CrossPlatformInputManager.GetButtonDown("Dash") )
			_player.Dash();


		if (_player.GetComponent<CharacterMelee>() != null) 
		{		
			if ( CrossPlatformInputManager.GetButtonDown("Melee")  )
				_player.GetComponent<CharacterMelee>().Melee();
		}		

		if (_player.GetComponent<CharacterShoot>() != null) 
		{
			_player.GetComponent<CharacterShoot>().SetHorizontalMove(CrossPlatformInputManager.GetAxis ("Horizontal"));
			_player.GetComponent<CharacterShoot>().SetVerticalMove(CrossPlatformInputManager.GetAxis ("Vertical"));

			if (CrossPlatformInputManager.GetButtonDown("Fire"))
				_player.GetComponent<CharacterShoot>().ShootOnce();			

			if (CrossPlatformInputManager.GetButton("Fire")) 
				_player.GetComponent<CharacterShoot>().ShootStart();

			if (CrossPlatformInputManager.GetButtonUp("Fire"))
				_player.GetComponent<CharacterShoot>().ShootStop();

		}

		if (_player.GetComponent<CharacterJetpack>()!=null)
		{
			if ((CrossPlatformInputManager.GetButtonDown("Jetpack")||CrossPlatformInputManager.GetButton("Jetpack")) )
				_player.GetComponent<CharacterJetpack>().JetpackStart();

			if (CrossPlatformInputManager.GetButtonUp("Jetpack"))
				_player.GetComponent<CharacterJetpack>().JetpackStop();
		}
	}	
}
