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
[RequireComponent(typeof(TextMesh))]
public class ResizeTextByTextLength : FunctionalMonoBehaviour
{
	[System.Serializable]
	public class CharacterSizeSetting
	{
		public int textLength = 1;
		public float characterSize = 0.01f;
		public int fontSize = 0;
	}
	public CharacterSizeSetting[] sizesDecreaseByLength;
	#region implemented abstract members of FunctionalMonoBehaviour
	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		GetComponent<TextDisplayer> ().TextUpdated += HandleTextUpdated;
		HandleTextUpdated (GetComponent<TextDisplayer> ().Text);
	}

	protected override void OnBecameUnFunctional ()
	{
		GetComponent<TextDisplayer> ().TextUpdated -= HandleTextUpdated;
	}
	#endregion
	
	void HandleTextUpdated (string text)
	{
		if (sizesDecreaseByLength.Length > 0) {
			CharacterSizeSetting selectedSizeSetting = null;
			for (int i = 0; i < sizesDecreaseByLength.Length; i++) {
				CharacterSizeSetting setting = sizesDecreaseByLength [i];
				if (text.Length <= setting.textLength) {
					selectedSizeSetting = setting;
					break;
				}
			}
			if (selectedSizeSetting == null)
				selectedSizeSetting = sizesDecreaseByLength [sizesDecreaseByLength.Length - 1];
			TextMesh tm = GetComponent<TextMesh> ();
			tm.characterSize = selectedSizeSetting.characterSize;
			tm.fontSize = selectedSizeSetting.fontSize;
		}
	}
}
