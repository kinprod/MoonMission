using GameInputActions;
using Interaction;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Player
{
    public class Harpoon : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;

        public delegate void CollisionWithObject(GameObject targgetObject);
        public event CollisionWithObject Collision;

        public event Action HartoonReset;

        private Transform _startTransform;
        private SpriteRenderer _spriteRenderer;
        private float _maxDistance = 10f;
        private float _speed = 0.1f;

        [SerializeField]
        private LineRenderer _line;

        [SerializeField]
        private Transform _harpoonSource;
        [SerializeField]
        private Transform _playerTransform;


        private bool _inShot = false;

        public void Init(Transform startTransform, SpriteRenderer spriteRenderer, float maxDistance, float speed)
        {
            _startTransform = startTransform;
            _spriteRenderer = spriteRenderer;
            _maxDistance = maxDistance;
            _speed = speed;

            _line.positionCount = 2;
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            _inShot = false;
            if (collision.collider.tag == "InteractiveObject")
            {
                Collision?.Invoke(collision.gameObject);
            }
            else
            {
                _spriteRenderer.enabled = false;
                transform.position = _startTransform.position;
            }
        }


        private void FixedUpdate()
        {
            if (_inShot)
            {
                DrawLine();
                transform.Translate(Vector3.up * _speed);

                if ((transform.position - _startTransform.position).magnitude > _maxDistance)
                {
                    _inShot = false;
                    _spriteRenderer.enabled = false;
                    transform.position = _startTransform.position;
                    HartoonReset?.Invoke();
                }

            }
        }

        public void DrawLine()
        {
            Debug.Log("Draw");
            _line.SetPosition(0, _playerTransform.position);
            _line.SetPosition(1, _harpoonSource.position);
        }

        public void DontDrawLine()
        {
            _line.SetPosition(0, _playerTransform.position);
            _line.SetPosition(1, _playerTransform.position);
        }

        public void Shot()
        {
            _inShot = true;
            _audioSource.Play();
            ShotDirection();
        }



        private void ShotDirection()
        {
            var mousePosition = Mouse.current.position.ReadValue();
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            var mouseWorldPosition2D = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

            var direction = (mouseWorldPosition2D - new Vector2(transform.position.x, transform.position.y)).normalized;

            var angel = Vector2.Angle(direction, Vector2.up);
            angel = direction.x < 0 ? angel : -angel;

            transform.rotation = Quaternion.Euler(0, 0, angel);
        }
    }
}
