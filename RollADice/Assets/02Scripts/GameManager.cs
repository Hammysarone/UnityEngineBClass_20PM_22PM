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
    public int diceNum
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
    public int goldenDiceNum
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

    // direction 1 : positive, -1 : negative
    private int _direction;
    public int direction
    {
        get
        {
            return _direction;
        }
        set
        {
            if (value < 0)
            {
                _direction = -1;
                _inverseIcon.SetActive(true);
            }
            else
            {
                _direction = 1;
                _inverseIcon.SetActive(false);
            }
        }
    }
    [SerializeField] private GameObject _inverseIcon;
    private int _tilesCount;
    private int _current;

    private void Awake()
    {
        instance = this;
        diceNum = Constants.DICE_NUM_INIT;
        goldenDiceNum = Constants.GOLDEN_DICE_NUM_INIT;
        direction = Constants.DIRECTION_POSITIVE;
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

    private void Start()
    {
        DiceAnimationUI.instance.RegisterCallBack(MovePlayer);
    }

    public void RollADice()
    {
        if(diceNum > 0
            && DiceAnimationUI.instance.isPlaying == false)
        {
            diceNum--;
            int randomValue = Random.Range(1, 7);
            DiceAnimationUI.instance.DoDiceAnimation(randomValue);
        }
    }

    public void RollAGoldenDice(int diceValue)
    {
        if(goldenDiceNum > 0 &&
            DiceAnimationUI.instance.isPlaying == false)
        {
            goldenDiceNum--;
            DiceAnimationUI.instance.DoDiceAnimation(diceValue);
        }
    }

    private void MovePlayer(int diceValue)
    {
        if(_direction > 0)
        {
            CalcPassedStarTiles(_current, diceValue);
            _current += diceValue;
            if (_current >= _tilesCount)
                _current -= _tilesCount;
        }
        else
        {
            _current -= diceValue;
            if (_current < 0)
                _current += _tilesCount;
            direction = Constants.DIRECTION_POSITIVE;
        }


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