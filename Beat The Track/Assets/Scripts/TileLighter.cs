using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileLighter : AudioSyncer
{
    public Tilemap tilemap;
    public Color color;

    public Color[] beatColors;
    public Color restColor;

    private Color tileColor;
    public Color targetColor;

    private int m_randomIndx;
    //private Image m_img;

    //private BoundsInt bounds = tilemap.cellBounds;
    //private TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

    public Color baseColor;
    // Start is called before the first frame update
    void Start()
    {
        /*bounds = tilemap.cellBounds;
        allTiles = tilemap.GetTilesBlock(bounds);*/
        tilemap.SetTileFlags(new Vector3Int(1, 1, 0), TileFlags.None);
        tilemap.SetColor(new Vector3Int(1, 1, 0), baseColor);
        tileColor = tilemap.GetColor(new Vector3Int(1, 1, 0));
        //m_img = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {

            Debug.Log("déjà ça marche jusque là");

            tilemap.SetColor(new Vector3Int(0, -12, 0), color);
            tilemap.SetTileFlags(new Vector3Int(0, -13, 0), TileFlags.None);
            tilemap.SetColor(new Vector3Int(0, -13, 0), color);
        }
    }
    private IEnumerator MoveToColor(Color _target)
    {
        Color _curr = tileColor;
        Color _initial = _curr;
        float _timer = 0;

        while (_curr != _target)
        {
            Debug.Log("testtt");
            _curr = Color.Lerp(_initial, _target, _timer / timeToBeat);
            _timer += Time.deltaTime;

            tileColor = _curr;
            tilemap.SetTileFlags(new Vector3Int(1, 1, 0), TileFlags.None);
            tilemap.SetColor(new Vector3Int(1, 1, 0), tileColor);
            //m_img.color = _curr;

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

        tileColor = Color.Lerp(tileColor, restColor, restSmoothTime * Time.deltaTime);
        tilemap.SetColor(new Vector3Int(1, 1, 0), tileColor);
        //m_img.color = Color.Lerp(m_img.color, restColor, restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        Color _c = RandomColor();

        Debug.Log("1212");
        StopCoroutine("MoveToColor");
        StartCoroutine("MoveToColor", targetColor);
    }

}
