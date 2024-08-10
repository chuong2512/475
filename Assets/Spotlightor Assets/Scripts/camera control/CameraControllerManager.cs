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
using System.Collections.Generic;

/// <summary>
/// Make sure that there is only 1 active CameraController at anytime for the same camera.
/// Also make the camera transition clean: Camera will never be deactivated by CameraController.
/// </summary>
public class CameraControllerManager : SingletonMonoBehaviour<CameraControllerManager>
{
	//private static CameraControllerManager _instance;
	
	private Dictionary<Camera, CameraControllerBase> _camControllerDict;

	protected Dictionary<Camera, CameraControllerBase> CameraControllerDict {
		get {
			if (_camControllerDict == null) {
				_camControllerDict = new Dictionary<Camera, CameraControllerBase> ();
			}
			return _camControllerDict;
		}
	}
	
	public void ChangeCameraController (Camera targetCamera, CameraControllerBase newController)
	{
		CameraControllerBase oldController;
		if (!CameraControllerDict.ContainsKey (targetCamera)) {
			oldController = null;
		} else
			oldController = CameraControllerDict [targetCamera];
		
		targetCamera.transform.parent = null;
		
		if (oldController != null) {
			oldController.gameObject.SetActive (false);
		}
		
		if (newController != null) {
			newController.target = targetCamera;
			newController.gameObject.SetActive (true);
			Debug.Log ("Change to new cam controller " + newController.gameObject.name);
		} else
			Debug.Log ("Camera controller is null now.");
		CameraControllerDict [targetCamera] = newController;
	}
	
	public CameraControllerBase GetActiveCameraController (Camera targetCamera)
	{
		if (CameraControllerDict.ContainsKey (targetCamera))
			return CameraControllerDict [targetCamera];
		return null;
	}
}
