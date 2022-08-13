using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public Animator[] _anim;
    public int healthPoint;

    [SerializeField] private int _maxHealthPoint;
    [SerializeField] private int _maxArmorPoint;

    private int _prevPoint;

    enum type
    {
        health, armor
    }

    public Image[] img;

    [SerializeField] private Sprite efullHealthPointSprite;
    [SerializeField] private Sprite ehalfHealthPointSprite;
    [SerializeField] private Sprite ezeroHealthPointSprite;


    void Start()
    {
        healthPoint = _maxHealthPoint;
        _prevPoint = _maxHealthPoint;
    }

    void Update()
    {
        ChangeSprite(healthPoint, "health");
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