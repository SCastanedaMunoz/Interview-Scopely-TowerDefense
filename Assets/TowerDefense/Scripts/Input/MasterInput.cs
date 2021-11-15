// GENERATED AUTOMATICALLY FROM 'Assets/TowerDefense/Scripts/Input/MasterInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MasterInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MasterInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MasterInput"",
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
        },
        {
            ""name"": ""TurretPlacer"",
            ""id"": ""cb034907-4e65-4163-98f4-e08dab13c577"",
            ""actions"": [
                {
                    ""name"": ""PlacerPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""17a3b205-ea68-4dcc-b725-f49e508ba339"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PreviewTurret"",
                    ""type"": ""Button"",
                    ""id"": ""a2e42f9f-0133-44de-8a8f-8f6afc119dfb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0caf8c84-3236-4335-997f-6c563f343c04"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlacerPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c39333a-430a-4738-a3b2-ef4129f1122e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviewTurret"",
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
        // TurretPlacer
        m_TurretPlacer = asset.FindActionMap("TurretPlacer", throwIfNotFound: true);
        m_TurretPlacer_PlacerPosition = m_TurretPlacer.FindAction("PlacerPosition", throwIfNotFound: true);
        m_TurretPlacer_PreviewTurret = m_TurretPlacer.FindAction("PreviewTurret", throwIfNotFound: true);
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
        private @MasterInput m_Wrapper;
        public CameraActions(@MasterInput wrapper) { m_Wrapper = wrapper; }
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

    // TurretPlacer
    private readonly InputActionMap m_TurretPlacer;
    private ITurretPlacerActions m_TurretPlacerActionsCallbackInterface;
    private readonly InputAction m_TurretPlacer_PlacerPosition;
    private readonly InputAction m_TurretPlacer_PreviewTurret;
    public struct TurretPlacerActions
    {
        private @MasterInput m_Wrapper;
        public TurretPlacerActions(@MasterInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlacerPosition => m_Wrapper.m_TurretPlacer_PlacerPosition;
        public InputAction @PreviewTurret => m_Wrapper.m_TurretPlacer_PreviewTurret;
        public InputActionMap Get() { return m_Wrapper.m_TurretPlacer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TurretPlacerActions set) { return set.Get(); }
        public void SetCallbacks(ITurretPlacerActions instance)
        {
            if (m_Wrapper.m_TurretPlacerActionsCallbackInterface != null)
            {
                @PlacerPosition.started -= m_Wrapper.m_TurretPlacerActionsCallbackInterface.OnPlacerPosition;
                @PlacerPosition.performed -= m_Wrapper.m_TurretPlacerActionsCallbackInterface.OnPlacerPosition;
                @PlacerPosition.canceled -= m_Wrapper.m_TurretPlacerActionsCallbackInterface.OnPlacerPosition;
                @PreviewTurret.started -= m_Wrapper.m_TurretPlacerActionsCallbackInterface.OnPreviewTurret;
                @PreviewTurret.performed -= m_Wrapper.m_TurretPlacerActionsCallbackInterface.OnPreviewTurret;
                @PreviewTurret.canceled -= m_Wrapper.m_TurretPlacerActionsCallbackInterface.OnPreviewTurret;
            }
            m_Wrapper.m_TurretPlacerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlacerPosition.started += instance.OnPlacerPosition;
                @PlacerPosition.performed += instance.OnPlacerPosition;
                @PlacerPosition.canceled += instance.OnPlacerPosition;
                @PreviewTurret.started += instance.OnPreviewTurret;
                @PreviewTurret.performed += instance.OnPreviewTurret;
                @PreviewTurret.canceled += instance.OnPreviewTurret;
            }
        }
    }
    public TurretPlacerActions @TurretPlacer => new TurretPlacerActions(this);
    public interface ICameraActions
    {
        void OnPanMovement(InputAction.CallbackContext context);
        void OnPanEnabled(InputAction.CallbackContext context);
        void OnReset(InputAction.CallbackContext context);
    }
    public interface ITurretPlacerActions
    {
        void OnPlacerPosition(InputAction.CallbackContext context);
        void OnPreviewTurret(InputAction.CallbackContext context);
    }
}
