using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("DES Actions")]
	[Tooltip("Checks if it should skip the game's intro animation.")]
	public class SkipIntroCheck : FsmStateAction
	{

		// Code that runs on entering the state.
		public override void OnEnter()
		{
			GameManager.Instance.SkipIntro();
			Finish();
		}


	}

}
