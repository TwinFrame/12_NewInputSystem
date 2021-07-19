using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _rotateSpeed;

	private PlayerInput _input;

	private Vector2 _direction;
	private Vector2 _rotate;
	private Vector2 _rotation;

	private void Awake()
	{
		_input = new PlayerInput();
		_input.Enable();
	}

	private void Update()
	{
		_rotate = _input.Player.Look.ReadValue<Vector2>();
		_direction = _input.Player.Move.ReadValue<Vector2>();

		Move(_direction);
		Look(_rotate);
	}

	private void Move(Vector2 direction)
	{
		if (direction.sqrMagnitude < 0.1f)
			return;

		float scaleMoveSpeed = _moveSpeed * Time.deltaTime;

		Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);

		transform.position += move * scaleMoveSpeed;
	}

	private void Look(Vector2 rotate)
	{
		if (rotate.sqrMagnitude < 0.1f)
			return;

		float scaleRotateSpeed = _rotateSpeed * Time.deltaTime;

		_rotation.y += rotate.x * scaleRotateSpeed;
		_rotation.x = Mathf.Clamp(_rotation.x - rotate.y * scaleRotateSpeed, -90, 90);

		transform.localEulerAngles = _rotation;
	}
}
