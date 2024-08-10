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

// Translate multi-touch event to mouse event.
[RequireComponent(typeof(Camera))]
public class TouchToMouseEventTranslator : MonoBehaviour
{
	private Dictionary<int, GameObject> touchDownGameObjectByFingerIds = new Dictionary<int, GameObject> ();
	private Dictionary<int, GameObject> touchOverGameObjectByFingerIds = new Dictionary<int, GameObject> ();

	void Start ()
	{
		ControlFreak2.CF2Input.simulateMouseWithTouches = false;
	}
	
	void Update ()
	{
		RaycastHit hit = new RaycastHit ();

		foreach (ControlFreak2.InputRig.Touch touch in ControlFreak2.CF2Input.touches) {
			int fingerId = touch.fingerId;
			TouchPhase phase = touch.phase;
			GameObject touchDownGo = GetTouchedGoByFingerId (fingerId);
			GameObject touchOverGo = null;
			touchOverGameObjectByFingerIds.TryGetValue (fingerId, out touchOverGo);
			
			Ray ray = GetComponent<Camera>().ScreenPointToRay (touch.position);
			if (Physics.Raycast (ray, out hit)) {
				GameObject hitGo = hit.transform.gameObject;
				
				if (phase.Equals (TouchPhase.Began)) {
					AssignTouchDownGameObject (hitGo, fingerId);
					touchOverGameObjectByFingerIds [fingerId] = hitGo;

					SimulateMouseMessages (hitGo, "OnMouseDown");
					SimulateMouseMessages (hitGo, "OnTouchEnter");
					this.Log ("Down, Enter {0}", hitGo.name);
				} else if (phase.Equals (TouchPhase.Ended) || phase.Equals (TouchPhase.Canceled)) {
					// Down & up at same gameObject
					if (hitGo == touchDownGo) {
						SimulateMouseMessages (touchDownGo, "OnMouseUpAsButton");
						this.Log ("UpAsButton {0}", touchDownGo.name);
					}

					// No mater if down & up at same gameObject, up will always be called for mouse down gameObject
					if (touchDownGo != null) {
						SimulateMouseMessages (touchDownGo, "OnMouseUp");
						RemoveTouchDownGameObject (fingerId);
						this.Log ("Up {0}", touchDownGo.name);
					}

					SimulateMouseMessages (hitGo, "OnTouchExit");
					touchOverGameObjectByFingerIds [fingerId] = null;
					this.Log ("Exit {0}", hitGo.name);
				} else {
					if (hitGo != touchOverGo) {
						if (touchOverGo != null) {
							SimulateMouseMessages (touchOverGo, "OnTouchExit");
							this.Log ("Exit {0}", touchOverGo);
						}

						SimulateMouseMessages (hitGo, "OnTouchEnter");
						touchOverGameObjectByFingerIds [fingerId] = hitGo;
						this.Log ("Enter {0}", hitGo.name);
					} 
				}
			} else {
				if (touchOverGo != null) {
					touchOverGameObjectByFingerIds [fingerId] = null;
					SimulateMouseMessages (touchOverGo, "OnTouchExit");
					this.Log ("Exit {0}", touchOverGo.name);
				}
				if (phase.Equals (TouchPhase.Ended) || phase.Equals (TouchPhase.Canceled)) {
					if (touchDownGo != null) {
						SimulateMouseMessages (touchDownGo, "OnMouseUp");
						RemoveTouchDownGameObject (fingerId);
						this.Log ("Up {0}", touchDownGo.name);
					}
				}
			}
		}
	}
	
	void AssignTouchDownGameObject (GameObject go, int touchID)
	{
		if (go != null) 
			touchDownGameObjectByFingerIds [touchID] = go;
	}
	
	void RemoveTouchDownGameObject (int touchId)
	{	
		touchDownGameObjectByFingerIds [touchId] = null;
	}
	
	GameObject GetTouchedGoByFingerId (int touchId)
	{
		GameObject touchedGo = null;
		touchDownGameObjectByFingerIds.TryGetValue (touchId, out touchedGo);
		return touchedGo;
	}

	private void SimulateMouseMessages (GameObject target, string message)
	{
		target.SendMessage (message, SendMessageOptions.DontRequireReceiver);
	}
}