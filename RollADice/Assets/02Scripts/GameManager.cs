using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
    [SerializeField] private List<TileInfo> _tiles;
    private List<TileInfoStar> _starTiles = new List<TileInfoStar>();
    private int _starScore;
    private int starScore
    {
        set
        {
            _starScore = value;
            _starScoreText.text = _starScore.ToString();
        }
    }
    [SerializeField] private Text _starScoreText;
    private int _diceNum;
    private int _goldenDiceNum;
    [SerializeField] private int _tilesCount;
    private int _current;

    private void Awake()
    {
        instance = this;
        _diceNum = Constants.DICE_NUM_INIT;
        _goldenDiceNum = Constants.GOLDEN_DICE_NUM_INIT;
        _tilesCount = _tiles.Count;
        _tiles.Sort();
        //_tiles.OrderBy(x => x.index);

        foreach (var tile in _tiles)
        {
            // is ������
            // ĳ��Ʈ ���� ��� ��ȯ�ϴ� ������
            // ĳ���� �����ϸ� true, �����ϸ� false
            //if(tile is TileInfoStar)
            //{
            //    _starTiles.Add((TileInfoStar)tile);
            //}

            // as ������ ĳ���ÿ�����
            // ����ȯ ���н� null ��ȯ
            TileInfoStar tmp = tile as TileInfoStar;
            if (tmp != null)
                _starTiles.Add(tmp);
            else
                throw new System.Exception("����ĭ ĳ���� ����");
        }
    }

    public void RollADice()
    {
        if(_diceNum > 0)
        {
            int randomValue = Random.Range(1, 7);
            MovePlayer(randomValue);
        }
    }

    private void MovePlayer(int diceValue)
    {
        _current += diceValue;
        if (_current >= _tilesCount)
            _current -= _tilesCount;

        Player.instance.MoveTo(_tiles[_current].transform);
        _tiles[_current].OnTile();
    }
}