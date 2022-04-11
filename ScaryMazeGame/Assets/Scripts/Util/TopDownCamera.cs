using System;
using UnityEngine;
using UnityEngine.UI;

namespace Util
{
    public class TopDownCamera : MonoBehaviour
    {
        [Header("Mouse settings")]
        [Range(0.1f, 5.0f)]
        public float mouseSensitivity;
        
        [Range(0.1f, 5.0f)]
        public float scrollSensitivity;
        
        public bool invertControls;

        
        [Header("World Borders")]
        public bool keepInBorders;
        
        public Vector3 border1;
        public Vector3 border2;


        private bool _canMove;

        
        private void Start()
        {
            _canMove = false;
        }

        private void Update()
        {
            HandleFire2Button();
            
            var offset = _canMove ? GetOffsetFromMouseInput() : Vector3.zero;
            ApplyPositionOffset(offset);
        }
        
        
        private void EnableMovement()
        {
            _canMove = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    
        private void DisableMovement()
        {
            _canMove = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }


        private void HandleFire2Button()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                EnableMovement();
            }

            if (Input.GetButtonUp("Fire2"))
            {
                DisableMovement();
            }
        }

        private Vector3 GetOffsetFromMouseInput()
        {
            var deltaX = Input.GetAxis("Mouse X") * mouseSensitivity;
            var deltaY = Input.GetAxis("Mouse Y") * mouseSensitivity;
            var scroll = Input.mouseScrollDelta.y * scrollSensitivity * -1;
            

            var xDirection = GetHorizontalRightDirection();
            var yDirection = GetHorizontalForwardDirection();
            var scrollDirection = scroll * transform.forward;

            var offset = deltaX * xDirection + deltaY * yDirection + scrollDirection;
            var direction = invertControls ? -1 : 1;

            return direction * offset;
        }

        private void ApplyPositionOffset(Vector3 offset)
        {
            var newPosition = transform.position + offset;

            if (keepInBorders)
            {
                newPosition.x = ClampedPosition(newPosition.x, border1.x, border2.x);
                newPosition.y = ClampedPosition(newPosition.y, border1.y, border2.y);
                newPosition.z = ClampedPosition(newPosition.z, border1.z, border2.z);
            }

            transform.position = newPosition;
        }

        private static float ClampedPosition(float toClamp, float pos1, float pos2)
        {
            var min = Mathf.Min(pos1, pos2);
            var max = Mathf.Max(pos1, pos2);

            return Mathf.Clamp(toClamp, min, max);
        } 

        private Vector3 GetHorizontalForwardDirection()
        {
            // Rider asked.
            var thisTransform = transform;
            
            var up = thisTransform.up;
            var forward = thisTransform.forward;

            up.y = 0.0f;
            forward.y = 0.0f;
            
            var isLookingDown = forward.sqrMagnitude < 1e-10;
            return isLookingDown ? up.normalized : forward.normalized;
        }

        private Vector3 GetHorizontalRightDirection()
        {
            return transform.right;
        }
    }
}
