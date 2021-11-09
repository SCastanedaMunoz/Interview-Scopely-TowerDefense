// GENERATED AUTOMATICALLY FROM 'Assets/TowerDefense/Scripts/Input/CameraControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CameraControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CameraControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CameraControls"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""c9b9d6bd-d024-49d4-aa70-def69c2e4bcd"",
            ""actions"": [
                {
                    ""name"": ""PanMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""20efa1d0-1296-41cf-8122-a19fb96d2c98"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PanEnabled"",
                    ""type"": ""Button"",
                    ""id"": ""e89dd109-ddf1-47ad-b4b9-0d085cf98dca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reset"",
                    ""type"": ""Button"",
                    ""id"": ""afeec72d-1ec6-4e55-ad98-95815bc68264"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5cdd82f9-c12b-4ac4-9c00-55999aaa0129"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PanMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0d91e0b-31a1-4adc-8025-7e801cfd3e20"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PanEnabled"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed784005-1ee7-42e5-9abc-bb49deaac455"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_PanMovement = m_Camera.FindAction("PanMovement", throwIfNotFound: true);
        m_Camera_PanEnabled = m_Camera.FindAction("PanEnabled", throwIfNotFound: true);
        m_Camera_Reset = m_Camera.FindAction("Reset", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_PanMovement;
    private readonly InputAction m_Camera_PanEnabled;
    private readonly InputAction m_Camera_Reset;
    public struct CameraActions
    {
        private @CameraControls m_Wrapper;
        public CameraActions(@CameraControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PanMovement => m_Wrapper.m_Camera_PanMovement;
        public InputAction @PanEnabled => m_Wrapper.m_Camera_PanEnabled;
        public InputAction @Reset => m_Wrapper.m_Camera_Reset;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @PanMovement.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnPanMovement;
                @PanMovement.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnPanMovement;
                @PanMovement.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnPanMovement;
                @PanEnabled.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnPanEnabled;
                @PanEnabled.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnPanEnabled;
                @PanEnabled.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnPanEnabled;
                @Reset.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnReset;
                @Reset.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnReset;
                @Reset.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnReset;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PanMovement.started += instance.OnPanMovement;
                @PanMovement.performed += instance.OnPanMovement;
                @PanMovement.canceled += instance.OnPanMovement;
                @PanEnabled.started += instance.OnPanEnabled;
                @PanEnabled.performed += instance.OnPanEnabled;
                @PanEnabled.canceled += instance.OnPanEnabled;
                @Reset.started += instance.OnReset;
                @Reset.performed += instance.OnReset;
                @Reset.canceled += instance.OnReset;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);
    public interface ICameraActions
    {
        void OnPanMovement(InputAction.CallbackContext context);
        void OnPanEnabled(InputAction.CallbackContext context);
        void OnReset(InputAction.CallbackContext context);
    }
}
