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

public abstract class Variator : MonoBehaviour
{
	[System.Serializable]
	public abstract class Variation
	{
		public string name = "A";

		public abstract void Apply (GameObject target);

		public abstract Object[] GetModifiedObjects (GameObject target);
	}

	public abstract Variation[] Variations{ get; }

	public bool ignoreRandomAll = false;

	void Awake ()
	{
		Destroy (this);
	}

	[ContextMenu ("随机此项变化")]
	public void RandomVariation ()
	{
		if (Variations.Length > 0) {
			Variator.Variation variation = Variations [Random.Range (0, Variations.Length)];
			variation.Apply (gameObject);
		}
	}

	[ContextMenu ("随机<所有>变化")]
	public void RandomAllVariators ()
	{
		Variator[] variators = GetComponents<Variator> ();
		foreach (Variator v in variators) {
			if (!v.ignoreRandomAll)
				v.RandomVariation ();
		}
	}

}
