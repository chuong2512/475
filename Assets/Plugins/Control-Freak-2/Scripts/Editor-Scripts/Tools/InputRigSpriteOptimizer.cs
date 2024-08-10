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

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

using ControlFreak2.Internal;
using ControlFreak2;

namespace ControlFreak2Editor
{

public class InputRigSpriteOptimizer : ISpriteOptimizer
	{

	// ----------------------
	static public void OptimizeHierarchy(GameObject root)
		{
		

		// if all sprites belong to the same texure dont pack!

		

		}	
		

	// --------------
	private List<SpriteElem>	sprites;

	
	// ------------------
	private InputRigSpriteOptimizer()
		{
		this.sprites = new List<SpriteElem>(32);		
		}



	// ---------------------
	void ISpriteOptimizer.AddSprite(Sprite sprite)
		{
		SpriteElem elem = this.FindElem(sprite);
		if (elem != null)
			return;	
			
		elem = new SpriteElem(sprite);
		this.sprites.Add(elem);
		}

	// ------------------
	Sprite ISpriteOptimizer.GetOptimizedSprite(Sprite sprite)
		{
		SpriteElem elem = this.FindElem(sprite);
		if (elem != null)
			return sprite;
			
		return elem.GetNewSprite();
		}
		




	// --------------------
	private SpriteElem FindElem(Sprite srcSprite)
		{
		return this.sprites.Find(elem => (elem.srcSprite == srcSprite));	
		}		


	// ----------------------
	private class SpriteElem
		{
		public Sprite srcSprite;	
		public Sprite newSprite;


		// ---------------
		public SpriteElem(Sprite srcSprite)
			{
			this.srcSprite = srcSprite;
			this.newSprite = srcSprite;
			}

		// ----------------
		public Sprite GetNewSprite()
			{
			return ((this.newSprite == null) ? this.srcSprite : this.newSprite);
			}
		}
	
	}
}

#endif
