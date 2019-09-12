using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRubyShared
{
    public class TouchBehaviour : MonoBehaviour
    {
        private CarPhysicsRoot carPhysics;
        private CarPhysicsBraking carBraking;
        private SwipeGestureRecognizer swipeGesture;
        private LongPressGestureRecognizer longPressGesture;
        //private readonly List<Vector3> swipeLines = new List<Vector3>();

        public void SetupTouchForPlayer(GameObject player)
        {
            carPhysics = player.GetComponent<CarPhysicsRoot>();
            carBraking = player.GetComponent<CarPhysicsBraking>();
            Debug.Assert(carPhysics, $"{typeof(CarPhysicsRoot)} is null");
            Debug.Assert(carBraking, $"{typeof(CarPhysicsBraking)} is null");
        }


        void Start()
        {
            // Swipes in any direction activated
            CreateSwipeAnyGesture();
            CreateLongPressGesture();
        }

        private void DebugText(string text, params object[] format)
        {
            //bottomLabel.text = string.Format(text, format);
            Debug.Log(string.Format(text, format));
        }


        // --------------------------------------
        // SWIPE INPUT IMPLEMENTATION
        // --------------------------------------
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

        private void CreateSwipeAnyGesture()
        {
            swipeGesture = new SwipeGestureRecognizer();
            swipeGesture.Direction = SwipeGestureRecognizerDirection.Any;

            swipeGesture.StateUpdated += SwipeAnyGestureCallback;
            swipeGesture.DirectionThreshold = 1.0f; // allow a swipe, regardless of slope
            swipeGesture.MinimumDistanceUnits = 0.5f;
            swipeGesture.MinimumSpeedUnits = 1.0f;

            FingersScript.Instance.AddGesture(swipeGesture);
        }

        private void SwipeLeftGestureCallback(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Ended)
            {

                Debug.Log("SwipedLeft");
                float swipeLenght = SwipeLenght(gesture.FocusX, gesture.FocusY);
                Vector2 swipeVector = new Vector2(Mathf.Abs(gesture.StartFocusX - gesture.FocusX), Mathf.Abs(gesture.StartFocusY - gesture.FocusY));
                carPhysics.InitializeSwipe("left", swipeLenght, swipeVector);

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
                Vector2 swipeVector = new Vector2(Mathf.Abs(gesture.StartFocusX - gesture.FocusX), Mathf.Abs(gesture.StartFocusY - gesture.FocusY));
                carPhysics.InitializeSwipe("right", swipeLenght, swipeVector);
                //HandleSwipe(gesture.FocusX, gesture.FocusY);
                //DebugText("Swiped from {0},{1} to {2},{3}; velocity: {4}, {5}", gesture.StartFocusX, gesture.StartFocusY, gesture.FocusX, gesture.FocusY, swipeGesture.VelocityX, swipeGesture.VelocityY);
            }
        }

        private void SwipeAnyGestureCallback(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Ended)
            {
                Debug.Log("Swiped");
                float swipeLenght = SwipeLenght(gesture.FocusX, gesture.FocusY);
                Vector3 swipeVector = new Vector2(gesture.StartFocusX - gesture.FocusX, gesture.StartFocusY - gesture.FocusY);
                carPhysics.InitializeSwipe("any", swipeLenght, swipeVector);
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

        // --------------------------------------
        // LONG PRESS / HOLD INPUT IMPLEMENTATION
        // --------------------------------------
        private void CreateLongPressGesture()
        {
            longPressGesture = new LongPressGestureRecognizer();
            longPressGesture.MaximumNumberOfTouchesToTrack = 1;
            longPressGesture.StateUpdated += LongPressGestureCallback;
            FingersScript.Instance.AddGesture(longPressGesture);
        }

        private void LongPressGestureCallback(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Began)
            {
                DebugText("Long press began: {0}, {1}", gesture.FocusX, gesture.FocusY);
                BeginDrag(gesture.FocusX, gesture.FocusY);
            }
            /* LongHold Drag, not needed
            else if (gesture.State == GestureRecognizerState.Executing)
            {
                DebugText("Long press moved: {0}, {1}", gesture.FocusX, gesture.FocusY);
                DragTo(gesture.FocusX, gesture.FocusY);
            }
            */
            else if (gesture.State == GestureRecognizerState.Ended)
            {
                DebugText("Long press end: {0}, {1}, delta: {2}, {3}", gesture.FocusX, gesture.FocusY, gesture.DeltaX, gesture.DeltaY);
                EndDrag(longPressGesture.VelocityX, longPressGesture.VelocityY);
            }
        }

        private void BeginDrag(float screenX, float screenY)
        {
            carBraking.breaksOn();
        }

        private void DragTo(float screenX, float screenY)
        {
            
        }

        private void EndDrag(float velocityXScreen, float velocityYScreen)
        {
            carBraking.breaksOff();
        }

    }

}
