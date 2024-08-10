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

public class UVScaler : MonoBehaviour
{

	public bool scaleSharedMaterial = false;
	public Vector2 scaleRange = new Vector2 (0.5f, 1.5f);
	public float scaleTime = 1f;
	public bool ignoreTimeScale = false;
	public int materialIndex = 0;
	public string propertyName = "_MainTex";
	private Vector2 originalScale;

	public Material TargetMaterial {
		get {
			if (scaleSharedMaterial) {
				return GetComponent<Renderer>().sharedMaterials [materialIndex];
			} else {
				return GetComponent<Renderer>().materials [materialIndex];
			} 
		}
	}
	
	protected virtual void Start ()
	{
		originalScale = TargetMaterial.GetTextureScale (propertyName);
	}
	
	void Update ()
	{
		ScaleMaterial ();
	}
	
	protected virtual void ScaleMaterial ()
	{
		float numPeriodsSinceStart = ignoreTimeScale ? Time.timeSinceLevelLoad / scaleTime : Time.time / scaleTime;
		float sinValue = Mathf.Sin (numPeriodsSinceStart * Mathf.PI);
		float t = Mathf.InverseLerp (-1, 1, sinValue);
		TargetMaterial.SetTextureScale (propertyName, originalScale * Mathf.Lerp (scaleRange.x, scaleRange.y, t));
	}
	
	protected virtual void OnApplicationQuit ()
	{
		if (scaleSharedMaterial)
			TargetMaterial.SetTextureScale (propertyName, originalScale);
	}
	
}
