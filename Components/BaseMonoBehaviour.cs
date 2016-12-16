using UnityEngine;

namespace Binocle.Components
{
    public class BaseMonoBehaviour : MonoBehaviour
    {
        public Entity Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        private Entity _entity;
    }
}

