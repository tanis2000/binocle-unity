using UnityEngine;
using Binocle.Processors;

namespace Binocle
{
    public class Game : MonoBehaviour
    {

        public int DesignWidth
        {
            get { return _designWidth; }
            set { _designWidth = value; }
        }

        private int _designWidth = 320;

        public int DesignHeight
        {
            get { return _designHeight; }
            set { _designHeight = value; }
        }

        private int _designHeight = 240;

        protected PixelCamera pixelCamera;

        public virtual void Start()
        {
            pixelCamera = Camera.main.gameObject.AddComponent<PixelCamera>();
            pixelCamera.DesignWidth = _designWidth;
            pixelCamera.DesignHeight = _designHeight;

            var guiCamera = GameObject.Find("GUI Camera");
            if (guiCamera != null)
            {
                PixelCamera pixelCameraGUI = guiCamera.AddComponent<PixelCamera>();
                pixelCameraGUI.DesignWidth = _designWidth;
                pixelCameraGUI.DesignHeight = _designHeight;
            }

            ComponentTypeManager.Initialize();
        }

        public virtual void Update()
        {
        }

        public T CreateScene<T>(string name) where T : Scene
        {
            GameObject go = new GameObject(name);
            var s = go.AddComponent<T>();
            object o = (object)s;
            Scene scene = (Scene)o;
            scene.Game = this;
            return s;
        }

    }
}
