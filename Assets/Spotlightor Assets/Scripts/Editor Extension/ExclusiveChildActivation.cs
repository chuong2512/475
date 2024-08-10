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

[ExecuteInEditMode ()]
public class ExclusiveChildActivation : MonoBehaviour
{
	public bool editModeOnly = false;
	[HideByBooleanProperty ("editModeOnly", true)]
	public bool activeChildrenOnDisable = false;
	private Transform lastActiveChild = null;

	void Update ()
	{
		if (!editModeOnly || !Application.isPlaying) {
			if (enabled) {
				if (lastActiveChild == null) {
					for (int i = 0; i < transform.childCount; i++) {
						Transform child = transform.GetChild (i);
						if (child.gameObject.activeSelf) {
							lastActiveChild = child;
							break;
						}
					}
				}

				for (int i = 0; i < transform.childCount; i++) {
					Transform child = transform.GetChild (i);
					if (child != lastActiveChild && child.gameObject.activeSelf) {
						lastActiveChild.gameObject.SetActive (false);
						lastActiveChild = child;
					}
				}
			}
		}
	}

	void OnDisable ()
	{
		if (!editModeOnly || !Application.isPlaying) {
			if (activeChildrenOnDisable) {
				for (int i = 0; i < transform.childCount; i++)
					transform.GetChild (i).gameObject.SetActive (true);
			}
		}
	}

	[ContextMenu ("Randomize Active Child")]
	void RandomizeActiveChild ()
	{
		int randomActiveChildIndex = Random.Range (0, transform.childCount);
		for (int i = 0; i < transform.childCount; i++)
			transform.GetChild (i).gameObject.SetActive (i == randomActiveChildIndex);

		lastActiveChild = transform.GetChild (randomActiveChildIndex);
	}
}
