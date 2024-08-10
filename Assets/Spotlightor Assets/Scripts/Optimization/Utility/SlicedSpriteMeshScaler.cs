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

public class SlicedSpriteMeshScaler : MonoBehaviour
{
	private Vector3 defaultScale;
	private Vector3[] defaultVertices;
	private bool defaultValuesSaved = false;

	public void ScaleTo (Vector3 newScale)
	{
		if (!defaultValuesSaved)
			SaveDefaultValues ();

		Vector3 scaleRelativeToDefault = newScale;
		scaleRelativeToDefault.x /= defaultScale.x;
		scaleRelativeToDefault.y /= defaultScale.y;
		scaleRelativeToDefault.z /= defaultScale.z;

		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		
		Vector3[] updatedVertices = mesh.vertices;
		for (int i = 0; i < updatedVertices.Length; i++) {
			updatedVertices [i].x = defaultVertices [i].x;
			updatedVertices [i].y = defaultVertices [i].y;
			updatedVertices [i].z = defaultVertices [i].z;
		}

		for (int i = 1; i <= 13; i+= 4)
			updatedVertices [i].x = updatedVertices [i - 1].x + (defaultVertices [i].x - defaultVertices [i - 1].x) / scaleRelativeToDefault.x;
		
		for (int i = 2; i <= 14; i+= 4)
			updatedVertices [i].x = updatedVertices [i + 1].x + (defaultVertices [i].x - defaultVertices [i + 1].x) / scaleRelativeToDefault.x;
		
		for (int i = 4; i <= 7; i++)
			updatedVertices [i].y = updatedVertices [i - 4].y + (defaultVertices [i].y - defaultVertices [i - 4].y) / scaleRelativeToDefault.y;
		
		for (int i = 8; i <= 11; i++)
			updatedVertices [i].y = updatedVertices [i + 4].y + (defaultVertices [i].y - defaultVertices [i + 4].y) / scaleRelativeToDefault.y;

		mesh.vertices = updatedVertices;

		transform.localScale = newScale;
	}

	private void SaveDefaultValues ()
	{
		defaultScale = transform.localScale;
		if (defaultScale.x == 0)
			defaultScale.x = 0.01f;
		if (defaultScale.y == 0)
			defaultScale.y = 0.01f;
		if (defaultScale.z == 0)
			defaultScale.z = 0.01f;

		defaultVertices = GetComponent<MeshFilter> ().mesh.vertices;

		defaultValuesSaved = true;
	}
}
