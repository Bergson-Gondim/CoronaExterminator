using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeutralAgeScreen : MonoBehaviour
{
    public static NeutralAgeScreen _neutralAgeScreen;
    [Header("Entrada de valores")]
    public Dropdown _monthInput;
    public Dropdown _yearInput;
    List<String> _yearsOptions = new List<String> { };
    public Dropdown _dayInput;
    List<String> _daysOptions = new List<String> { };
    public DateTime _bornDate;
    [Header("Resultado da idade")]
    public int _age;
    public bool _kid, _teen, _adult;
    public bool _COPPA = false;
    [Header("Layout")]
    public GameObject _windowConfig;
    public Button _continue;
    // Start is called before the first frame update
    void Start()
    {
        //verificar se já foi feita a informação
        if (!PlayerPrefs.HasKey("NeutralAgeScreen"))
        {
            _windowConfig.SetActive(true);
        }
        else
        {
            _windowConfig.SetActive(false);
            if (PlayerPrefs.GetInt("Fase") == 1)
            {
                _COPPA = true;
                SetCOPPA();
            }
            else
            {
                _COPPA = false;
                SetCOPPA();
            }
        }
        //
        _neutralAgeScreen = this;
        _bornDate = DateTime.Now;
        _continue.interactable = false;

        InstanciarAnos();
        InstanciarDiasMes();
        ChangeDate();
    }
    public void InstanciarAnos()
    {
        //instanciar quantidade de anos

        for (int i = 0; i <= 130; i++)
        {
            _yearsOptions.Add((DateTime.Now.Year - i).ToString());
        }
        _yearInput.ClearOptions();
        _yearInput.AddOptions(_yearsOptions);
        print(_yearInput.captionText.text);
    }
    public void InstanciarDiasMes()
    {
        int _days = DateTime.DaysInMonth(_bornDate.Year, _bornDate.Month);
        _daysOptions.Clear();
        _dayInput.ClearOptions();
        for (int i = 0; i < _days; i++)
        {
            _daysOptions.Add((i + 1).ToString());
        }

        _dayInput.AddOptions(_daysOptions);

    }
    public void SetButtonActive()
    {
        _continue.interactable = true;
    }
    public void ChangeDate()
    {
        _bornDate = new DateTime(int.Parse(_yearInput.captionText.text), int.Parse((_monthInput.value + 1).ToString()), int.Parse((_dayInput.captionText.text)));
        InstanciarDiasMes();
        SetButtonActive();
    }
    public void CalcAge()
    {
        //_bornDate = new DateTime(int.Parse(_yearInput.text), _monthInput.value + 1, int.Parse(_dayInput.text));
        //calculando a idade
        _age = DateTime.Today.Year - _bornDate.Year;
        //corrigindo a idade de acordo com o Mês e dia
        if (_bornDate.Month > DateTime.Now.Month || _bornDate.Month == DateTime.Now.Month && _bornDate.Day > DateTime.Now.Day)
        {
            _age--;
        }
        //definindo a faixa etária
        if (_age < 13)
        {
            _kid = true;
            _teen = false;
            _adult = false;
            PlayerPrefs.SetInt("Fase", 1);
            _COPPA = true;
        }
        else
        {
            if (_age >= 13 & _age < 18)
            {
                _kid = false;
                _teen = true;
                _adult = false;
                PlayerPrefs.SetInt("Fase", 2);
                _COPPA = false;
            }
            else
            {
                _kid = false;
                _teen = false;
                _adult = true;
                PlayerPrefs.SetInt("Fase", 3);
                _COPPA = false;
            }
        }
        print(_bornDate);
        print(_kid.ToString() + _teen.ToString() + _adult.ToString());
        print(_age);

        PlayerPrefs.SetInt("NeutralAgeScreen", 0);
        PlayerPrefs.SetInt("Age", _age);
        _windowConfig.SetActive(false);

        SetCOPPA();
    }
    public void SetCOPPA()
    {
        //inserir o comando para ativar ou não ativar COPPA
        YodoOne.yodoOne.isCOPPA = _COPPA;
        YodoOne.yodoOne.StartYodo();
    }
}
