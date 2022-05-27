using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace WelcomeToHell.Interaction
{
    [RequireComponent(typeof(Collider))]
    public class HandButton : XRBaseInteractable
    {
        [SerializeField] private UnityEvent onPress = null;
        
        private float yMin = 0f;
        private float yMax = 0f;
        private bool previousPress = false;
        
        private IXRHoverInteractor hoverInteractor;
        private float previousHandHeight = 0.0f;
        
        protected override void Awake()
        {
            base.Awake();
            hoverEntered.AddListener(StartPress);
            hoverExited.AddListener(EndPress);
        }

        protected override void OnDestroy()
        {
            hoverEntered.RemoveListener(StartPress);
            hoverExited.RemoveListener(EndPress);
            base.OnDestroy();
        }

        private void StartPress(HoverEnterEventArgs hoverEnterEventArgs)
        {
            hoverInteractor = hoverEnterEventArgs.interactorObject;
            previousHandHeight = GetYPosition(hoverEnterEventArgs.interactorObject.transform.position);
        }

        private void EndPress(HoverExitEventArgs hoverExitEventArgs)
        {
            hoverInteractor = null;
            previousHandHeight = 0f;
            previousPress = false;
            SetYPosition(yMax);
        }

        private void Start()
        {
            SetMinMax();
        }

        private void SetMinMax()
        {
            Collider coll = GetComponent<Collider>();
            Vector3 position = transform.position;
            yMin = position.y - (coll.bounds.size.y * .5f);
            yMax = position.y;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            if (hoverInteractor != null)
            {
                float newHandHeight = GetYPosition(hoverInteractor.transform.position);
                float handDifference = previousHandHeight - newHandHeight;
                previousHandHeight = newHandHeight;

                float newPosition = transform.localPosition.y - handDifference;
                SetYPosition(newPosition);
                
                CheckPress();
            }
        }

        private float GetYPosition(Vector3 position)
        {
            Vector3 localPosition = position;
            
            return localPosition.y;
        }

        private void SetYPosition(float position)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = Mathf.Clamp(position, yMin, yMax);
            transform.position = newPosition;
        }

        private void CheckPress()
        {
            bool inPosition = InPosition();
            if (inPosition && !previousPress)
            {
                onPress.Invoke();
                previousPress = true;
            }

            
        }

        private bool InPosition()
        {
            float inRange = Mathf.Clamp(transform.position.y, yMin, yMin + 0.01f);
            return transform.position.y == inRange;
        }
    }
}
