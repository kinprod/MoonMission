using GameInputActions;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Player
{
    public class Harpoon : MonoBehaviour
    {
        public delegate void AtractPlayer(Transform targgetObject);
        public event AtractPlayer AtractPlayerToObject;

        private InputManager _inputManager;

        [SerializeField]
        private Player _player;
        [SerializeField]
        private Transform _harpoonSprite;
        [SerializeField]
        private Transform _startPosition;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private float _maxDistance = 10f;
        [SerializeField]
        private float _speed = 0.1f;

        private Vector2 _direction;

        private Transform _interactObject = null;

        private bool _inShot = false;


        [Inject]
        private void Init(InputManager inputManager)
        {
            _inputManager = inputManager;
        }


        private void Awake()
        {
            _spriteRenderer.enabled = false;
        }


        private void OnEnable()
        {
            _inputManager.ShotPerformedAction += ShotOrAttract;
            _player.Collision += PlayerIsCollision;
        }


        private void OnDisable()
        {
            _inputManager.ShotPerformedAction -= ShotOrAttract;
            _player.Collision -= PlayerIsCollision;
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            _inShot = false;
            if (collision.collider.tag == "InteractiveObject")
            {
                _interactObject = collision.transform;
            }
            else
            {
                _spriteRenderer.enabled = false;
                transform.position = _startPosition.position;
            }
        }


        private void FixedUpdate()
        {
            if (_inShot)
            {
                transform.Translate(Vector3.up * _speed);

                if ((transform.position - _startPosition.position).magnitude > _maxDistance)
                {
                    _inShot = false;
                    _spriteRenderer.enabled = false;
                }
            }
        }


        private void ShotOrAttract()
        {
            if (!_inShot && _interactObject == null)
            {
                _inShot = true;
                _spriteRenderer.enabled = true;
                ShotDirection();
            }
            else if (_interactObject != null)
            {
                AtractPlayerToObject?.Invoke(_interactObject);
            }
        }


        private void ShotDirection()
        {
            transform.position = _startPosition.position;

            var mousePosition = Mouse.current.position.ReadValue();
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            var mouseWorldPosition2D = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

            _direction = (mouseWorldPosition2D - new Vector2(_startPosition.position.x, _startPosition.position.y)).normalized;

            var angel = Vector2.Angle(_direction, Vector2.up);
            angel = _direction.x < 0 ? angel : - angel;

            transform.rotation = Quaternion.Euler(0, 0, angel);
        }


        private void PlayerIsCollision()
        {
            _interactObject = null;
        }
    }
}
