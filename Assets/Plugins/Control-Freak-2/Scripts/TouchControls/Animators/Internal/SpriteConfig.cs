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

namespace ControlFreak2.Internal
{
[System.Serializable]
public class SpriteConfig
	{
	public bool		enabled;
	public Sprite	sprite;
	public Color	color;
	public float	scale;
	public float	rotation;
	public Vector2	offset;

	public bool
		resetOffset,
		resetRotation,
		resetScale;

	[System.NonSerialized]
	public bool 	
		oneShotState;
	public float	
		duration,
		baseTransitionTime,
		colorTransitionFactor,	
		scaleTransitionFactor,	
		rotationTransitionFactor,
		offsetTransitionFactor;

		
	// -------------------
	public SpriteConfig()
		{
		this.enabled	= true;
		this.color		= Color.white;
		this.rotation	= 0;
		this.scale		= 1;
		this.duration					= 0.1f;
		this.baseTransitionTime			= 0.1f;
		this.colorTransitionFactor		= 1.0f;	
		this.scaleTransitionFactor		= 1.0f;	
		this.rotationTransitionFactor	= 1.0f;	
		this.offsetTransitionFactor		= 1.0f;	
		this.oneShotState				= false;
		}


	// --------------------
	public SpriteConfig(Sprite sprite, Color color) : this()
		{
		this.sprite		= sprite;
		this.color		= color;
		}


	// -----------------------
	public SpriteConfig(bool enabled, bool oneShot, float scale) : this()
		{	
		this.scale			= scale;
		this.enabled		= enabled;
		this.oneShotState	= oneShot;
		this.resetOffset	= oneShot;
		this.resetScale		= oneShot;
		this.resetRotation	= oneShot;
		}

		


	}
}

//! \endcond
