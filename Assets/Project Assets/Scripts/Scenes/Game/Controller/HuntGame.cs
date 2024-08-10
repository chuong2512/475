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

public class HuntGame : MonoBehaviour
{
	[System.Serializable]
	public class Rules
	{
		public Transform fixedFinishPoint;
		public Treasure treasurePrefab;
		public int roundsToWin = 0;
		public bool spawnPlayCopy = false;
	}

	private static ObjectInstanceFinder<HuntGame> instanceFinder = new ObjectInstanceFinder<HuntGame> ();

	public static HuntGame Instance{ get { return instanceFinder.Instance; } }

	public delegate void BasicEventHandler (HuntGame huntGame);

	public delegate void EndedEventHandler (HuntGame huntGame, bool win);

	public event BasicEventHandler Started;
	public event EndedEventHandler Ended;
	public event BasicEventHandler RoundStarted;
	public event BasicEventHandler RoundEnded;

	public Rules rules;

	private List<Transform> allFinishPoints;
	private float timeElapsed = 0;
	private int rounds = 0;

	public bool IsPlaying{ get; private set; }

	public bool LastGameWin{ get; private set; }

	public List<Transform> AllFinishPoints {
		get {
			if (allFinishPoints == null) {
				GameObject[] finishGos = GameObject.FindGameObjectsWithTag ("Finish");
				allFinishPoints = new List<Transform> ();
				foreach (GameObject go in finishGos)
					allFinishPoints.Add (go.transform);
			}
			return allFinishPoints;
		}
	}

	public Transform CurrentFinishPoint { get; private set; }

	public float TimeElapsed {
		get { return timeElapsed; }
	}

	void Start ()
	{
		Home.Instance.Entered += HandlePlayerEnterHome;
		Finish.Instance.Entered += HandlePlayerEnterFinish;
		Player.Instance.Life.Current.MinValueReached += HandlePlayerDead;
	}

	void HandlePlayerEnterFinish (PlayerTrigger trigger)
	{
		Home.Instance.gameObject.SetActive (true);
		Finish.Instance.gameObject.SetActive (false);
		Player.Instance.MotionRecorder.CurrentRecord.SetFinish ();
	}

	void HandlePlayerEnterHome (PlayerTrigger trigger)
	{
		if (rules.spawnPlayCopy)
			PlayerCopySpawner.Instance.Spawn (Player.Instance.MotionRecorder.CurrentRecord);

		if (rules.roundsToWin == 0) {
			EndGame (true);
		} else {
			if (RoundEnded != null)
				RoundEnded (this);

			StartNewRound ();
		}
	}

	void HandlePlayerDead (RangeInt source)
	{
		if (rules.roundsToWin > 0)
			EndGame (rounds >= rules.roundsToWin);
		else
			EndGame (false);
	}

	public void StartGame ()
	{
		rounds = 0;
		StartNewRound ();

		IsPlaying = true;

		if (Started != null)
			Started (this);
	}

	private void StartNewRound ()
	{
		rounds++;
		timeElapsed = 0;

		Home.Instance.gameObject.SetActive (false);

		if (rules.fixedFinishPoint != null)
			CurrentFinishPoint = rules.fixedFinishPoint;
		else
			CurrentFinishPoint = AllFinishPoints [Random.Range (0, AllFinishPoints.Count)];
		
		Finish.Instance.transform.CopyPositionRotation (CurrentFinishPoint);
		Finish.Instance.gameObject.SetActive (true);
		Finish.Instance.AddTreasure (rules.treasurePrefab);

		if (RoundStarted != null)
			RoundStarted (this);
	}

	void Update ()
	{
		if (IsPlaying)
			timeElapsed += Time.deltaTime;
	}

	public void EndGame (bool win)
	{
		IsPlaying = false;
		LastGameWin = win;

		Home.Instance.gameObject.SetActive (false);
		Finish.Instance.gameObject.SetActive (false);

		if (Ended != null)
			Ended (this, win);
	}

}
