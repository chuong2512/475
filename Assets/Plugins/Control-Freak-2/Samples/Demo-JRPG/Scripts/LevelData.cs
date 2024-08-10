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

using UnityEngine;
using System.Collections.Generic;

namespace ControlFreak2.Demos.RPG
{

public class LevelData : MonoBehaviour 
	{
	public List<InteractiveObjectBase> 
		interactiveObjectList;


	// --------------------
	public LevelData() : base()
		{
		this.interactiveObjectList = new List<InteractiveObjectBase>();
		}


	// -------------------
	public InteractiveObjectBase FindInteractiveObjectFor(CharacterAction chara)
		{
		InteractiveObjectBase 
			nearestObj = null;
		float	
			nearestObjDistSq = 0;



		for (int i = 0; i < this.interactiveObjectList.Count; ++i)
			{
			InteractiveObjectBase o = this.interactiveObjectList[i];
			if ((o == null) || !o.IsNear(chara))
				continue;

			float distSq = (chara.transform.position - o.transform.position).sqrMagnitude;

			if ((nearestObj == null) || (distSq < nearestObjDistSq))
				{
				nearestObj = o;
				nearestObjDistSq = distSq;
				}
			}		

		return nearestObj;
		}
	}
}
