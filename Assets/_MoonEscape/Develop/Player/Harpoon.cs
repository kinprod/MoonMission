using DI;
using GameInputActions;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class Harpoon : MonoBehaviour
{
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


    [Inject]
    private void Init(InputManager inputManager)
    {
        _inputManager = inputManager;
        Debug.Log("Harpoon inject");
    }


    private void Awake()
    {
        ResetHarpoonParametrs();
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
        if (collision.gameObject.tag == "InteractionObject")
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
        gameObject.SetActive(false);
        gameObject.transform.position = _hand.position;
        gameObject.SetActive(true);
    }


    private void ResetHarpoon()
    {
        if (_interactionObject != null)
        {
            ResetHarpoonParametrs();
        }
    }
}
