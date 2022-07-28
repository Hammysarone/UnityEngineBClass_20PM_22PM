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
        get
        {
            return _starScore;
        }
    }
    [SerializeField] private Text _starScoreText;
    private int _diceNum;
    private int diceNum
    {
        get
        {
            return _diceNum;
        }
        set
        {
            _diceNum = value;
            _diceNumText.text = _diceNum.ToString();
        }
    }
    [SerializeField] private Text _diceNumText;
    private int _goldenDiceNum;
    private int goldenDiceNum
    {
        get
        {
            return _goldenDiceNum;
        }
        set
        {
            _goldenDiceNum = value;
            _goldenDiceNumText.text = _goldenDiceNum.ToString();
        }
    }
    [SerializeField] private Text _goldenDiceNumText;
    private int _tilesCount;
    private int _current;

    private void Awake()
    {
        instance = this;
        diceNum = Constants.DICE_NUM_INIT;
        goldenDiceNum = Constants.GOLDEN_DICE_NUM_INIT;
        _tilesCount = _tiles.Count;
        _tiles.Sort();
        //_tiles.OrderBy(x => x.index);

        foreach (var tile in _tiles)
        {
            // is 연산자
            // 캐스트 후의 결과 반환하는 연산자
            // 캐스팅 성공하면 true, 실패하면 false
            if (tile is TileInfoStar)
            {
                _starTiles.Add((TileInfoStar)tile);
            }

            // as 명시적 캐스팅연산자
            // 형변환 실패시 null 반환
            //TileInfoStar tmp = tile as TileInfoStar;
            //if (tmp != null)
            //    _starTiles.Add(tmp);
        }
    }

    public void RollADice()
    {
        if(diceNum > 0)
        {
            diceNum--;
            int randomValue = Random.Range(1, 7);
            MovePlayer(randomValue);
            DiceAnimationUI.instance.DoDiceAnimation();
        }
    }

    private void MovePlayer(int diceValue)
    {
        CalcPassedStarTiles(_current, diceValue);
        _current += diceValue;
        if (_current >= _tilesCount)
            _current -= _tilesCount;

        Player.instance.MoveTo(_tiles[_current].transform);
        _tiles[_current].OnTile();
    }

    private void CalcPassedStarTiles(int previous, int totalMove)
    {
        int tmpSum = 0;
        foreach(TileInfoStar starTile in _starTiles)
        {
            if(starTile.index > previous &&
                starTile.index <= previous + totalMove)
            {
                tmpSum += starTile.starValue;
            }
        }
        starScore += tmpSum;
    }
}