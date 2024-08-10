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

public class MaterialVariator : Variator
{
	[System.Serializable]
	public class MaterialVariation : Variation
	{
		public Material material;

		public override void Apply (GameObject target)
		{
			MaterialVariator mv = target.GetComponent<MaterialVariator> ();
			Renderer[] rds = mv.renderers;
			if (rds != null) {
				foreach (Renderer rd in rds) {
					if (rd != null)
						rd.sharedMaterial = material;
				}
			}
		}

		public override Object[] GetModifiedObjects (GameObject target)
		{
			MaterialVariator mv = target.GetComponent<MaterialVariator> ();
			List<Object> objs = new List<Object> ();
			if (mv.renderers != null) {
				foreach (Renderer rd in mv.renderers)
					objs.Add (rd);
			}
			return objs.ToArray ();
		}
	}

	public Renderer[] renderers;
	public MaterialVariation[] variations;

	public override Variation[] Variations { get { return variations; } }

	[ContextMenu ("Migrate From Other")]
	public void MigrateFromOther ()
	{
		List<MaterialVariator> materialVariators = new List<MaterialVariator> (GetComponents<MaterialVariator> ());
		materialVariators.Remove (this);
		if (materialVariators.Count > 0) {
			MaterialVariator other = materialVariators [0];
			Renderer otherRenderer = other.renderers [0];
			int otherMaterialIndex = -1;
			for (int i = 0; i < other.variations.Length; i++) {
				if (otherRenderer.sharedMaterial == other.variations [i].material) {
					otherMaterialIndex = i;
					break;
				}
			}

			if (otherMaterialIndex >= 0 && otherMaterialIndex < this.variations.Length) {
				Material myMaterialWithSameIndex = this.variations [otherMaterialIndex].material;
				foreach (Renderer myRenderer in this.renderers) {
					if (myRenderer.sharedMaterial != myMaterialWithSameIndex)
						myRenderer.sharedMaterial = myMaterialWithSameIndex;
				}
			}
		} else
			Debug.LogWarningFormat ("Cannot find other MaterialVariator at same GameObject {0}", gameObject.name);
	}
}
