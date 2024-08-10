/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
	[OverrideLabel ("最大生命值")]
	public int maxLife = 3;
	[Header ("绘制Debug生命值界面")]
	public bool drawDebugGui = false;

	public UnityEvent onDamage;
	public UnityEvent onHeal;
	public UnityEvent onDead;
	public bool destroyOnDead = false;

	private RangeInt current;
	private float lastLifeChangeTime = -100;

	public RangeInt Current {
		get {
			if (current == null)
				current = new RangeInt (0, maxLife, maxLife);
			return current;
		}
	}

	void Start ()
	{
		Current.MinValueReached += HandleMinValueReached;
		Current.ValueChanged += HandleValueChanged;
	}

	void HandleValueChanged (RangeInt source, int amount)
	{
		if (amount < 0)
			onDamage.Invoke ();
		else
			onHeal.Invoke ();

		lastLifeChangeTime = Time.time;
	}

	void HandleMinValueReached (RangeInt source)
	{
		onDead.Invoke ();
		if (destroyOnDead)
			Destroy (gameObject);
	}

	protected virtual void OnGUI ()
	{
		if (drawDebugGui && Time.time - lastLifeChangeTime < 3) {
			Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);
			GUI.color = Color.black;
			GUI.Box (new Rect (screenPos.x, Screen.height - screenPos.y, 40, 20), string.Format ("{0}/{1}", Current.Value, maxLife));
		}
	}
}
