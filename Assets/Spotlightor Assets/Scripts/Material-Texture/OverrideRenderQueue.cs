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

// DEFAULT RENDER QUEUE
// Background	= 1000
// Geometry 	= 2000
// Transparent	= 3000
// Overlay		= 4000
public class OverrideRenderQueue : MonoBehaviour
{
	[Header ("Geometry = 2000, Transparent = 3000")]
	public int renderQueue = 3001;
	public bool useSharedMaterial = false;

	void Start ()
	{
		SetRenderQueue ();
	}

	[ContextMenu ("Set Render Queue")]
	public void SetRenderQueue ()
	{
		if (useSharedMaterial)
			GetComponent<Renderer> ().sharedMaterial.renderQueue = renderQueue;
		else
			GetComponent<Renderer> ().material.renderQueue = renderQueue;
	}
}
