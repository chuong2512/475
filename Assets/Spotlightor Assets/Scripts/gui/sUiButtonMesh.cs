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
public class sUiButtonMesh : FunctionalMonoBehaviour
{
	public MeshFilter target;
	public Mesh overMesh;
	public Mesh downMesh;
	public Mesh disableMesh;
	private Mesh normalMesh;
	private sUiButton button;
	
	public Mesh NormalMesh {
		get {
			if (normalMesh == null)
				normalMesh = target.sharedMesh;
			return normalMesh;
		}
	}
	
	private sUiButton Button {
		get {
			if (button == null)
				button = GetComponent<sUiButton> ();
			return button;
		}
	}
	
	void Awake ()
	{
		normalMesh = target.sharedMesh;
	}

	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		Button.StateChanged += HandleButtonStateChanged;
		ChangeMeshByButtonState ();
	}

	protected override void OnBecameUnFunctional ()
	{
		Button.StateChanged -= HandleButtonStateChanged;
	}
	
	void HandleButtonStateChanged (sUiButton button, sUiButton.StateTypes stateBefore, sUiButton.StateTypes stateNow)
	{
		ChangeMeshByButtonState ();
	}

	private void ChangeMeshByButtonState ()
	{
		switch (Button.StateType) {
		case sUiButton.StateTypes.Normal:
			ChangeMesh (normalMesh);
			break;
		case sUiButton.StateTypes.Over:
			ChangeMesh (overMesh);
			break;
		case sUiButton.StateTypes.Down:
			ChangeMesh (downMesh);
			break;
		case sUiButton.StateTypes.Disable:
			ChangeMesh (disableMesh);
			break;
		}
	}

	private void ChangeMesh (Mesh newMesh)
	{
		target.mesh = newMesh != null ? newMesh : NormalMesh;
	}
}
