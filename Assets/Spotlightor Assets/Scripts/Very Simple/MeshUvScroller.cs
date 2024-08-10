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

[RequireComponent(typeof(MeshFilter))]
public class MeshUvScroller : MonoBehaviour
{
	public Vector2 speed;
	public bool ignoreTimeScale = false;
	public bool scrollSharedMesh = false;
	private Vector2[] defaultUvs;
	private Vector2[] uvs;
	private Vector2 uvTotalOffset;
	private Mesh targetMesh;
	
	// Use this for initialization
	void Start ()
	{
		if (scrollSharedMesh) {
			targetMesh = GetComponent<MeshFilter> ().sharedMesh;
			defaultUvs = targetMesh.uv;
		} else
			targetMesh = GetComponent<MeshFilter> ().mesh;
		uvs = targetMesh.uv;
		uvTotalOffset = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update ()
	{
		ScrollMeshUv (targetMesh);
	}
	
	protected virtual void ScrollMeshUv (Mesh mesh)
	{
		float deltaTime = ignoreTimeScale ? Time.deltaTime / Time.timeScale : Time.deltaTime;
		Vector2 deltaUv = deltaTime * speed;
		uvTotalOffset += deltaUv;
		if (Mathf.Abs (uvTotalOffset.x) > 1) {
			float loopX = Mathf.Floor (uvTotalOffset.x);
			deltaUv.x -= loopX;
			uvTotalOffset.x -= loopX;
		}
		if (Mathf.Abs (uvTotalOffset.y) > 1) {
			float loopY = Mathf.Floor (uvTotalOffset.y);
			deltaUv.y -= loopY;
			uvTotalOffset.y -= loopY;
		}
		
		for (int i = 0; i < uvs.Length; i++) 
			uvs [i] += deltaUv;
		
		targetMesh.uv = uvs;
	}
	
	protected virtual void OnApplicationQuit ()
	{
		if (scrollSharedMesh)
			targetMesh.uv = defaultUvs;
	}
}
