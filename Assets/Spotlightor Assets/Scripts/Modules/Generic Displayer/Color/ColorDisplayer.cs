/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorDisplayer : MonoBehaviour
{
	public bool tintSharedMaterial = false;
	public string materialPropertyName = "_Color";

	private delegate void DisplayColorDelegate (Color color);

	private Mesh meshInstance;

	private DisplayColorDelegate displayColor;

	private DisplayColorDelegate DisplayColor {
		get {
			if (displayColor == null) {
				if (GetComponent<Graphic> () != null)
					displayColor = UpdateGraphicColor;
				else if (GetComponent<Text> () != null)
					displayColor = UpdateTextColor;
				else if (GetComponent<SpriteRenderer> () != null)
					displayColor = UpdateSpriteRendererColor;
				else if (GetComponent<TextMesh> () != null)
					displayColor = UpdateTextMeshColor;
				else if (GetComponent<Renderer> () != null && GetComponent<Renderer> ().sharedMaterial.HasProperty (materialPropertyName))
					displayColor = UpdateRendererColor;
				else if (GetComponent<MeshFilter> () != null) {
					displayColor = UpdateMeshVertexColor;
					Mesh sharedMesh = GetComponent<MeshFilter> ().sharedMesh;
					meshInstance = GetComponent<MeshFilter> ().mesh;
					meshInstance.Clear ();
					meshInstance.vertices = sharedMesh.vertices;
					meshInstance.normals = sharedMesh.normals;
					meshInstance.triangles = sharedMesh.triangles;
					meshInstance.tangents = sharedMesh.tangents;
					meshInstance.colors32 = sharedMesh.colors32;
					meshInstance.uv = sharedMesh.uv;
					meshInstance.uv2 = sharedMesh.uv2;

					GetComponent<MeshFilter> ().mesh = meshInstance;

					meshInstance.UploadMeshData (false);
				} else if (GetComponent<Light> () != null)
					displayColor = UpdateLightColor;
				else if (GetComponent<ParticleSystem> () != null)
					displayColor = UpdateParticleSystemColor;
				else if (GetComponent<LensFlare> () != null)
					displayColor = UpdateLensFlareColor;
				else
					this.Log ("Cannot find color update target.");
			}
			return displayColor;
		}
	}

	private void UpdateRendererColor (Color color)
	{
		Material material = tintSharedMaterial ? GetComponent<Renderer> ().sharedMaterial : GetComponent<Renderer> ().material;
		material.SetColor (materialPropertyName, color);
	}

	private void UpdateGraphicColor (Color color)
	{
		GetComponent<Graphic> ().color = color;
	}

	private void UpdateSpriteRendererColor (Color color)
	{
		GetComponent<SpriteRenderer> ().color = color;
	}

	private void UpdateTextColor (Color color)
	{
		GetComponent<Text> ().color = color;
	}

	private void UpdateTextMeshColor (Color color)
	{
		GetComponent<TextMesh> ().color = color;
	}

	private void UpdateMeshVertexColor (Color color)
	{
		MeshUtility.Tint (meshInstance, color);
	}

	private void UpdateLightColor (Color color)
	{
		GetComponent<Light> ().color = color;
	}

	private void UpdateParticleSystemColor (Color color)
	{
		ParticleSystem.MainModule main = GetComponent<ParticleSystem> ().main;
		ParticleSystem.MinMaxGradient startColor = main.startColor;
		startColor.color = color;
		main.startColor = startColor;
	}

	private void UpdateLensFlareColor (Color color)
	{
		GetComponent<LensFlare> ().color = color;
	}


	public void Display (Color color)
	{
		DisplayColor (color);
	}

	#if UNITY_EDITOR
	[ContextMenu ("Show Display Color Method")]
	private void LogDisplayColorDelegate ()
	{
		this.Log (DisplayColor.Method.Name);
	}
	#endif
}
