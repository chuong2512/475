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

[RequireComponent(typeof(Renderer))]
public class SpritePlayer : MonoBehaviour
{
	public bool playOnAwake = true;
	public float speed = 1;
	public bool pingPone = false;
	public int tilesX = 1;
	public int tilesY = 1;
	private int currentImageIndex = 0;
	private bool isPlayingForward = true;
	
	public int CurrentImageIndex {
		get { return currentImageIndex;}
		set {
			int validIndex = value %= TilesCount;
			if (validIndex < 0)
				validIndex += TilesCount;
			
			this.currentImageIndex = validIndex;
			int uIndex = validIndex % tilesX;
			int vIndex = validIndex / tilesX;
			GetComponent<Renderer>().material.mainTextureOffset = new Vector2 ((float)uIndex * GetComponent<Renderer>().material.mainTextureScale.x, (float)vIndex * GetComponent<Renderer>().material.mainTextureScale.y);
			GetComponent<Renderer>().material.mainTextureScale = new Vector2 (1f / tilesX, 1f / tilesY);
		}
	}
	
	public int TilesCount { get { return Mathf.Max (1, tilesX * tilesY); } }

	void Start ()
	{
		CurrentImageIndex = 0;
	}

	void OnEnable ()
	{
		if (playOnAwake)
			Play ();
	}

	void OnDisable ()
	{
		Stop ();
	}
	
	public void Play ()
	{
		StartCoroutine ("PlaySpriteSheet");
	}
	
	public void Stop ()
	{
		StopCoroutine ("PlaySpriteSheet");
	}
	
	IEnumerator PlaySpriteSheet ()
	{
		while (true) {
			yield return new WaitForSeconds (1f / (TilesCount * speed));

			if (!pingPone)
				isPlayingForward = true;
			else {
				if (CurrentImageIndex >= TilesCount - 1 && isPlayingForward)
					isPlayingForward = false;
				else if (CurrentImageIndex <= 0 && !isPlayingForward)
					isPlayingForward = true;
			}

			CurrentImageIndex += isPlayingForward ? 1 : -1;
		}
	}
}
