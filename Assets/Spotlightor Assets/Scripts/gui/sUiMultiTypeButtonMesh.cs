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

[RequireComponent(typeof(sUiMultiTypeButton))]
public class sUiMultiTypeButtonMesh : MonoBehaviour
{
	public MeshFilter target;
	public Mesh[] meshOfButtonTypes;
	// Use this for initialization
	void Start ()
	{
		ChangeTargetMeshByButtonType (GetComponent<sUiMultiTypeButton> ());
		GetComponent<sUiMultiTypeButton> ().ButtonTypeChanged += HandleButtonTypeChanged;
	}

	void HandleButtonTypeChanged (sUiMultiTypeButton button)
	{
		ChangeTargetMeshByButtonType (button);
	}
	
	private void ChangeTargetMeshByButtonType (sUiMultiTypeButton button)
	{
		if (target != null && meshOfButtonTypes.Length > 0) {
			int meshIndex = Mathf.Clamp (button.ButtonType, 0, meshOfButtonTypes.Length);
			target.mesh = meshOfButtonTypes [meshIndex];
		}
	}
}
