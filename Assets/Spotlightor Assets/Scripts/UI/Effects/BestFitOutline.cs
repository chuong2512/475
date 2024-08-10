/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

//Author: Melang http://forum.unity3d.com/members/melang.593409/
using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	[AddComponentMenu ("UI/Effects/BestFit Outline", 15)]
	public class BestFitOutline : Shadow
	{
		protected BestFitOutline ()
		{
		}
#if UNITY_5_3_OR_NEWER
		public override void ModifyMesh (VertexHelper vh)
		{
			if (!this.IsActive ())
				return;

			Text foundtext = GetComponent<Text> ();
			
			float best_fit_adjustment = 1f;
			
			if (foundtext && foundtext.resizeTextForBestFit) {
				best_fit_adjustment = (float)foundtext.cachedTextGenerator.fontSizeUsedForBestFit / (foundtext.resizeTextMaxSize - 1); //max size seems to be exclusive 
				//Debug.Log("best_fit_adjustment:"+best_fit_adjustment);
				
			}

			List<UIVertex> verts = new List<UIVertex>();
			vh.GetUIVertexStream (verts);
			
			int start = 0;
			int count = verts.Count;
			base.ApplyShadow (verts, base.effectColor, start, verts.Count, base.effectDistance.x * best_fit_adjustment, base.effectDistance.y * best_fit_adjustment);
			start = count;
			count = verts.Count;
			base.ApplyShadow (verts, base.effectColor, start, verts.Count, base.effectDistance.x * best_fit_adjustment, -base.effectDistance.y * best_fit_adjustment);
			start = count;
			count = verts.Count;
			base.ApplyShadow (verts, base.effectColor, start, verts.Count, -base.effectDistance.x * best_fit_adjustment, base.effectDistance.y * best_fit_adjustment);
			start = count;
			count = verts.Count;
			base.ApplyShadow (verts, base.effectColor, start, verts.Count, -base.effectDistance.x * best_fit_adjustment, -base.effectDistance.y * best_fit_adjustment);

			vh.Clear ();
			vh.AddUIVertexTriangleStream (verts);
		}
#else
		public override void ModifyVertices (List<UIVertex> verts)
		{
			if (!this.IsActive ())
				return;
			
			Text foundtext = GetComponent<Text> ();
			
			float best_fit_adjustment = 1f;
			
			if (foundtext && foundtext.resizeTextForBestFit) {
				best_fit_adjustment = (float)foundtext.cachedTextGenerator.fontSizeUsedForBestFit / (foundtext.resizeTextMaxSize - 1); //max size seems to be exclusive 
				//Debug.Log("best_fit_adjustment:"+best_fit_adjustment);
				
			}

			int start = 0;
			int count = verts.Count;
			base.ApplyShadow (verts, base.effectColor, start, verts.Count, base.effectDistance.x * best_fit_adjustment, base.effectDistance.y * best_fit_adjustment);
			start = count;
			count = verts.Count;
			base.ApplyShadow (verts, base.effectColor, start, verts.Count, base.effectDistance.x * best_fit_adjustment, -base.effectDistance.y * best_fit_adjustment);
			start = count;
			count = verts.Count;
			base.ApplyShadow (verts, base.effectColor, start, verts.Count, -base.effectDistance.x * best_fit_adjustment, base.effectDistance.y * best_fit_adjustment);
			start = count;
			count = verts.Count;
			base.ApplyShadow (verts, base.effectColor, start, verts.Count, -base.effectDistance.x * best_fit_adjustment, -base.effectDistance.y * best_fit_adjustment);
		}
#endif
	}
}
