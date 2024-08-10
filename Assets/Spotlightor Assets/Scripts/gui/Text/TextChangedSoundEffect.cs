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

[RequireComponent(typeof(TextDisplayer))]
public class TextChangedSoundEffect : FunctionalMonoBehaviour
{
	public AudioClip textChangedSound;
	public float volume = 1;
	public float minSoundPlayInterval = 0.3f;
	private float lastSoundPlayTime = 0;

	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		GetComponent<TextDisplayer> ().TextChanged += HandleTextChanged;
	}

	protected override void OnBecameUnFunctional ()
	{
		GetComponent<TextDisplayer> ().TextChanged -= HandleTextChanged;
	}
	
	void HandleTextChanged (string text)
	{
		if (textChangedSound != null) {
			if (Time.time - lastSoundPlayTime > minSoundPlayInterval) {
				GlobalSoundPlayer.PlaySound (textChangedSound, volume);
				lastSoundPlayTime = Time.time;
			}
		}
	}
	
}
