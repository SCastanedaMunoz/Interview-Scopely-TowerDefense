using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerDefense
{
    [RequireComponent(typeof(CinemachineInputProvider))]
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class FreeLookPanning : MonoBehaviour
    {
        [SerializeField] private float panMultiplierSpeed = 30f;

        /// <summary>
        /// reference to chinemachine input provider
        /// </summary>
        private CinemachineInputProvider _cinInputProvider;

        /// <summary>
        /// reference to cinemachine virtual camera
        /// </summary>
        private CinemachineVirtualCamera _cinVirtualCamera;

        /// <summary>
        /// references to our camera controls schema
        /// </summary>
        private CameraControls _cameraControls;
        
        /// <summary>
        /// reference to camera transform, it is better to hold reference than to poll for this value constantly
        /// </summary>
        private Transform _camTransform;
        
        /// <summary>
        /// screen limits for panning, could be better handled than a margin of 25%? but this should do for now
        /// </summary>
        private readonly float _topLimit = Screen.height * 0.75f;
        private readonly float _botLimit = Screen.height * 0.25f;
        private readonly float _leftLimit = Screen.width * 0.75f;
        private readonly float _rightLimit = Screen.width * 0.25f;
        
        /// <summary>
        /// holds camera initial position for reset
        /// </summary>
        private Vector3 _startPosition;
        
        private void OnEnable() {
            // enable camera controls input asset
            _cameraControls.asset.Enable();
        }
        
        private void OnDisable() {
            // disable camera controls input asset
            _cameraControls.asset.Disable();
        }

        private void Awake() {
            // get references to our elements, do it once, very expensive operations
            _cameraControls = new CameraControls();
            _cinInputProvider = GetComponent<CinemachineInputProvider>();
            _cinVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            _camTransform = _cinVirtualCamera.VirtualCameraGameObject.transform;
            _startPosition = _camTransform.position;
        }

        private void Update() {
            PanCameraOnInput();
            ResetCameraOnInput();
        }

        /// <summary>
        /// quickly reset camera position to origin
        /// </summary>
        private void ResetCameraOnInput() {
            if (_cameraControls.Camera.Reset.ReadValue<float>() == 0)
                return;
            var camPosition = _camTransform.position;
            _camTransform.position = Vector3.Lerp(camPosition, _startPosition, .25f);
        }
        
        /// <summary>
        /// handles input fetching and performing camera pan
        /// </summary>
        private void PanCameraOnInput() {
            // if camera pan isn't enabled, leave as it is.
            if (_cameraControls.Camera.PanEnabled.ReadValue<float>() == 0) 
                return;
            
            // get axis values for camera if enabled
            var x = _cinInputProvider.GetAxisValue(0);
            var y = _cinInputProvider.GetAxisValue(1);
            
            // pan camera on changes
            if (x != 0 || y != 0)
                PanCamera(x, y);
        }

        /// <summary>
        /// get pan direction based off input
        /// </summary>
        /// <param name="xInput">x axis input</param>
        /// <param name="yInput">y axis input</param>
        /// <returns></returns>
        private Vector2 PanDirection(float xInput, float yInput) {
            var dir = Vector2.zero;
            dir.y += yInput >= _topLimit ? 1 : yInput <= _botLimit ? -1 : 0;
            dir.x += xInput >= _leftLimit ? 1 : xInput <= _rightLimit ? -1 : 0;
            return dir.normalized;
        }

        /// <summary>
        /// moves camera based off input
        /// </summary>
        /// <param name="xInput">x axis input</param>
        /// <param name="yInput">y axis input</param>
        private void PanCamera(float xInput, float yInput) {
            var dir = PanDirection(xInput, yInput);
            var camPosition = _camTransform.position;
            // pan camera position towards direction
            _camTransform.position = Vector3.Lerp(camPosition, camPosition + (Vector3)dir * panMultiplierSpeed, Time.deltaTime);
        }
    }
}