﻿using FlappyBean.Runtime.Model.Input;
using FlappyBean.Runtime.Model.Movement.Player.Rotation;
using FlappyBean.Runtime.Root.SystemUpdates;
using FlappyBean.Runtime.View.Health;
using FlappyBean.Runtime.View.Movement.Player;
using FlappyBean.Runtime.View.Movement.Player.Rotation;
using UnityEngine;

namespace FlappyBean.Runtime.Root.Player
{
	[RequireComponent(typeof(Collider2D))]
	public class PlayerRoot : CompositeRoot
	{
		[SerializeField] private int _healthValue;
		[SerializeField] private IHealthTransformView _healthTransformView;
		[Space]
		[SerializeField] private Vector2 _jumpDirection;
		[SerializeField] private IPlayerJumpView _jumpView;
		[Space]
		[SerializeField] private int _maxRotation;
		[SerializeField] private int _minRotation;
		[SerializeField] private int _rotationSpeed;
		[SerializeField] private IPlayerRotationView _rotationView;
		[Space]
		[SerializeField] private IInput _input;

		private ISystemUpdate _systemUpdate;

		public override void Compose()
		{
			_systemUpdate = new SystemUpdate();

			_healthTransformView.Init(new Model.Health.Health(_healthValue));

			var jump = new Model.Movement.Player.Jump.PlayerJump(_jumpView, _input, _jumpDirection);

			var rotationData = new PlayerRotationData(_maxRotation, _minRotation, _rotationSpeed);
			var rotation = new PlayerRotation(_input, _rotationView, rotationData);

			_systemUpdate.Add(jump, rotation);
		}

		private void Update() => _systemUpdate?.UpdateAll();
	}
}
