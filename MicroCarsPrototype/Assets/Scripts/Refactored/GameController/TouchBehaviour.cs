using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRubyShared
{
    public class TouchBehaviour : MonoBehaviour
    {
        public CarPhysicsRoot carPhysics;
        private SwipeGestureRecognizer swipeGesture;
        //private readonly List<Vector3> swipeLines = new List<Vector3>();

        // Use this for initialization
        void Start()
        {
            CreateSwipeLeftGesture();
            CreateSwipeRightGesture();
            
        }

        private void DebugText(string text, params object[] format)
        {
            //bottomLabel.text = string.Format(text, format);
            Debug.Log(string.Format(text, format));
        }

        private void CreateSwipeLeftGesture()
        {
            swipeGesture = new SwipeGestureRecognizer();
            swipeGesture.Direction = SwipeGestureRecognizerDirection.Left;

            swipeGesture.StateUpdated += SwipeLeftGestureCallback;
            swipeGesture.DirectionThreshold = 1.0f; // allow a swipe, regardless of slope
            FingersScript.Instance.AddGesture(swipeGesture);
        }

        private void CreateSwipeRightGesture()
        {
            swipeGesture = new SwipeGestureRecognizer();
            swipeGesture.Direction = SwipeGestureRecognizerDirection.Right;
            swipeGesture.StateUpdated += SwipeRightGestureCallback;
            swipeGesture.DirectionThreshold = 1.0f; // allow a swipe, regardless of slope
            FingersScript.Instance.AddGesture(swipeGesture);
        }

        private void SwipeLeftGestureCallback(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Ended)
            {

                Debug.Log("SwipedLeft");
                float swipeLenght = SwipeLenght(gesture.FocusX, gesture.FocusY);
                carPhysics.InitializeSwipe("left", swipeLenght);
                
                //HandleSwipe(gesture.FocusX, gesture.FocusY);
                //DebugText("Swiped from {0},{1} to {2},{3}; velocity: {4}, {5}", gesture.StartFocusX, gesture.StartFocusY, gesture.FocusX, gesture.FocusY, swipeGesture.VelocityX, swipeGesture.VelocityY);
            }
        }

        private void SwipeRightGestureCallback(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Ended)
            {
                Debug.Log("SwipedRight");
                float swipeLenght = SwipeLenght(gesture.FocusX, gesture.FocusY);
                carPhysics.InitializeSwipe("right", swipeLenght);
                //HandleSwipe(gesture.FocusX, gesture.FocusY);
                //DebugText("Swiped from {0},{1} to {2},{3}; velocity: {4}, {5}", gesture.StartFocusX, gesture.StartFocusY, gesture.FocusX, gesture.FocusY, swipeGesture.VelocityX, swipeGesture.VelocityY);
            }
        }

        private float SwipeLenght(float endX, float endY)
        {
            Vector2 start = new Vector2(swipeGesture.StartFocusX, swipeGesture.StartFocusY);
            Vector3 startWorld = Camera.main.ScreenToWorldPoint(start);
            Vector3 endWorld = Camera.main.ScreenToWorldPoint(new Vector2(endX, endY));
            float distance = Vector3.Distance(startWorld, endWorld);

            return distance;

            /*
            startWorld.z = endWorld.z = 0.0f;

            swipeLines.Add(startWorld);
            swipeLines.Add(endWorld);

            if (swipeLines.Count > 4)
            {
                swipeLines.RemoveRange(0, swipeLines.Count - 4);
            }
            */
        }
        
        

        
    }

}
