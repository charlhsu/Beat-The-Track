using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileBlink : AudioSyncer
{

	private IEnumerator MoveToColor(Color _target, Vector3Int pos)
	{
		
		Color _curr = tilemap.GetColor(pos);
		Color _initial = _curr;
		float _timer = 0;
		

		while (_curr != _target)
		{
			
			_curr = Color.Lerp(_initial, _target, _timer / timeToBeat);
			_timer += Time.deltaTime;
			
			tilemap.SetColor(pos, _curr);

			yield return null;
		}

		m_isBeat = false;
	}

	private Color RandomColor()
	{
		if (beatColors == null || beatColors.Length == 0) return Color.white;
		m_randomIndx = Random.Range(0, beatColors.Length);
		return beatColors[m_randomIndx];
	}

	public override void OnUpdate()
	{
		base.OnUpdate();

		if (m_isBeat) return;
		foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
		{
			colorLerp = Color.Lerp(tilemap.GetColor(tilePosition), restColor, restSmoothTime * Time.deltaTime);
			tilemap.SetColor(tilePosition, colorLerp);
		}
	}

	public override void OnBeat()
	{
		base.OnBeat();

		foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
		{
			Color _c = RandomColor();
		
			StopCoroutine("MoveToColor");
			StartCoroutine(MoveToColor( _c, tilePosition));
		}
		
	}

	/*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
			
			tilemap.SetColor(new Vector3Int(2, 1, 0), Color.blue);
			Debug.Log("ArgluaArglu");
        }
    }*/
	
	private void Start()
	{
		/*tilemap.SetTileFlags(pos, TileFlags.None);
		tilemap.SetColor(pos, restColor);
		pos = new Vector3Int(1, 1, 0);*/
		
	}

	public Color[] beatColors;
	public Color restColor;
	public Tilemap tilemap;
	//private Vector3Int pos;
	private Color colorLerp;
	private BoundsInt bounds;
	private TileBase[] allTiles;

	private int m_randomIndx;
	
}