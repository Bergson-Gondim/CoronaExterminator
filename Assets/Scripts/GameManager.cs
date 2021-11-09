using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _gameManager;
    public GameObject _virus;
    public Vector2 _posicaoInicial;
    public int _limitVirus;
    public int _quantidadeVirus;
    public int _nivel;
    //
    public int _intervaloMultiplicacao;
    public float _intervaloTime;
    //
    public float _countdown;
    float _countdownTimer;
    public bool _perdeu;
    public bool _ganhou;
    //
    public bool _dirigindo;
        //
    public bool _gamePlay;
    [Header("Pontuação")]
    public int _pontos;
    public int _record;
    public float _timer;
    public float _timerRecord;
    [Header("Virus instanciados")]
    public List<GameObject> _instanciasVirus;
    [Header("Musica")]
    public AudioSource _musicaBG;
    // Start is called before the first frame update
    private void Awake()
    {
        //definindo 
        if (_gameManager == null)
        {
            _gameManager = this;
        }
        else if (_gameManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        _musicaBG = GetComponentInChildren<AudioSource>();
        _intervaloTime = _intervaloMultiplicacao;
        _record = PlayerPrefs.GetInt("Record");
        _timerRecord = PlayerPrefs.GetFloat("TimerRecord");
        _quantidadeVirus = 0;
    }
    public void Start()
    {
        if (PlayerPrefs.GetInt("Nivel") == 0)
        {
            _nivel = 1;
        }
        else
        {
            _nivel = PlayerPrefs.GetInt("Nivel");
        }

        for (int i = 0; i < _nivel; i++)
        {
            var _spawnInstancia = Instantiate(_virus);
            _spawnInstancia.transform.position = new Vector3(Random.Range(-_posicaoInicial.x, _posicaoInicial.x), _spawnInstancia.transform.position.y, Random.Range(-_posicaoInicial.y, _posicaoInicial.y));
        }
        _countdownTimer = _countdown;
        _dirigindo = false;
    }

    void Update()
    {
        if (!_gamePlay || _ganhou || _perdeu)
            return;

        if (_intervaloTime < 0 && _quantidadeVirus < _limitVirus)
        {
            Multiplicar();
        }
        else
        {
            _intervaloTime -= Time.deltaTime;
            _quantidadeVirus = _instanciasVirus.Count;
        }
        //timer
        _timer += Time.deltaTime;
        float min = (int)(_timer / 60);
        float sec = (int)(_timer % 60);
        float mil = (int)( _timer * 100f)%100;
        UImanager._uimanager._timer.text = min.ToString("00")+":"+sec.ToString("00") + ":" + mil.ToString("00");
        //
        if (_quantidadeVirus == _limitVirus)
        {
            _countdownTimer -= Time.deltaTime;
            float minutes = Mathf.Floor(_countdownTimer / 60);
            float seconds = _countdownTimer % 60;
            UImanager._uimanager._countdown.gameObject.SetActive(true);
            UImanager._uimanager._countdown.text=minutes.ToString("F0")+":"+seconds.ToString("F0");
            if (_countdownTimer < 0)
            {
                UImanager._uimanager._countdown.gameObject.SetActive(false);
                UImanager._uimanager._vocePerdeu.SetActive(true);
                _perdeu = true;
            }
        }
        else
        {
            _countdownTimer = _countdown;
            UImanager._uimanager._countdown.gameObject.SetActive(false);
        }
        if (_quantidadeVirus == 0)
        {   
            UImanager._uimanager._voceGanhou.SetActive(true);
            _musicaBG.Stop();
            _ganhou = true;
            _nivel++;
        }
    }
    void Multiplicar()
    {
        _intervaloMultiplicacao = _instanciasVirus.Count;
        _intervaloTime = _intervaloMultiplicacao;
        for (int i = 0; i < _instanciasVirus.Count; i++)
        {
            if (i < _limitVirus - _instanciasVirus.Count && !_instanciasVirus[i].GetComponent<Covid>()._multiplicado)
            {
                Instantiate(_instanciasVirus[i]);
            }
        }
    }
    public void SaveData()
    {
        if (_pontos > _record)
        {
            PlayerPrefs.SetInt("Record", _pontos);
        }
        if (_timer < _timerRecord)
        {
            PlayerPrefs.SetFloat("TimerRecord", _timer);
            PlayService.PostScore((long)_timer, GPGSIds.leaderboard_melhor_tempo);
        }
        if (_ganhou)
        {
            PlayerPrefs.SetInt("Nivel", _nivel);
            PlayService.PostScore((long)_nivel, GPGSIds.leaderboard_ranking);
        }
        //play services
        
        
    }
    public void ReordenarLista(int _index)
    {

        _instanciasVirus.RemoveAt(_index);
        CameraRadar._cameraRadar._radarDots.RemoveAt(_index);
        for (int i = 0; i < _instanciasVirus.Count; i++)
        {
            _instanciasVirus[i].GetComponent<Covid>()._indexList = i;
            //CameraRadar._cameraRadar._radarDots[i].GetComponent<CameraRadar>
        }


    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
