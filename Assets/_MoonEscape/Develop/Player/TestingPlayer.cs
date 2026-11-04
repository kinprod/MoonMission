using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class TestingPlayer : MonoBehaviour
{
    [SerializeField]
    private Harpoon _harpoon;
    
    [SerializeField]
    private float _attractSpeed = 1.0f;

    private bool _isAtract = false;

    private Transform _targetTransform;


    private void OnEnable()
    {
        _harpoon.PositionObserver += Attract;
    }


    private void OnDisable()
    {
        _harpoon.PositionObserver -= Attract;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isAtract = false;
    }


    private void FixedUpdate()
    {
        if (_isAtract)
        {
            Move();
        }
    }


    private void Move()
    {
        transform.position += (_targetTransform.position - transform.position).normalized * _attractSpeed;
    }


    private void Attract(Transform targetTransfrom)
    {
        _isAtract = true;
        _targetTransform = targetTransfrom;
    }
}
