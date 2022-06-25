using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncCamScale : AudioSyncer
{
	private IEnumerator MoveToScale(float _target)
	{
		float _curr = CameraController.instance.mainCamera.orthographicSize;
		float _initial = _curr;
		float _timer = 0;

		while (_curr != _target)
		{
			_curr = Mathf.Lerp(_initial, _target, _timer / timeToBeat);
			_timer += Time.deltaTime;

			CameraController.instance.mainCamera.orthographicSize = _curr;

			yield return null;
		}

		m_isBeat = false;
	}

	public override void OnUpdate()
	{
		base.OnUpdate();

		if (m_isBeat) return;


		CameraController.instance.mainCamera.orthographicSize = Mathf.Lerp(CameraController.instance.mainCamera.orthographicSize, restSize, restSmoothTime * Time.deltaTime);
	}

	public override void OnBeat()
	{
		base.OnBeat();

		StopCoroutine("MoveToScale");
		StartCoroutine("MoveToScale", beatSize);
	}

	public float beatSize;
	public float restSize;
}
