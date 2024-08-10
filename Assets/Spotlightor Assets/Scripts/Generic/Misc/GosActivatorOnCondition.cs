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

public abstract class GosActivatorOnCondition : MonoBehaviour
{
	public bool updateEachFrame = false;

	[Space ()]
	public List<GameObject> activeObjectsIfTrue;
	public List<GameObject> activeObjectsIfFalse;

	[Space ()]
	public List<Behaviour> enableBehavioursIfTrue;
	public List<Behaviour> disableBehavioursIfTrue;

	protected abstract bool HasMetCondition{ get; }

	protected virtual void Awake ()
	{
		if (enabled)
			ActivateObjectsOnCondition ();
	}

	protected virtual void OnEnable ()
	{
		ActivateObjectsOnCondition ();
	}

	protected virtual void Update ()
	{
		if (updateEachFrame)
			ActivateObjectsOnCondition ();
	}

	public void ActivateObjectsOnCondition ()
	{
		ActivateObjectsOnCondition (this.HasMetCondition);
	}

	[ContextMenu ("Activate Objs on TRUE")]
	public void ActivateObjectsOnConditionMet ()
	{
		ActivateObjectsOnCondition (true);
	}

	[ContextMenu ("Activate Objs on FALSE")]
	public void ActivateObjectsOnConditionNotMet ()
	{
		ActivateObjectsOnCondition (false);
	}

	private void ActivateObjectsOnCondition (bool hasMetCondition)
	{
		for (int i = 0; i < activeObjectsIfTrue.Count; i++)
			activeObjectsIfTrue [i].SetActive (hasMetCondition);

		for (int i = 0; i < activeObjectsIfFalse.Count; i++)
			activeObjectsIfFalse [i].SetActive (!hasMetCondition);

		for (int i = 0; i < enableBehavioursIfTrue.Count; i++)
			enableBehavioursIfTrue [i].enabled = hasMetCondition;

		for (int i = 0; i < disableBehavioursIfTrue.Count; i++)
			disableBehavioursIfTrue [i].enabled = !hasMetCondition;
	}
}
