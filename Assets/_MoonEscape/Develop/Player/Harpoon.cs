using DI;
using GameInputActions;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

public class Harpoon : MonoBehaviour
{
    [SerializeField]
    private LifetimeScope _container;

    public delegate void HarpoonPositionObserver(Transform transform);
    public event HarpoonPositionObserver PositionObserver;

    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private Transform _hand;

    [SerializeField]
    private float _speed = 0.3f;

    private InputManager _inputManager;

    private GameObject _interactionObject = null;

    private Vector2 _direction;

    private bool _inShot = false;
    private bool _inAttract = false;



    private void Awake()
    {
        ResetHarpoonParametrs();
        _inputManager = _container.Container.Resolve<InputManager>();
    }


    private void OnEnable()
    {
        _inputManager.ShotPerformedAction += ShotOrAttract;
        _inputManager.ShotCanceledAction += ResetHarpoon;
    }


    private void OnDisable()
    {
        _inputManager.ShotPerformedAction -= ShotOrAttract;
        _inputManager.ShotCanceledAction -= ResetHarpoon;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        _inAttract = false;
        if (collision.gameObject.tag == "InteractiveObject")
        {
            _interactionObject = collision.gameObject;
        }
        else
        {
            ResetHarpoonParametrs();
        }
        _inShot = false;
    }


    private void FixedUpdate()
    {
        if (_inShot)
        {
            MoveHarpoon();
        }
    }


    private void MoveHarpoon()
    {
        _rigidbody.position += _direction * _speed;
        Debug.DrawRay(_hand.position, _direction * 100, Color.green);
    }


    private void ShotOrAttract()
    {
        if (_interactionObject == null)
        {
            ShotDirection();
        }
        else
        {
            PositionObserver?.Invoke(_rigidbody.transform);
            _inAttract = true;
        }
    }


    private void ShotDirection()
    {
        _inShot = true;

        var mousePosition = Mouse.current.position.ReadValue();
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var mouseWorldPosition2D = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

        _direction = (mouseWorldPosition2D - new Vector2(_hand.position.x, _hand.position.y)).normalized;
    }


    private void ResetHarpoonParametrs()
    {
        _interactionObject = null;
        transform.position = _hand.position;
    }


    private void ResetHarpoon()
    {
        if (_interactionObject != null)
        {
            ResetHarpoonParametrs();
        }
    }
}
