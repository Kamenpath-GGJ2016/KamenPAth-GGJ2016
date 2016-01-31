using UnityEngine;
using System.Collections;

public class SnowCloud : MonoBehaviour {
	protected Transform _playerTransform;
	public float yDelta = 10.0f;
	public float xDelta = 0.0f;

	// Use this for initialization
	void Start () {
		// we get the player from its tag
		_playerTransform = GameManager.Instance.Player.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3 ( _playerTransform.position.x + xDelta, 
			_playerTransform.position.y + yDelta,
			0.0f);
	}
}
