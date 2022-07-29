using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DiceAnimationUI : MonoBehaviour
{
    public static DiceAnimationUI instance;
    [SerializeField] private Image _image;
    [SerializeField] private float _animationDelay;
    [SerializeField] private float _animationTime;
    private float _timer;
    private List<Sprite> sprites = new List<Sprite>();
    //private Coroutine _coroutine = null;
    public bool isPlaying { get; private set; }
    public delegate void AfterAnimation(int diceValue);
    event AfterAnimation OnAnimationFinish;

    public void RegisterCallBack(AfterAnimation afterAnimation)
    {
        OnAnimationFinish += afterAnimation;
    }

    private void Awake()
    {
        instance = this;
        LoadSprite();
    }

    private void LoadSprite()
    {
        sprites = Resources.LoadAll<Sprite>("DiceImages").ToList();
    }

    public void DoDiceAnimation(int diceValue)
    {
        //_coroutine = StartCoroutine(E_DiceAnimation());
        StartCoroutine(E_DiceAnimation(diceValue));
    }

    IEnumerator E_DiceAnimation(int diceValue)
    {
        isPlaying = true;
        float elapsedTime = 0;
        while(elapsedTime < _animationTime)
        {
            if(_timer < 0)
            {
                _image.sprite = sprites[Random.Range(6, sprites.Count)];
                _timer = _animationDelay;
            }
            _timer -= Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //while(elapsedTime < _animationTime)
        //{
        //    _image.sprite = sprites[Random.Range(6, sprites.Count)];
        //    yield return new WaitForSeconds(_animationDelay);
        //}
        _image.sprite = sprites[diceValue - 1];
        OnAnimationFinish(diceValue);
        isPlaying = false;
    }
}