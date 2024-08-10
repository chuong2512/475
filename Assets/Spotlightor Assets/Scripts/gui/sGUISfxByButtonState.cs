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

[RequireComponent(typeof(MouseEventDispatcher))]
public class sGUISfxByButtonState : MonoBehaviour
{
	[System.Serializable]
	public class SfxSetting
	{
		public AudioClip clip;
		public float pitch = 1;
		public float volumeScale = 1;
	}
	public SfxSetting overSfx;
	public SfxSetting outSfx;
	public SfxSetting downSfx;
	
	// Use this for initialization
	void Start ()
	{
		MouseEventDispatcher mouseEventDispatcher = GetComponent<MouseEventDispatcher> ();
		mouseEventDispatcher.HoverIn += OnRollOverButton;
		mouseEventDispatcher.HoverOut += OnRollOutButton;
		mouseEventDispatcher.PressDown += OnMouseDownOnButton;
	}

	private void OnMouseDownOnButton (MouseEventDispatcher source)
	{
		if (downSfx.clip)
			PlaySfx (downSfx);
	}

	private void OnRollOutButton (MouseEventDispatcher source)
	{
		if (outSfx.clip)
			PlaySfx (outSfx);
	}

	private void OnRollOverButton (MouseEventDispatcher source)
	{
		if (overSfx.clip) 
			PlaySfx (overSfx);
	}
	
	private void PlaySfx (SfxSetting sfx)
	{
		if (GetComponent<AudioSource>()) {
			GetComponent<AudioSource>().pitch = sfx.pitch;
			GetComponent<AudioSource>().PlayOneShot (sfx.clip, sfx.volumeScale);
		} else {
			if (sfx.pitch != 1)
				this.LogWarning ("SFX pitch is not supported without AudioSource component! Attach an AudioSource or set pitch to 1");
			
			AudioSource.PlayClipAtPoint (sfx.clip, transform.position, sfx.volumeScale);
		}
	}
}
