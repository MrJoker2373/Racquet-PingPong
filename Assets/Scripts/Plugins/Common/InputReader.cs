namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class InputReader : MonoBehaviour
    {
        [SerializeField]
        private InputActionAsset _controls;

        private List<(Action<InputAction.CallbackContext>, object)> _callbacks;

        public static InputReader Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _callbacks = new();
            }
        }

        public void Subscribe<T>(InputID key, Action<T> callback) where T : struct
        {
            var action = _controls.FindAction(key.ToString());
            action.Enable();

            Action<InputAction.CallbackContext> handler = (ctx) => callback?.Invoke(ctx.ReadValue<T>());
            _callbacks.Add((handler, callback.Target));
            action.performed += handler;
        }

        public void Unsubscribe<T>(InputID key, Action<T> callback, bool disable = false)
        {
            if (_callbacks.Any(c => c.Item2 == callback.Target) == false)
                return;

            var action = _controls.FindAction(key.ToString());
            if (disable == true)
                action.Disable();

            var handler = _callbacks.First(c => c.Item2 == callback.Target);
            action.performed -= handler.Item1;
            _callbacks.Remove(handler);
        }
    }
}