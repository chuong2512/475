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

/// <summary>
/// Test animations using a simple GUI interface.
/// </summary>
[RequireComponent(typeof(Animation))]
public class AnimationTesterByGui : MonoBehaviour
{
	public AnimationClip idleAnimation;
	public AnimationClip[] animations;
	public int offsetX = 10;
	public int offsetY = 10;
	public int btHeight = 30;
	public int btWidth = 150;
	// Use this for initialization
	void Start ()
	{
		foreach (AnimationClip clip in animations)
			GetComponent<Animation>() [clip.name].layer = 1;
	}

	// Update is called once per frame
	void OnGUI ()
	{
		bool hasAnimation = false;
		for (int i = 0; i < animations.Length; i++) {
			if (GUI.Button (new Rect (offsetX, offsetY + i * btHeight, btWidth, btHeight), animations [i].name)) {
				GetComponent<Animation>().CrossFade (animations [i].name, 0.5f);
				hasAnimation = true;
			}
		}
		if (!hasAnimation)
			GetComponent<Animation>().CrossFadeQueued (idleAnimation.name, 0.5f);
	}
}

