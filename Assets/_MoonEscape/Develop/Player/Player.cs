using Cysharp.Threading.Tasks;
using GameInputActions;
using Interaction;
using System;
using UnityEngine;
using VContainer;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private InputManager _inputManager;

        [Header("Harpoon")]
        [SerializeField]
        private Harpoon _harpoon;
        [SerializeField]
        private Transform _harpoonTransform;
        [SerializeField]
        private Transform _harpoonStartPosition;
        [SerializeField]
        private SpriteRenderer _harpoonSprite;
        [SerializeField]
        private float _maxHarpoonDistance = 15f;

        [Space]
        [Header("Components")]
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [Space]
        [Header("Parametrs")]
        [SerializeField]
        private float _harpoonSpeed = 2f;
        [SerializeField]
        private float _attractBoost = 0.5f;

        private GameObject _targgetObject;

        private bool _isShot = false;
        private bool _isAttractPlayer = false;
        private bool _isAttractObject = false;

        private UniTask _playerBoostTask = UniTask.CompletedTask;
        private UniTask _objectBoostTask = UniTask.CompletedTask;

        private InteractiveObject _interactiveObject;
        private Rigidbody2D _interactiveObjectRigidbody;


        [Inject]
        private void Init(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        private void Awake()
        {
            _harpoon.Init(_harpoonStartPosition, _harpoonSprite, _maxHarpoonDistance, _harpoonSpeed);
            HarpoonReset();
        }

        private void OnEnable()
        {
            _inputManager.ShotPerformedAction += ShotPerformed;
            _inputManager.ShotCanceledAction += ShotCanceled;
            _inputManager.AttractPerformedAction += AttractPerformed;
            _inputManager.AttractCanceledAction += AttractCanceled;
            _harpoon.Collision += HarpoonCollision;
            _harpoon.HartoonReset += HarpoonReset;
        }

        private void OnDisable()
        {
            _inputManager.ShotPerformedAction -= ShotPerformed;
            _inputManager.ShotCanceledAction -= ShotCanceled;
            _inputManager.AttractPerformedAction -= AttractPerformed;
            _inputManager.AttractCanceledAction -= AttractCanceled;
            _harpoon.Collision -= HarpoonCollision;
            _harpoon.HartoonReset -= HarpoonReset;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.Equals(_targgetObject))
            {
                _isAttractPlayer = false;
                _rigidbody.linearDamping = 100;
                _rigidbody.angularDamping = 100;
            }
        }

        private void FixedUpdate()
        {
            if (_isAttractPlayer)
            {
                PlayerBoostInMove();
            }
            else if (_isAttractObject)
            {
                ObjectBoostInMove();
            }
        }


        private void ObjectBoostInMove()
        {
            if (_objectBoostTask.Status == UniTaskStatus.Succeeded)
            {
                _objectBoostTask = ObjectBoost();
            }
        }


        private async UniTask ObjectBoost()
        {
            var direciton = (transform.position - _targgetObject.transform.position).normalized;
            var direction2D = new Vector2(direciton.x, direciton.y).normalized;

            _interactiveObjectRigidbody.linearVelocity = (1 + _attractBoost) * direction2D;

            await UniTask.Delay(1000);
        }


        private void PlayerBoostInMove()
        {
            if (_playerBoostTask.Status == UniTaskStatus.Succeeded)
            {
                _playerBoostTask = PlayerBoost();
            }
        }


        private async UniTask PlayerBoost()
        {
            var direciton = (_targgetObject.transform.position - transform.position).normalized;
            var direction2D = new Vector2(direciton.x, direciton.y).normalized;

            _rigidbody.linearVelocity = (1 + _attractBoost) * direction2D;

            await UniTask.Delay(1000);
        }

        private void ShotPerformed()
        {
            if (_targgetObject == null && !_isShot)
            {
                HarpoonReset();
                _isShot = true;
                _harpoon.Shot();
                _harpoonSprite.enabled = true;
            }
            else if (_targgetObject != null && !_isAttractObject)
            {
                AttractPlayerToObject();
            }
        }

        private void ShotCanceled()
        {
            if (_targgetObject != null)
            {
                HarpoonReset();
                _isAttractPlayer = false;
                _isAttractObject = false;
                _targgetObject = null;
            }
            _harpoon.transform.SetParent(null);
            _interactiveObjectRigidbody = null;
        }

        private void AttractPerformed()
        {
            _interactiveObject = _targgetObject?.GetComponent<InteractiveObject>();
            if (_interactiveObject == null)
            {
                return;
            }
            _harpoon.transform.SetParent(_interactiveObject.transform);
            _interactiveObjectRigidbody = _interactiveObject.GetComponent<Rigidbody2D>();
            AttractObjectToPlayer();
        }

        private void AttractCanceled()
        {
            if (_targgetObject != null)
            {
                HarpoonReset();
                _isAttractPlayer = false;
                _isAttractObject = false;
                _targgetObject = null;
            }
            _harpoon.transform.SetParent(null);
            _interactiveObjectRigidbody = null;
        }

        private void AttractPlayerToObject()
        {
            _isAttractPlayer = true;
            _rigidbody.linearDamping = 0;
            _rigidbody.angularDamping = 0;
        }

        private void AttractObjectToPlayer()
        {
            _isAttractObject = true;
            _rigidbody.linearDamping = 0;
            _rigidbody.angularDamping = 0;
        }

        private void HarpoonReset()
        {
            _isShot = false;
            _harpoonSprite.enabled = false;
            _harpoonTransform.position = _harpoonStartPosition.position;
        }

        private void HarpoonCollision(GameObject targgetObject)
        {
            Debug.Log("Harpoon collision");
            _targgetObject = targgetObject;
            _isShot = false;
        }
    }
}