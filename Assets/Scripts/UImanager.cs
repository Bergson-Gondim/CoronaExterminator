using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager _uimanager;
    [Header("Canvas")]
    public GameObject _layoutMenu;
    public GameObject _layoutGamePlay;
    public GameObject _Politica;
    public GameObject _tutorial;
    [Header("Menu")]
    public Text _record;
    public Text _nivel;

    [Header("HUD")]
    public Slider _biohazard;
    public Slider _caminahoTanque;
    public Text _pontos;
    public Text _timer;
    public Slider _vida;
    public Text _countdown;
    [Header("Botões")]
    public GameObject _pausa;
    public GameObject _sairVeiculo;

    public GameObject _vocePerdeu;

    public GameObject _voceGanhou;
    // Start is called before the first frame update
    void Start()
    {
        _uimanager = this;
        _layoutMenu.SetActive(true);
        _layoutGamePlay.SetActive(false);
        //
       
        _pontos.text = 0.ToString();
        _vida.value = 1;
        //detectar a primeira vez
        if (PlayerPrefs.GetInt("PrimeiraVez")==0)
        {
            _tutorial.SetActive(true);
            PlayerPrefs.SetInt("PrimeiraVez", 1);
        }
        else
        {
            _tutorial.SetActive(false);
        }
       //
        float min = (int)(GameManager._gameManager._record / 60);
        float sec = (int)(GameManager._gameManager._record % 60);
        float mil = (int)(GameManager._gameManager._record * 100f) % 100;
        _record.text = min.ToString("00") + ":" + sec.ToString("00") + ":" + mil.ToString("00");
        //
        _vocePerdeu.SetActive(false);
        _voceGanhou.SetActive(false);
        _countdown.gameObject.SetActive(false);
        //
        UpdateBiohazard(GameManager._gameManager._quantidadeVirus , GameManager._gameManager._limitVirus);
        UpdatePontos();
        UpdateNivel();
    }

    public void UpdateBiohazard(float _virusMount, float _virusMax)
    {
        _biohazard.value = _virusMount / _virusMax;
    }
    public void UpdatePontos()
    {
        _pontos.text = GameManager._gameManager._pontos.ToString();
    }
    public void UpdateVida(float _vidaValor)
    {
        _vida.value = _vidaValor;
    }
    public void UpdateNivel()
    {
        _nivel.text = "Nível"+" "+GameManager._gameManager._nivel.ToString();
    }
    public void ShowRanking()
    {
        Social.ShowLeaderboardUI();
    }
    public void ShowTutorial()
    {
        Tutorial._tutorial.Start();
        _tutorial.SetActive(true);
    }
}
