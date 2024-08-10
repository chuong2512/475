using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeVirtualJoystick : VirtualJoystickBase
{
	[System.Serializable]
	public class SwipHistory
	{
		public float targetLength = 0.2f;
		private List<Vector2> historyPoints = new List<Vector2> ();
		private float length = 0;

		public float Length { get { return length; } }

		public List<Vector2> HistoryPoints { get { return historyPoints; } }

		public Vector2 LatestPoint{ get { return historyPoints.Count > 0 ? historyPoints [historyPoints.Count - 1] : Vector2.zero; } }

		public Vector2 OldestPoint{ get { return historyPoints.Count > 0 ? historyPoints [0] : Vector2.zero; } }

		public void AddPoint (Vector2 point)
		{
			if (historyPoints.Count > 0) {
				float distance = Vector2.Distance (point, LatestPoint);
				if (distance != 0) {
					historyPoints.Add (point);
					length += distance;

					KeepLengthToTargetValue ();
				}
			} else
				historyPoints.Add (point);
		}

		private void KeepLengthToTargetValue ()
		{
			while (historyPoints.Count >= 2 && length > targetLength) {
				Vector2 oldestPoint = historyPoints [0];
				Vector2 nextOldestPoint = historyPoints [1];
				float distance = Vector2.Distance (oldestPoint, nextOldestPoint);

				float newLength = length - distance;
				if (newLength > targetLength) {
					length -= distance;
					historyPoints.RemoveAt (0);
				} else
					break;
			}
		}

		public void AlignPointsToDir (Vector2 oldestToLatestDir)
		{
			oldestToLatestDir.Normalize ();
			if (historyPoints.Count > 1 && oldestToLatestDir != Vector2.zero) {
				length = Vector2.Distance (OldestPoint, LatestPoint);
				float lengthPerSegment = length / ((float)historyPoints.Count - 1f);

				for (int i = historyPoints.Count - 1; i >= 1; i--) {
					Vector2 point = historyPoints [i];
					historyPoints [i - 1] = point - oldestToLatestDir * lengthPerSegment;
				}
			}
		}

		public void Clear ()
		{
			length = 0;
			historyPoints.Clear ();
		}
	}


	public SwipHistory swipeHistoryInInches = new SwipHistory ();
	[SingleLineLabel ()]
	public float maxStrengthSwipeLengthInInches = 0.25f;
	public float noiseInInches = 0.05f;

	public bool logInfo = false;

	private bool isPressed = false;
	private int trackingFingerId = -1;

	public override Vector2 Strength { 
		get {
			Vector2 strength = Vector2.zero;

			Vector2 oldestPoint = swipeHistoryInInches.OldestPoint;
			Vector2 latestPoint = swipeHistoryInInches.LatestPoint;

			if (oldestPoint != latestPoint) {
				Vector2 oldToNew = latestPoint - oldestPoint;
				float oldToNewLength = oldToNew.magnitude;
				float strengthLength = Mathf.InverseLerp (noiseInInches, maxStrengthSwipeLengthInInches, oldToNewLength);

				strength = oldToNew.normalized * strengthLength;

				if (logInfo)
					Debug.LogFormat ("oldToNew = {0:0.00}, swipeLength = {1:0.00}, pointsCount = {2}",
						oldToNewLength, swipeHistoryInInches.Length, swipeHistoryInInches.HistoryPoints.Count);
			}

			return strength;
		} 
	}

	public bool IsPressed {
		get { return isPressed; }
		private set {
			if (isPressed != value) {
				isPressed = value;

				swipeHistoryInInches.Clear ();
			}
		}
	}

	void Update ()
	{
		if (!IsPressed) {
			for (int i = 0; i < ControlFreak2.CF2Input.touchCount; i++) {
				ControlFreak2.InputRig.Touch touch = ControlFreak2.CF2Input.GetTouch (i);
				if (touch.phase == TouchPhase.Began && IsInJoystickScreenArea (touch.position)) {
					trackingFingerId = touch.fingerId;
					IsPressed = true;

					swipeHistoryInInches.AddPoint (ScreenPixelToInches (touch.position));
				}
			}
		} else {
			bool newIsPressed = false;
			for (int i = 0; i < ControlFreak2.CF2Input.touchCount; i++) {
				ControlFreak2.InputRig.Touch touch = ControlFreak2.CF2Input.GetTouch (i);
				if (touch.fingerId == trackingFingerId
				    && touch.phase != TouchPhase.Ended
				    && touch.phase != TouchPhase.Canceled) {
					newIsPressed = true;

					swipeHistoryInInches.AddPoint (ScreenPixelToInches (touch.position));

					if (touch.phase == TouchPhase.Stationary && fixedDirectionsCount > 0 && Strength != Vector2.zero) {
						swipeHistoryInInches.AlignPointsToDir (this.StrengthDirFixed);
					}
				}
			}
			IsPressed = newIsPressed;
		}
	}

	private bool IsInJoystickScreenArea (Vector2 position)
	{
		Vector2 viewportPos = new Vector2 (position.x / Screen.width, position.y / Screen.height);
		return inputViewportArea.Contains (viewportPos);
	}

	void OnGUI ()
	{
		if (logInfo) {
			foreach (Vector2 pt in swipeHistoryInInches.HistoryPoints) {
				Vector2 guiPoint = ScreenInchesToPixels (pt);
				guiPoint.y = Screen.height - guiPoint.y;
				float pointRadius = 3;
				GUI.DrawTexture (new Rect (guiPoint.x - pointRadius, guiPoint.y - pointRadius, pointRadius * 2, pointRadius * 2), Texture2D.whiteTexture);
			}
		}
	}
}
