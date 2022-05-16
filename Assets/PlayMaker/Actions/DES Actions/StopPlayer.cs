using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("DES Actions")]
	[Tooltip("Prevents all player movement")]
	public class StopPlayer : FsmStateAction
	{

		// Code that runs on entering the state.
		public override void OnEnter()
		{
			GameManager.Instance.StopPlayer();
			Finish();
		}


	}

}
