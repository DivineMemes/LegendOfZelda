using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Randall {
	public class PlayerInput : MonoBehaviour {

		static public Vector2 GetMovement () {
			return new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		}

		static public bool LeftClickDown()
		{
			//
			return Input.GetButtonDown("Fire1");
		}

		static public bool RightClickDown()
		{
			//
			return Input.GetButtonDown("Fire2");
		}
	}
}