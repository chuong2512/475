/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(UiMultiStates))]
public class UiMultiStatesSwitchSprite : MonoBehaviour
{
	public Image image;
	public List<Sprite> spriteByStateIds;

	void Start ()
	{
		UpdateObjectActivation (GetComponent<UiMultiStates> ());
		
		GetComponent<UiMultiStates> ().StateChanged += HandleStateChanged;
	}
	
	void HandleStateChanged (UiMultiStates multiStates)
	{
		UpdateObjectActivation (multiStates);
	}
	
	private void UpdateObjectActivation (UiMultiStates multiStates)
	{
		image.sprite = spriteByStateIds [Mathf.Clamp (multiStates.StateId, 0, spriteByStateIds.Count)];
	}
}
