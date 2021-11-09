using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public static SoundEffect _soundEffect;
    public AudioSource _stepSound;
    public AudioSource _pulverizador;
    //
    [Header("Som")]
    public AudioClip _audioClip;
    public AudioSource _sound;
    public float _intervaloAndar;
    public float _intervaloCorrer;
    float _timer;
    //

    // Start is called before the first frame update
    void Start()
    {
        _soundEffect = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player._player.gameObject.activeSelf)
        {
            if (Player._player._pulverizar)
            {
                if (!_pulverizador.isPlaying)
                {
                    _pulverizador.Play();
                }
            }
            else
            {
                _pulverizador.Stop();

            }
        }else if (GameManager._gameManager._dirigindo)
        {
            if (Caminhao._caminhao._pulverizar)
            {
                if (!_pulverizador.isPlaying)
                {
                    _pulverizador.Play();
                }
            }
            else
            {
                _pulverizador.Stop();
            }
        }
    }
}