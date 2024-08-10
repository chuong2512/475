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

[RequireComponent (typeof(BezierPath))]
[RequireComponent (typeof(LineRenderer))]
public class BezierPathLineRenderer : MonoBehaviour
{
	public float segementLength = 0.5f;
	[Space ()]
	public bool enableRendererOnAwake = true;
	public bool updateEachFrame = false;

	private BezierPath Path{ get { return GetComponent<BezierPath> (); } }

	private LineRenderer Line{ get { return GetComponent<LineRenderer> (); } }

	void Start ()
	{
		UpdateLineRenderer ();

		if (enableRendererOnAwake)
			Line.enabled = true;

		Line.useWorldSpace = true;
	}

	void LateUpdate ()
	{
		if (updateEachFrame)
			UpdateLineRenderer ();
	}

	[ContextMenu ("Update LineRenderer")]
	private void UpdateLineRenderer ()
	{
		float length = Path.EstimatedLength;
		int segementCount = Mathf.CeilToInt (length / segementLength) + 1;
		
		Line.positionCount = segementCount + 1;
		for (int i = 0; i < segementCount + 1; i++) {
			float progress = Mathf.InverseLerp (0, segementCount, i);
			Line.SetPosition (i, Path.GetPositionOnPath (progress));
		}
	}
}
