using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractObject : MonoBehaviour
{
    private string _tagObject = null;
    [SerializeField] private Animator _animImg;
    [SerializeField] private Animator _animText;
    [SerializeField] private TextMeshProUGUI _coinCounterText;

    [SerializeField] private TextMeshProUGUI[] _bigPotionCounterText;
    [SerializeField] private TextMeshProUGUI[] _smallPotionCounterText;
    [SerializeField] private GameObject _potionEquip;




    [SerializeField] private GameObject _interactObjectImage;
    [SerializeField] private GameObject _coin_TextX;


    private int _oldCountCoin = 0;

    private StatsControl _statsControl;

    private bool _findObject;
    private bool _isWeaperonEquip = false;

    private Collider2D _objectCollision;

    private GameObject _newInteractObjectImage;
    private GameObject _currentWeapon;

    private Quaternion _oldTransformRotation;

    void Start()
    {
        _statsControl = GetComponent<StatsControl>();
        _potionEquip.SetActive(false);
        _interactObjectImage.SetActive(false);
        _newInteractObjectImage = _interactObjectImage;
        _coinCounterText.text = _statsControl.coin.ToString();
        _oldCountCoin = _statsControl.coin;
    }

    private void Update()
    {
        if (_oldCountCoin != _statsControl.coin)
        {
            checkCountNumCoin();
            _coinCounterText.text = _statsControl.coin.ToString();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _potionEquip.SetActive(true);
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            _potionEquip.SetActive(false);
            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        if (_potionEquip.active) 
        {
            for (int i = 0; i < 4; i++)
            {
                _bigPotionCounterText[i].text = _statsControl.bigPotion[i].ToString();
                _smallPotionCounterText[i].text = _statsControl.smallPotion[i].ToString();
            }
        }
    }

    private void checkCountNumCoin()
    {
        double countNum = Math.Log10(_statsControl.coin);
        if (countNum >= 5)
        {
            _coin_TextX.transform.localPosition = new Vector3(-7.5f, 0, 0);
        }
        else if (countNum >= 4)
        {
            _coin_TextX.transform.localPosition = new Vector3(-3f, 0, 0);
        }
        else if (countNum >= 3)
        {
            _coin_TextX.transform.localPosition = new Vector3(2.3f, 0, 0);
        }
        else if (countNum >= 2)
        {
            _coin_TextX.transform.localPosition = new Vector3(6.8f, 0, 0);
        }
        else if (countNum >= 1)
        {
            _coin_TextX.transform.localPosition = new Vector3(11.3f, 0, 0);
        }
        else if (countNum >= 0)
        {
            _coin_TextX.transform.localPosition = new Vector3(16f, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            _newInteractObjectImage.SetActive(true);
            _animImg.Play("Base Layer.key_idle");
            _animText.Play("Base Layer.key_text_idle");

            _tagObject = LayerMask.LayerToName(collision.gameObject.layer);

            _newInteractObjectImage.transform.position = collision.transform.position + new Vector3(0, 1.2f, 0);
            _objectCollision = collision;
            _findObject = true;
        }
        else if (collision.gameObject.tag == "Coin")
        {
            _statsControl.coin += 1;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            _newInteractObjectImage.SetActive(false);
            _findObject = false;
        }
    }

    public void StartClickAnim()
    {
        if (_findObject)
        {
            _animImg.SetTrigger("pressedImg");
            _animText.SetTrigger("pressedText");
        }
    }

    public void EquipObject()
    {
        switch (_tagObject)
        {
            case "WeaperonMelee":
                {
                    HandMeleeWeaperon();
                } break;
            case "WeaperonRange":
                {

                }
                break;
            case "Potion":
                {
                    HandPotion();
                } break;
        }
    }

    public void HandPotion()
    {
        string[] s = _objectCollision.GetComponent<Text>().text.Split('.');
        bool sizeSelect = false;
        int[] curPotion = _statsControl.bigPotion;

        switch (s[0])
        {
            case "Big":
                {
                    curPotion = _statsControl.bigPotion;
                    sizeSelect = true;
                }
                break;
            case "Small":
                {
                    curPotion = _statsControl.smallPotion;
                    sizeSelect = true;
                }
                break;
        }

        if (sizeSelect)
        {
            switch (s[1])
            {
                case "Health":
                    {
                        curPotion[0] += 1;
                    }
                    break;
                case "UpDmg":
                    {
                        curPotion[1] += 1;
                    }
                    break;
            }
            Destroy(_objectCollision.gameObject);
        }
        

    }

        public void HandMeleeWeaperon()
    {
        _findObject = false;
        _objectCollision.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("WeaperonEquip");

        if (_isWeaperonEquip == true)
        {
            _currentWeapon.transform.SetParent(null);
            _currentWeapon.transform.rotation = _oldTransformRotation;
            _currentWeapon.transform.position += new Vector3(0, -0.3f, 0);
            _currentWeapon.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Weaperon");
        }

        _currentWeapon = _objectCollision.gameObject;
        _isWeaperonEquip = true;

        _oldTransformRotation = _objectCollision.gameObject.transform.rotation;
        _objectCollision.gameObject.transform.parent = transform;
        Debug.Log(transform.localRotation.y);
        //Debug.Log(transform.gameObject.name);
        _objectCollision.gameObject.transform.position = transform.localRotation.y == -1 ? transform.position + new Vector3(0.3f, 0, 0) : transform.position + new Vector3(-0.3f, 0, 0);
        
        _objectCollision.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 45));

    }
}
