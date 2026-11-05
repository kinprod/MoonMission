using GameInputActions;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Interaction
{
    public class InteractiveObject : MonoBehaviour
    {
        private InputManager _inputManager;

        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private float _flySpeed = 0.1f;

        private bool _canMove = false;

        private IInteractive _interaction;

        private Vector2 _baseDirection;


        [Inject]
        private void Init(InputManager inputManager)
        {
            _inputManager = inputManager;
            _interaction = new ReflectInteraction();
        }


        private void OnEnable()
        {
            _inputManager.ShotPerformedAction += Move;
        }


        private void OnDisable()
        {
            _inputManager.ShotPerformedAction -= Move;
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            _baseDirection = _interaction.InteractDirection(collision, _baseDirection);
            _flySpeed *= 0.8f;
            _rigidbody.AddForce(_baseDirection * _flySpeed, ForceMode2D.Impulse);

            if (_flySpeed < 0.5)
            {
                _rigidbody.linearVelocity = Vector2.zero;
                _flySpeed = 0f;
            }
        }


        private void Move()
        {
            var mousePosition = Mouse.current.position.ReadValue();
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            var mouseWorldPosition2D = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

            _rigidbody.AddForce((mouseWorldPosition2D - _rigidbody.position).normalized * _flySpeed, ForceMode2D.Impulse);
            _baseDirection = (mouseWorldPosition2D - _rigidbody.position).normalized;
        }
    }
}