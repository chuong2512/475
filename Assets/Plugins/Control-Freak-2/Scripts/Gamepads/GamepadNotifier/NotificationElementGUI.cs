/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2018 Dan's Game Tools
// http://DansGameTools.blogspot.com
// -------------------------------------------

//! \cond

using UnityEngine;
using System.Collections.Generic;

namespace ControlFreak2
{

public class NotificationElementGUI : MonoBehaviour 
	{
	public UnityEngine.UI.Text
		targetText;
	public UnityEngine.UI.Image
		targetIcon;

	private float 
		duration,
		elapsed;


	// ------------------
	public void End()
		{
		this.gameObject.SetActive(false);
		}

	// -------------------
	public void Show(string msg, Sprite icon, RectTransform parent, float duration)
		{
		RectTransform rect = (RectTransform)this.transform;
		rect.SetParent(parent, false);
		rect.SetSiblingIndex(0);
		this.gameObject.SetActive(true);	

		this.duration = duration;
		this.elapsed = 0;	

		this.targetText.text = msg;

		if (this.targetIcon != null)
			{
			if (this.targetIcon.enabled = (icon != null))
				this.targetIcon.sprite = icon;
			}
		}
	

	// -------------------
	public void UpdateNotification(float dt)
		{
		if  (!this.IsActive())
			return;

		this.elapsed += dt;
		if (this.elapsed > this.duration)
			this.gameObject.SetActive(false);

		this.OnUpdate();
		}


	// ------------------
	public float GetTimeElapsed()
		{ return this.elapsed; }

	// ----------------
	virtual protected void OnUpdate()
		{
		float n = (this.elapsed / this.duration);
		this.transform.localScale = Vector3.one * Mathf.Clamp01((1.0f - n) * 8.0f);

		
		}

	// ---------------------
	public bool IsActive()
		{ return this.gameObject.activeSelf; }

	// ------------------
	static public int Compare(NotificationElementGUI a, NotificationElementGUI b)
		{
		if ((a == null) || (b == null))
			return 0;
		if (a.IsActive() != b.IsActive())
			return (a.IsActive() ? -1 : 1);
		if (!a.IsActive())
			return 0;
		return ((a.elapsed == b.elapsed) ? 0 : (a.elapsed < b.elapsed) ? -1 : 1);
		}
	}
}

//! \endcond
