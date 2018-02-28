using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Randall {
	public class PlayerInput : MonoBehaviour {

		static public Vector2 GetMovement () {
			return new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		}

		static public bool LeftClickDown () {
			return Input.GetButtonDown ("Fire1");
		}

		static public bool RightClickDown () {
			return Input.GetButtonDown ("Fire2");
		}
		static public bool RightClick () {
			return Input.GetButton ("Fire2");
		}
		static public bool RightClickUp () {
			return Input.GetButtonUp ("Fire2");
		}
	}

	public class Utilities {
		public static bool CheckIfDoneMoving (Vector3 pos1, Vector3 pos2, float snapDistance) {
			if (Vector3.Distance (pos1, pos2) < snapDistance) {
				return true;
			} else {
				return false;
			}
		}
	}

	public class Timer{
		float _clock;
		public float maxTime;

		public void Update(float iteration)
		{
			_clock += iteration;
		}

		public bool IsGoingOff()
		{
			return _clock >= maxTime;
		}

		public void Reset()
		{
			_clock = 0;
		}
	}
}