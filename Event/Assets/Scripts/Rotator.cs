using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	private int _rotationSpeed = 90;
	private Transform _transform;

	void Start()
	{
		_transform = transform;
	}
	void Update()
	{
		_transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0, Space.World);
	}
}
