/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TextureAtlasMeshGeneartor))]
public class TextureAtlasMeshGeneartorEditor : Editor
{
	private const string MeshFolderName = "Atlas Meshes";
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		if (GUILayout.Button ("Generate Meshes")) {
			TextureAtlasMeshGeneartor meshGenerator = target as TextureAtlasMeshGeneartor;
			
			GenerateMeshes (meshGenerator);
		}
		if (GUILayout.Button ("Generate Meshes & Pack Atlas")) {
			TextureAtlasMeshGeneartor meshGenerator = target as TextureAtlasMeshGeneartor;
			
			TextureAtlasEditor.Pack (meshGenerator.atlas);
			
			GenerateMeshes (meshGenerator);
		}
	}
	
	public static void GenerateMeshes (TextureAtlasMeshGeneartor meshGenerator)
	{
		string path = AssetDatabase.GetAssetPath (meshGenerator);
		string currentFolderPath = path.Substring (0, path.LastIndexOf ("/"));
		string meshFolderPath = currentFolderPath + "/" + MeshFolderName;
			
		Mesh[] generatedMeshes = meshGenerator.GenerateAllMeshes ();
		foreach (Mesh mesh in generatedMeshes)
			SaveAssetUtility.SaveMesh (mesh, string.Format ("{0}/{1}.asset", meshFolderPath, mesh.name));
	}
}
