using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    enum type
    {
        health, armor
    }
    public Animator[] _anim;

    [SerializeField] private int _maxArmorPoint;

    private StatsControl _healthPoint;
    private int _prevPoint;
    private int _maxHealthPoint;


    public Image[] img;

    [SerializeField] private Sprite efullHealthPointSprite;
    [SerializeField] private Sprite ehalfHealthPointSprite;
    [SerializeField] private Sprite ezeroHealthPointSprite;


    void Start()
    {
        _healthPoint = GameObject.Find("Player").GetComponent<StatsControl>();
        _maxHealthPoint = (int)_healthPoint.health;
        _prevPoint = _maxHealthPoint;
    }

    void Update()
    {
        ChangeSprite((int)_healthPoint.health, "health");
    }
        
    private void ChangeSprite(int point, string type)
    {
        int maxPoint = 0;
        if (type == "health")
        {
            maxPoint = _maxHealthPoint;
        }
        else if (type == "armor")
        {
            maxPoint = _maxArmorPoint;
        }

        if (point == maxPoint)
        {
            for (int item = 0; item < _maxHealthPoint / 2; item++)
            {
                img[item].sprite = efullHealthPointSprite;
                _prevPoint = point;
            }
        }
        else if (_prevPoint > point)
        {
            if (point % 2 != 0)
                img[point / 2].sprite = ehalfHealthPointSprite;
            else if (point % 2 == 0)
                img[point / 2].sprite = ezeroHealthPointSprite;
            _anim[point / 2].SetTrigger("hit");
            _prevPoint = point;
        }
        else if (_prevPoint < point)
        {
            if (point % 2 != 0)
            {
                img[point / 2].sprite = ehalfHealthPointSprite;
                _anim[point / 2].SetTrigger("healing");
            }
            else if (point % 2 == 0)
            {
                img[point / 2 - 1].sprite = efullHealthPointSprite;
                _anim[point / 2 - 1].SetTrigger("healing");
            }
            _prevPoint = point;
        }
    }
}