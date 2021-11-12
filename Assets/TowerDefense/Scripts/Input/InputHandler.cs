using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Input
{
    public class InputHandler : MonoBehaviour
    {
        public static MasterInput Input => _masterControls;
        
        public static MasterInput.CameraActions Camera => _masterControls.Camera;

        public static MasterInput.TurretPlacerActions TurretPlacer => _masterControls.TurretPlacer;
        
        /// <summary>
        /// references to our camera controls schema
        /// </summary>
        private static MasterInput _masterControls;

        private void Awake() {
            _masterControls = new MasterInput();
        }

        private void OnEnable() {
            // enable camera controls input asset
            _masterControls.asset.Enable();
        }
        
        private void OnDisable() {
            // disable camera controls input asset
            _masterControls.asset.Disable();
        }
    }
}