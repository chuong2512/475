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

[RequireComponent(typeof(sUiButton))]
public class sUiButtonSound : FunctionalMonoBehaviour
{
	[System.Serializable]
	public class SoundSetting
	{
		public AudioClip audioClip;
		public float volume = 1;
		public float pitch = 1;
	}
	public SoundSetting rollOverSound;
	public SoundSetting rollOutSound;
	public SoundSetting mouseDownSound;
	public SoundSetting mouseUpSound;
	public SoundSetting clickSound;
	private sUiButton button;

	private sUiButton Button {
		get {
			if (button == null)
				button = GetComponent<sUiButton> ();
			return button;
		}
	}

	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		Button.HoverIn += OnRollOverButton;
		Button.HoverOut += OnRollOutButton;
		Button.PressDown += OnMouseDownButton;
		Button.PressUp += OnMouseUpButton;
		Button.Clicked += OnClickedButton;
	}

	protected override void OnBecameUnFunctional ()
	{
		Button.HoverIn -= OnRollOverButton;
		Button.HoverOut -= OnRollOutButton;
		Button.PressDown -= OnMouseDownButton;
		Button.PressUp -= OnMouseUpButton;
		Button.Clicked -= OnClickedButton;
	}

	void OnClickedButton (GenericButton source)
	{
		PlaySound (clickSound);
	}
	
	void OnMouseUpButton (MouseEventDispatcher source)
	{
		PlaySound (mouseUpSound);
	}

	void OnMouseDownButton (MouseEventDispatcher source)
	{
		PlaySound (mouseDownSound);
	}

	void OnRollOutButton (MouseEventDispatcher source)
	{
		PlaySound (rollOutSound);
	}

	void OnRollOverButton (MouseEventDispatcher source)
	{
		PlaySound (rollOverSound);
	}
	
	private void PlaySound (SoundSetting sound)
	{
		if (sound.audioClip != null) 
			GlobalSoundPlayer.PlaySound (sound.audioClip, sound.volume, sound.pitch);
	}
}
