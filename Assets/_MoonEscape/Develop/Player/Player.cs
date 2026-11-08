using System;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public event Action Collision;

        [SerializeField]
        private Harpoon _harpoon;
        [SerializeField]
        private float _atractSpeed = 0.1f;

        private bool _isAtract = false;
        private Transform _targetObject;


        private void OnEnable()
        {
            _harpoon.AtractPlayerToObject += AtractPlayer;
        }


        private void OnDisable()
        {
            _harpoon.AtractPlayerToObject -= AtractPlayer;
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform == _targetObject)
            {
                Collision?.Invoke();
                _isAtract = false;
            }
        }


        private void FixedUpdate()
        {
            if (_isAtract)
            {
                transform.position += (_targetObject.position - transform.position).normalized * _atractSpeed;
            }
        }


        private void AtractPlayer(Transform targetObject)
        {
            if (_targetObject != targetObject)
            {
                _targetObject = targetObject;
                _isAtract = true;
            }
            else
            {
                Collision?.Invoke();
            }
        }
    }
}