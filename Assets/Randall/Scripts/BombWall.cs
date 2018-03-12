using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWall : MonoBehaviour, IBombable{
	public SpriteRenderer spriteRenderer;
	public Collider2D c2D;
	public void Bombed()
	{
		spriteRenderer.sprite = null;
		c2D.isTrigger = true;
	}
}
