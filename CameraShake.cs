using System;
using Binocle.Components;
using UnityEngine;


namespace Binocle
{
	public class CameraShake : BaseMonoBehaviour
	{
		Vector2 _shakeDirection = new Vector2(0, 0);
		Vector2 _shakeOffset = new Vector2(0, 0);
		float _shakeIntensity = 0f;
		float _shakeDegredation = 0.95f;


		/// <summary>
		/// if the shake is already running this will overwrite the current values only if shakeIntensity > the current shakeIntensity.
		/// if the shake is not currently active it will be started.
		/// </summary>
		/// <param name="shakeIntensity">how much should we shake it</param>
		/// <param name="shakeDegredation">higher values cause faster degradation</param>
		/// <param name="shakeDirection">Vector3.zero will result in a shake on just the x/y axis. any other values will result in the passed
		/// in shakeDirection * intensity being the offset the camera is moved</param>
		public void shake( float shakeIntensity = 15f, float shakeDegredation = 0.9f, Vector2 shakeDirection = default( Vector2 ) )
		{
			if( _shakeIntensity < shakeIntensity )
			{
				_shakeDirection = shakeDirection;
				_shakeIntensity = shakeIntensity;
				if( shakeDegredation < 0f || shakeDegredation >= 1f )
					shakeDegredation = 0.95f;

				_shakeDegredation = shakeDegredation;
			}
		}


		public void Update()
		{
			if( Math.Abs( _shakeIntensity ) > 0f )
			{
                var cameraFollow = Camera.main.GetComponent<CameraFollow>();
                if (cameraFollow != null) {
                    cameraFollow.enabled = false;
                }

				_shakeOffset = _shakeDirection;
				if( _shakeOffset.x != 0f || _shakeOffset.y != 0f )
				{
					_shakeOffset.Normalize();
				}
				else
				{
					_shakeOffset.x = _shakeOffset.x + UnityEngine.Random.value - 0.5f;
					_shakeOffset.y = _shakeOffset.y + UnityEngine.Random.value - 0.5f;
				}

				// TODO: this needs to be multiplied by camera zoom so that less shake gets applied when zoomed in
				_shakeOffset *= _shakeIntensity;
				_shakeIntensity *= _shakeDegredation;
				if( Math.Abs( _shakeIntensity ) <= 0.01f )
				{
					_shakeIntensity = 0f;
					_shakeOffset.x = 0f;
					_shakeOffset.y = 0f;
                    if (cameraFollow != null) {
                        cameraFollow.enabled = true;
                    }
				}
                //Debug.Log(_shakeIntensity);
                //Debug.Log(_shakeOffset);
                Camera.main.transform.position += new Vector3((int)_shakeOffset.x, (int)_shakeOffset.y, 0);
            }
		}
	}
}

