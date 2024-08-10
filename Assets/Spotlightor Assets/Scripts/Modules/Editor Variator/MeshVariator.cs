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

public class MeshVariator : Variator
{
	[System.Serializable]
	public class MeshVariation : Variation
	{
		public Mesh mesh;

		public override void Apply (GameObject target)
		{
			MeshVariator targetMeshVariator = target.GetComponent<MeshVariator> ();
			if (targetMeshVariator != null) {
				if (targetMeshVariator.targetMeshFilter != null)
					targetMeshVariator.targetMeshFilter.sharedMesh = mesh;
				if (targetMeshVariator.bonusTargetMeshFilter != null)
					targetMeshVariator.bonusTargetMeshFilter.ForEach (mf => mf.sharedMesh = mesh);
				if (targetMeshVariator.targetMeshCollider != null)
					targetMeshVariator.targetMeshCollider.sharedMesh = mesh;
			}
		}

		public override Object[] GetModifiedObjects (GameObject target)
		{
			MeshVariator targetMeshVariator = target.GetComponent<MeshVariator> ();

			List<Object> objs = new List<Object> ();
			if (targetMeshVariator.targetMeshFilter != null)
				objs.Add (targetMeshVariator.targetMeshFilter);
			if (targetMeshVariator.targetMeshCollider != null)
				objs.Add (targetMeshVariator.targetMeshCollider);

			return objs.ToArray ();
		}
	}

	public MeshFilter targetMeshFilter;
	public List<MeshFilter> bonusTargetMeshFilter;
	public MeshCollider targetMeshCollider;
	public MeshVariation[] variations;

	public override Variation[] Variations { get { return variations; } }

	void Reset ()
	{
		if (targetMeshFilter == null)
			targetMeshFilter = GetComponent<MeshFilter> ();
		if (targetMeshCollider == null)
			targetMeshCollider = GetComponent<MeshCollider> ();
		if (variations == null || variations.Length == 0) {
			MeshVariation variation = new MeshVariation ();
			if (targetMeshFilter != null)
				variation.mesh = targetMeshFilter.sharedMesh;
			
			variations = new MeshVariation[]{ variation };
		}
	}
}
