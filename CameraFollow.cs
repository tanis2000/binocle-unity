using UnityEngine;

namespace Binocle
{
    public class CameraFollow : MonoBehaviour
    {

        public Transform target;
        public float speed = 10f;
        public float delay = 1f;
        public bool freeCamera;
        public Vector2 SubPixelCounter = Vector2.zero;

        private bool following = true;
        private float targetLoss;
        private bool hadTargetLastFrame;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (freeCamera && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
                following = false;

            if (target != null)
            {
                if (!hadTargetLastFrame)
                    following = true;

                hadTargetLastFrame = true;
            }
            else
            {
                if (hadTargetLastFrame)
                    targetLoss = Time.time;
                hadTargetLastFrame = false;
                following = false;
            }

            if (following)
            {
                var camTargetPos = target.position;
                camTargetPos.z = transform.position.z;
                camTargetPos.x = Mathf.Clamp(camTargetPos.x, Camera.main.orthographicSize * Camera.main.aspect, 64 * 16 - (Camera.main.orthographicSize * Camera.main.aspect));
                camTargetPos.y = Mathf.Clamp(camTargetPos.y, Camera.main.orthographicSize, 10000f);
                var newPos = Vector3.Lerp(transform.position, camTargetPos, Time.deltaTime * 5f);
                
                SubPixelCounter.x += newPos.x - transform.position.x;
                int dx = (int) Mathf.Round(SubPixelCounter.x);
                SubPixelCounter.x -= dx;

                SubPixelCounter.y += newPos.y - transform.position.y;
                int dy = (int) Mathf.Round(SubPixelCounter.y);
                SubPixelCounter.y -= dy;

                transform.position = new Vector3(transform.position.x + dx, transform.position.y + dy, Mathf.RoundToInt(newPos.z));

                //transform.position = new Vector3(Mathf.RoundToInt(newPos.x), Mathf.RoundToInt(newPos.y), Mathf.RoundToInt(newPos.z));
            }
            else
            {
                if (Input.GetKey(KeyCode.S))
                    transform.position += Vector3.down * Time.deltaTime * speed;
                if (Input.GetKey(KeyCode.D))
                    transform.position += Vector3.right * Time.deltaTime * speed;
                if (Input.GetKey(KeyCode.A))
                    transform.position += Vector3.left * Time.deltaTime * speed;
                if (Input.GetKey(KeyCode.W))
                    transform.position += Vector3.up * Time.deltaTime * speed;
            }
        }

        public static void GetScreenExtents(ref float minX, ref float maxX, ref float minY, ref float maxY)
        {
            Vector3 vector = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width, (float)Screen.height, 10f));
            maxX = vector.x;
            maxY = vector.y;
            vector = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 10f));
            minX = vector.x;
            minY = vector.y;
        }

    }
}