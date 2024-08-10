/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BezierPath : MonoBehaviour
{
	public enum CurveTypes
	{
		iTween = -1,
		Bezier = 0,
	}

	public CurveTypes curveType = CurveTypes.iTween;
	public bool alwaysDrawGizmos = false;
	private float estimatedLength = -1f;

	public float EstimatedLength {
		get {
			if (estimatedLength == -1) {
				estimatedLength = 0;
				if (Points != null && Points.Count > 1) {
					for (int i = 1; i < Points.Count; i++) {
						estimatedLength += Vector3.Distance (Points [i], Points [i - 1]);
					}
				}
			}
			return estimatedLength;
		}
	}

	public abstract List<Vector3> Points{ get; }

	public Vector3 GetPositionOnPath (float progress)
	{
		Vector3 pos = Vector3.zero;
		if (curveType == CurveTypes.iTween)
			pos = iTween.PointOnPath (Points.ToArray (), Mathf.Clamp01 (progress));
		else if (curveType == CurveTypes.Bezier) {
			List<Vector3> points = this.Points;
			int pointsCount = points.Count;
			if (pointsCount == 0)
				pos = transform.position;
			else if (pointsCount == 1)
				pos = points [0];
			else if (pointsCount == 2)
				pos = Vector3.Lerp (points [0], points [1], progress);
			else if (pointsCount == 3)
				pos = MathAddons.BezierQuadratic (points [0], points [2], points [1], progress);
			else {
				if (progress <= 0)
					pos = points [0];
				else if (progress >= 1)
					pos = points [pointsCount - 1];
				else {
					int segementsCount = pointsCount - 2;
					int segmentIndex = Mathf.FloorToInt ((float)segementsCount * progress);

					Vector3 pointFrom = Vector3.zero;
					if (segmentIndex == 0)
						pointFrom = points [0];
					else
						pointFrom = (points [segmentIndex] + points [segmentIndex + 1]) * 0.5f;

					Vector3 pointControl = points [segmentIndex + 1];

					Vector3 pointTo = Vector3.zero;
					if (segmentIndex == segementsCount - 1)
						pointTo = points [pointsCount - 1];
					else
						pointTo = (points [segmentIndex + 1] + points [segmentIndex + 2]) * 0.5f;

					float segmentProgressLength = 1f / (float)segementsCount;
					float segmentProgress = (progress - (float)segmentIndex * segmentProgressLength) / segmentProgressLength;
					pos = MathAddons.BezierQuadratic (pointFrom, pointTo, pointControl, segmentProgress);
				}
			}
		}
		return pos;
	}

	public Quaternion GetLookRotationOnPath (float progress)
	{
		progress = Mathf.Clamp01 (progress);
		float fromPercent = progress;
		float toPercent = progress + 0.01f;
		if (toPercent > 1) {
			toPercent = 1;
			fromPercent = toPercent - 0.01f;
		}
		return Quaternion.LookRotation (GetPositionOnPath (toPercent) - GetPositionOnPath (fromPercent));
	}

	public Vector3 FindNearestPathPoint (Vector3 targetPosition)
	{
		float minDistance = float.MaxValue;
		float distance = 0;
		int nearestWaypointIndex = -1;
		for (int i = 0; i < Points.Count; i++) {
			Vector3 offset = targetPosition - Points [i];
			distance = offset.sqrMagnitude;
			if (distance < minDistance) {
				nearestWaypointIndex = i;
				minDistance = distance;
			}
		}
		return Points [nearestWaypointIndex];
	}

	public Vector3[] FindNearestPathSegment (Vector3 targetPosition)
	{
		Vector3[] linePoints = new Vector3[2];
		if (Points.Count < 2) {
			this.LogWarning ("Path points must be greater than 2");
			linePoints [0] = transform.position;
			linePoints [0] = transform.position;
		} else if (Points.Count == 2) {
			linePoints [0] = Points [0];
			linePoints [1] = Points [1];
		} else {
			float nearestDistance = float.MaxValue - 1;
			int nearestDistanceIndex = -1;
			float nextNearestDistance = float.MaxValue;
			int nextNearestDistanceIndex = -1;
			for (int i = 0; i < Points.Count; i++) {
				Vector3 pathPoint = Points [i];
				float distance = Vector3.Distance (pathPoint, targetPosition);
				if (distance < nearestDistance) {
					if (nearestDistance >= 0) {
						nextNearestDistanceIndex = nearestDistanceIndex;
						nextNearestDistance = nearestDistance;
					}
					nearestDistanceIndex = i;
					nearestDistance = distance;
				} else if (distance < nextNearestDistance) {
					nextNearestDistanceIndex = i;
					nextNearestDistance = distance;
				}
			}
			linePoints [0] = Points [Mathf.Min (nearestDistanceIndex, nextNearestDistanceIndex)];
			linePoints [1] = Points [Mathf.Max (nearestDistanceIndex, nextNearestDistanceIndex)];
		}
		
		return linePoints;
	}

	void OnDrawGizmosSelected ()
	{
		DrawGizmos ();
	}

	void OnDrawGizmos ()
	{
		if (alwaysDrawGizmos && !Application.isPlaying)
			DrawGizmos ();
	}

	void DrawGizmos ()
	{
		if (Points != null && Points.Count >= 2) {
			int numCurveParts = Points.Count * 5;
			float step = (float)1 / numCurveParts;
			float progress = 0;
			Vector3 lineStart = Points [0];
			Vector3 lineEnd = Vector3.zero;
			Gizmos.color = Color.cyan;
			for (int i = 0; i < numCurveParts; i++) {
				progress += step;
				lineEnd = GetPositionOnPath (progress);
				Gizmos.DrawLine (lineStart, lineEnd);
				lineStart = lineEnd;
			}
		}
	}

}
