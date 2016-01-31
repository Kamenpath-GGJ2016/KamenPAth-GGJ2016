using UnityEngine;
using System.Collections;
/// <summary>
/// Add this component to a CorgiController2D and it will try to kill your player on sight.
/// </summary>
public class AIGroundFollower : MonoBehaviour,IPlayerRespawnListener
{
	/// The speed of the agent
	public float Speed;
	/// The initial direction
	public bool GoesRightInitially=true;
	/// If set to true, the agent will try and avoid falling
	public bool AvoidFalling = false;

	public bool playerOnRight;

	// private stuff
	protected CorgiController _controller;
	public Vector2 _direction;
	protected Vector2 _startPosition;
	protected Vector2 _initialDirection;
	protected Vector3 _initialScale;
	protected Vector3 _holeDetectionOffset;
	protected float _holeDetectionRayLength = 1.5f;
	protected Transform _playerTransform;
	protected MaskSensor _maskSensor;

	protected float LastFlipToPlayer;
	protected float FlipToPlayerInterval = 0.5f;

	protected virtual void Start()
	{
		// getting the mask sensor
		_maskSensor = GetComponent<MaskSensor>();
		// we get the player from its tag
		_playerTransform = GameManager.Instance.Player.transform;
		if (_playerTransform == null || _maskSensor == null) 
		{
			Debug.LogError ("Destroying enemy object " + gameObject.name + " since the player variable is not initialized.");
			GameObject.Destroy (this);
		}
		LastFlipToPlayer = Time.time;
	}

	/// <summary>
	/// Initialization
	/// </summary>
	protected virtual void Awake()
	{
		// we get the CorgiController2D component
		_controller = GetComponent<CorgiController>();
		// initialize the start position
		_startPosition = transform.position;
		// initialize the direction
		_direction = GoesRightInitially ? Vector2.right : -Vector2.right;
		_initialDirection = _direction;
		_initialScale = transform.localScale;
		_holeDetectionOffset = new Vector3(1, -1f, 0);
	}

	/// <summary>
	/// Every frame, moves the agent and checks if it can shoot at the player.
	/// </summary>
	protected virtual void Update () 
	{	
		playerOnRight = transform.position.x < _playerTransform.position.x;

		if (LastFlipToPlayer + FlipToPlayerInterval < Time.time) 
		{
			int reverseMovement = _maskSensor.Scared ? -1 : 1;
			if (playerOnRight && Mathf.Sign (reverseMovement*_direction.x) < 0) 
			{
				ChangeDirection (reverseMovement*Vector2.right);
				LastFlipToPlayer = Time.time;
			} 
			else if (!playerOnRight && Mathf.Sign (reverseMovement*_direction.x) >= 0) 
			{
				ChangeDirection (-reverseMovement * Vector2.right);
				LastFlipToPlayer = Time.time;
			}
		}

		CheckForHoles ();

		// moves the agent in its current direction
		_controller.SetHorizontalForce(_direction.x * Speed);
	}

	/// <summary>
	/// Checks for a wall and changes direction if it meets one
	/// </summary>
	protected virtual void CheckForWalls()
	{
		// if the agent is colliding with something, make it turn around
		if ((_direction.x < 0 && _controller.State.IsCollidingLeft) || (_direction.x > 0 && _controller.State.IsCollidingRight))
		{
			ChangeDirection(-1*_direction);
		}
	}

	/// <summary>
	/// Checks for holes 
	/// </summary>
	protected virtual void CheckForHoles()
	{
		Vector2 raycastOrigin = new Vector2(transform.position.x+_direction.x*_holeDetectionOffset.x, transform.position.y+ _holeDetectionOffset.y - (transform.localScale.y / 2));
		RaycastHit2D raycast = CorgiTools.CorgiRayCast(raycastOrigin, Vector2.down, _holeDetectionRayLength, 1 << LayerMask.NameToLayer("Platforms"), true, Color.yellow);
		// if the raycast doesn't hit anything
		if (!raycast)
		{
			// we change direction
			ChangeDirection(-1*_direction);
			LastFlipToPlayer = Time.time;
		}
	}

	/// <summary>
	/// Changes the agent's direction and flips its transform
	/// </summary>
	protected virtual void ChangeDirection(Vector2 newDirection)
	{
		_direction = newDirection;
		float signal = Mathf.Sign (newDirection.x);
		float absLocalScaleX = -1 * Mathf.Abs (transform.localScale.x);
		transform.localScale = new Vector3(signal * absLocalScaleX, transform.localScale.y, transform.localScale.z);
	}

	/// <summary>
	/// When the player respawns, we reinstate this agent.
	/// </summary>
	/// <param name="checkpoint">Checkpoint.</param>
	/// <param name="player">Player.</param>
	public virtual void onPlayerRespawnInThisCheckpoint (CheckPoint checkpoint, CharacterBehavior player)
	{
		_direction = _initialDirection;
		transform.localScale= _initialScale;
		transform.position=_startPosition;
		gameObject.SetActive(true);
	}

}
