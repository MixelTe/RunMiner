using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Crack : MonoBehaviour
{
	private float _stage;
	private Animator _animator;


	public void Enlarge(float step)
	{
		if (!_animator) _animator = GetComponent<Animator>();
		_stage += step;
		_animator.SetFloat("stage", _stage);
	}
}
