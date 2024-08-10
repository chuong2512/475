/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;

public static class SaveAssetUtility
{
	public static Texture2D SaveTexture (Texture2D texureToSave, string textureAssetPath)
	{
		CreateFolderForAssetIfNeeded (textureAssetPath);
		
		Texture2D tempTexture = new Texture2D (texureToSave.width, texureToSave.height, TextureFormat.ARGB32, false);
		tempTexture.SetPixels32 (texureToSave.GetPixels32 ());
		
		byte[] texBytes = tempTexture.EncodeToPNG ();
		
		string fileName = Application.dataPath.Replace ("Assets", "") + textureAssetPath;
		FileStream fileStream = new FileStream (fileName, FileMode.OpenOrCreate, FileAccess.Write);
		BinaryWriter b = new BinaryWriter (fileStream);
		for (int i = 0; i < texBytes.Length; i++)
			b.Write (texBytes [i]);
		b.Close ();
		
		AssetDatabase.Refresh ();
		
		return AssetDatabase.LoadAssetAtPath (textureAssetPath, typeof(Texture2D)) as Texture2D;
	}
	
	public static Mesh SaveMesh (Mesh mesh, string meshAssetPath)
	{
		CreateFolderForAssetIfNeeded (meshAssetPath);
		
		Mesh meshAsset = AssetDatabase.LoadAssetAtPath (meshAssetPath, typeof(Mesh)) as Mesh;
		if (meshAsset == null) {
			meshAsset = mesh;
			AssetDatabase.CreateAsset (meshAsset, meshAssetPath);

			Debug.Log(string.Format("Mesh asset created at: {0}", meshAssetPath));
		} else {
			meshAsset.Clear();

			meshAsset.vertices = mesh.vertices;
			meshAsset.triangles = mesh.triangles;
			meshAsset.uv = mesh.uv;
			meshAsset.uv2 = mesh.uv2;
			meshAsset.normals = mesh.normals;
			meshAsset.tangents = mesh.tangents;
			meshAsset.colors32 = mesh.colors32;
			
			meshAsset.RecalculateBounds ();
			
			EditorUtility.SetDirty (meshAsset);

			Debug.Log(string.Format("Mesh asset updated at: {0}", meshAssetPath));
		}
		return meshAsset;
	}
	
	public static void CreateFolderForAssetIfNeeded (string assetPath)
	{
		string folderPath = assetPath.Substring (0, assetPath.LastIndexOf ("/"));
		if (AssetDatabase.LoadAssetAtPath (folderPath, typeof(Object)) == null) {
			string folderParentPath = folderPath.Substring (0, folderPath.LastIndexOf ("/"));
			string folderName = folderPath.Substring (folderPath.LastIndexOf ("/") + 1);
			AssetDatabase.CreateFolder (folderParentPath, folderName);
		}
	}
}
