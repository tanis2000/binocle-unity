using UnityEngine;

namespace Binocle
{
    [ExecuteInEditMode]
    public class PixelCamera : MonoBehaviour
    {

        public int DesignWidth = 320;
        public int DesignHeight = 240;
        public Camera _camera;
        public int Scaling = 1;

        // Use this for initialization
        void Start()
        {
            _camera = gameObject.GetComponent<Camera>();
        }

        void LateUpdate()
        {
            int w = Screen.width;
            int h = Screen.height;
            int m = h / DesignHeight;
            if (m < 1)
                m = 1;
            float ratio = h / (float)(DesignHeight * m);
            //Debug.Log ("w: " + DesignWidth + " h: " + DesignHeight + " m: " + m + " ratio: " + ratio);

            _camera.orthographicSize = DesignHeight * ratio / 2;
            Scaling = m;
        }
    }
}