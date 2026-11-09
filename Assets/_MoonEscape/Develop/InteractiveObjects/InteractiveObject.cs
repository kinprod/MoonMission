using Cysharp.Threading.Tasks;
using GameInputActions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Interaction
{
    enum Interaction
    {
        Reflect,
        SumDirection
    }

    public class InteractiveObject : MonoBehaviour
    {
        public Vector2 Direction { get { return _baseDirection; } }

        private static float _stopСoefficient = 0.5f;

        [SerializeField]
        private Interaction _interactionType = Interaction.Reflect;

        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private float _flySpeed = 0.1f;

        private IInteractive _interaction;

        private Vector2 _baseDirection;
        private float _baseSpeed;

        private UniTask _stopTask;



        private void Awake()
        {
            if (_interactionType == Interaction.Reflect)
            {
                _interaction = new ReflectInteraction();
            }
            else if (_interactionType == Interaction.SumDirection)
            {
                _interaction = new SumDirectionInteraction();
            }

            _baseSpeed = _flySpeed;
        }



        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag != "Harpoon")
            {
                _flySpeed *= _stopСoefficient;
                _rigidbody.linearVelocity = _interaction.InteractDirection(collision, _baseDirection).normalized * _flySpeed;
                _baseDirection = _rigidbody.linearVelocity;


                if (_flySpeed < 0.005)
                {
                    _rigidbody.linearVelocity = Vector2.zero;
                    _baseDirection = Vector2.zero;
                }
            }
        }


        private void FixedUpdate()
        {
            if (_flySpeed > 0.05f && _rigidbody.linearVelocity != Vector2.zero)
            {
                StopInMove();
            }
        }


        private void StopInMove()
        {
            if (_stopTask.Status == UniTaskStatus.Succeeded)
            {
                _stopTask = Stopping();
            }
        }


        private async UniTask Stopping()
        {
            await UniTask.Delay(1000);

            _rigidbody.linearVelocity *= 0.9f;
            _flySpeed = 0.9f;

            if (_flySpeed < 0.05)
            {
                _rigidbody.linearVelocity = Vector2.zero;
                _baseDirection = Vector2.zero;
            }
        }

    }
}