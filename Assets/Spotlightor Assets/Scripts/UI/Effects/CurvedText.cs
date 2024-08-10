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
using UnityEngine.UI;
using System;

[AddComponentMenu("UI/Effects/Curved Text")]
[RequireComponent(typeof(Text),typeof(RectTransform))]
#if UNITY_5_3_OR_NEWER
public class CurvedText : BaseMeshEffect
#else
public class CurvedText : BaseVertexEffect
#endif
{
	public AnimationCurve curveForText = AnimationCurve.Linear (0, 0, 1, 10);
	public float curveMultiplier = 1;

#if UNITY_EDITOR
		protected override void OnValidate ()
		{
				base.OnValidate ();
				if (curveForText [0].time != 0) {
						var tmpRect = curveForText [0];
						tmpRect.time = 0;
						curveForText.MoveKey (0, tmpRect);
				}
				if (curveForText [curveForText.length - 1].time != GetComponent<RectTransform> ().rect.width)
						OnRectTransformDimensionsChange ();
		}
#endif
	protected override void Awake ()
	{
		base.Awake ();
		OnRectTransformDimensionsChange ();
	}

	protected override void OnEnable ()
	{
		base.OnEnable ();
		OnRectTransformDimensionsChange ();
	}

	#if UNITY_5_3_OR_NEWER
	public override void ModifyMesh (VertexHelper vh)
	{
		if (IsActive ()) {
			List<UIVertex> verts = new List<UIVertex>();
			vh.GetUIVertexStream (verts);
			
			ApplyCurve (verts);
			
			vh.Clear ();
			vh.AddUIVertexTriangleStream (verts);
		}
	}
#else
	public override void ModifyVertices (List<UIVertex> verts)
	{
		if (IsActive ())
			ApplyCurve (verts);
	}
#endif

	public void ApplyCurve (List<UIVertex> verts)
	{
		for (int index = 0; index < verts.Count; index++) {
			var uiVertex = verts [index];
			RectTransform rectTrans = GetComponent<RectTransform> ();
			uiVertex.position.y += curveForText.Evaluate (rectTrans.rect.width * rectTrans.pivot.x + uiVertex.position.x) * curveMultiplier;
			verts [index] = uiVertex;
		}
	}

	protected override void OnRectTransformDimensionsChange ()
	{
		var tmpRect = curveForText [curveForText.length - 1];
		tmpRect.time = GetComponent<RectTransform> ().rect.width;
		curveForText.MoveKey (curveForText.length - 1, tmpRect);
	}
}
