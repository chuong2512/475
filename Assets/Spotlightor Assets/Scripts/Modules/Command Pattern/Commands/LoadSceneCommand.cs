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
using UnityEngine.SceneManagement;

public class LoadSceneCommand : CoroutineCommandBehavior
{
	public bool loadNextScene = false;
	[HideByBooleanProperty("loadNextScene", true)]
	public string sceneName = "Home";
	public bool asyncLoad = false;
	public bool allowSceneActivation = false;
	public ProgressBar asyncProgressBar;

	private AsyncOperation loadOperation;

	protected override IEnumerator CoroutineCommand ()
	{
		if (asyncLoad) {
			if (!loadNextScene)
				loadOperation = SceneManager.LoadSceneAsync (sceneName);
			else
				loadOperation = SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex + 1);
			loadOperation.allowSceneActivation = allowSceneActivation;
			float completeProgress = allowSceneActivation ? 1 : 0.9f;
			do {
				if (asyncProgressBar != null)
					asyncProgressBar.TweenProgressTo (loadOperation.progress / completeProgress);
				yield return null;
			} while(loadOperation.progress < completeProgress);
			asyncProgressBar.TweenProgressTo (1);
		} else {
			if (!loadNextScene)
				SceneManager.LoadScene (sceneName);
			else
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}

	public void ActivateAsyncLoadedScene ()
	{
		if (loadOperation != null)
			loadOperation.allowSceneActivation = true;
	}
}
