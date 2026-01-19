using System;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Input
{
    public class InputReader: IDisposable
    {
        public event Action Click;
        private Inputs _inputs;
        private InputAction _positionAction;
        private InputAction _fireAction;
            
        public InputReader()
        {
            _inputs = new Inputs();
            _inputs.Player.Fire.performed += OnClick;
        }
            
        public void EnableInputs(bool value)
        {
            if(value)
                _inputs.Enable();
            else
                _inputs.Disable();
        }
            
        private void OnClick(InputAction.CallbackContext context)
        {
            Click?.Invoke();//вызывается если не нул
        }
            
        private bool _isFire;//кликнули или нет
        
        public void Dispose()//освобождает память, тк сборщик мусора может не сработать когда нужно
        {
            _inputs.Player.Fire.performed -= OnClick;
        }
        
        public Vector2 Position() => _inputs.Player.Select.ReadValue<Vector2>();
    }
}