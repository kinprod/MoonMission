using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using VContainer;

namespace GameInputActions
{
    public class InputManager : MonoBehaviour
    {
        public event Action ShotPerformedAction;
        public event Action ShotCanceledAction;
        public event Action AttractPerformedAction;
        public event Action AttractCanceledAction;

        private GameActions _inputActions;

        private InputAction _shot;
        private InputAction _attract;


        private void OnEnable()
        {
            _inputActions = new GameActions();
            _inputActions.Enable();

            _shot = _inputActions.Gameplay.Shot;
            _attract = _inputActions.Gameplay.Attract;

            _shot.performed += ShotPerformed;
            _shot.canceled += ShotCanceled;
            _attract.performed += AttractPerformed;
            _attract.canceled += AttractCanceled;
        }


        private void OnDisable()
        {
            _shot.performed -= ShotPerformed;
            _shot.canceled -= ShotCanceled;
            _attract.performed -= AttractPerformed;
            _attract.canceled -= AttractCanceled;
        }

        private void ShotPerformed(InputAction.CallbackContext context)
        {
            ShotPerformedAction?.Invoke();
        }


        private void ShotCanceled(InputAction.CallbackContext context)
        {
            ShotCanceledAction?.Invoke();
        }


        private void AttractPerformed(InputAction.CallbackContext context)
        {
            AttractPerformedAction?.Invoke();
        }


        private void AttractCanceled(InputAction.CallbackContext context)
        {
            AttractCanceledAction?.Invoke();
        }

    }
}