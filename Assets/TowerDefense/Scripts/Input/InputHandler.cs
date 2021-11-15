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

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            new GameObject("Input Handler").AddComponent<InputHandler>();
            _masterControls = new MasterInput();
            _masterControls.asset.Enable();
        }
        
        private void OnDestroy() {
            // disable camera controls input asset
            _masterControls.asset.Disable();
        }
    }
}