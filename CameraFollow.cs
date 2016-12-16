using UnityEngine;

namespace Binocle
{
    public class CameraFollow : MonoBehaviour
    {

        public Transform target;
        public float speed = 10f;
        public float delay = 1f;
        public bool freeCamera;

        private bool following = true;
        private float targetLoss;
        private bool hadTargetLastFrame;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void FixedUpdate()
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
                transform.position = Vector3.Lerp(transform.position, camTargetPos, Time.deltaTime * 5f);
                transform.position = new Vector3(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), Mathf.FloorToInt(transform.position.z));
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