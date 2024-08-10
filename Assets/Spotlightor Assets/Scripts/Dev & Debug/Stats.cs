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

public class Stats : MonoBehaviour
{
	private const float UpdateInterval = 1f;
	public KeyCode visibilitySwitchKey = KeyCode.F;
	private float fps = 0;
	private int framesCounter = 0;
	private float lastUpdateTime;
	private bool isVisible = true;
	// Use this for initialization
	void Start ()
	{
		lastUpdateTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (ControlFreak2.CF2Input.GetKeyDown (visibilitySwitchKey))
			isVisible = !isVisible;
		
		framesCounter ++;
		float timeElpased = Time.time - lastUpdateTime;
		if (timeElpased >= UpdateInterval) {
			fps = (float)framesCounter / timeElpased;
			lastUpdateTime = Time.time;
			framesCounter = 0;
		}
	}
	
	void OnGUI ()
	{
		if (isVisible)
			GUI.Box (new Rect (Screen.width - 100, 0, 100, 30), string.Format ("FPS:{0:00.0}", fps));
	}
}
