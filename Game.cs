using UnityEngine;
using Binocle.Processors;

namespace Binocle
{
    public class Game : MonoBehaviour
    {
        public static float TimeRate = 1f;
        private const float CLAMP_ADD = 0.008333334f;
        private const float FRAME_RATE = 60f;
        private const float FULL_DELTA = 0.01666667f;

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

        public static float LastTimeMult {get; private set;}
        public static float TimeMult {get; private set;}
        public static float DeltaTime {get; private set;}
        public static float ActualDeltaTime {get; private set;}
        public bool IsFixedTimeStep = false;

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
            LastTimeMult = TimeMult;
            ActualDeltaTime = Time.deltaTime * TimeRate;
            if (IsFixedTimeStep)
            {
                TimeMult = 1f * TimeRate;
                DeltaTime = 0.01666667f * TimeRate;
            }
            else
            {
                DeltaTime = Mathf.Min(ActualDeltaTime, 0.01666667f * (TimeRate + 0.008333334f));
                TimeMult = DeltaTime / 0.01666667f;
            }
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
