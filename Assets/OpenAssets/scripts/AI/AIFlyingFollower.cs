using UnityEngine;
using System.Collections;

public class AIFlyingFollower : MonoBehaviour {
	public float Speed = 3.0f;
	public float TrembleMaxDegrees = 0.0f;
	public float DirChangeInterval = 0.3f;

	protected MaskSensor _maskSensor;
	protected CorgiController _controller;
	protected Transform _playerTransform;
	protected Vector3 _direction;

	protected float _lastDirChange;


	protected virtual void Start()
	{
		// getting the mask sensor
		_maskSensor = GetComponent<MaskSensor>();
		// we get the CorgiController2D component
		_controller = GetComponent<CorgiController>();
		// we get the player from its tag
		_playerTransform = GameManager.Instance.Player.transform;
		if (_playerTransform == null || _controller == null || _maskSensor == null) 
		{
			Debug.LogError ("Destroying enemy object " + gameObject.name + " since its variables are not initialized.");
			GameObject.Destroy (this);
		}

		ChangeDirection();
	}


	// Update is called once per frame
	void Update () 
	{
		if (Vector3.SqrMagnitude (_playerTransform.position - transform.position) < 200.0f) 
		{
			ChangeDirection ();
			_controller.SetHorizontalForce (_direction.x * Speed);
			_controller.SetVerticalForce (_direction.y * Speed);
		} 
		else 
		{
			_controller.SetHorizontalForce (0.0f);
			_controller.SetVerticalForce (0.0f);
		}
	}

	protected void ChangeDirection()
	{
		if(_lastDirChange + DirChangeInterval < Time.time)
		{
			_direction = _playerTransform.position - transform.position;
			_direction = Vector3.Normalize (_direction);
			_direction = Quaternion.Euler (0, 0, Random.value * TrembleMaxDegrees) *  _direction;
			_lastDirChange = Time.time;

			if (_maskSensor.Scared) 
			{
				_direction *= -1;
			}

			// Fixing animation direction
			float signal = Mathf.Sign (_direction.x);
			float absLocalScaleX = -1 * Mathf.Abs (transform.localScale.x);
			transform.localScale = new Vector3(signal * absLocalScaleX, transform.localScale.y, transform.localScale.z);
		}
	}

}
