using UnityEngine;
using UnityEngine.SceneManagement;

public class Controle : MonoBehaviour
{
    public static Controle _controle;
    public RectTransform _handle;
    

    void Start()
    {
        _controle = this;
        GameManager._gameManager._gamePlay = false;
        Time.timeScale = 1;
        
    }
    //private void Update()
    //{
    //    //if (Input.GetAxis("Fire2")>0)
    //    //{
    //    //    Player._player._pulverizar = true;
    //    //}
    //    //else
    //    //{
    //    //    Player._player._pulverizar = false;
    //    //}
    //    //if (Input.GetAxis("Fire3") > 0)
    //    //{
    //    //    Player._player._correr = true;
    //    //}
    //    //else
    //    //{
    //    //    Player._player._correr = false;
    //    //}
    //}

    public void Correr(bool _ativo)
    {
        if (!_ativo)
        {
            if (Player._player)
            {
                Player._player._correr = false;                
            }
            if (Caminhao._caminhao)
            {
                Caminhao._caminhao._correr = false;
            }
        }
        else
        {
            if (Player._player)
            {
                Player._player._correr = true;

            }
            if (Caminhao._caminhao)
            {
                Caminhao._caminhao._correr = true;
            }
        }
        
    }
    public void Mirar()
    {
        if (Player._player)
        {
            if (Player._player._aim)
            {

                Player._player._aim = false;

            }
            else
            {
                Player._player._aim = true;

            }
        }
    }

    public void Pulverizar(bool _ativo)
    {

        if (!_ativo)
        {
            if (Player._player)
            {
                Player._player._pulverizar = false;
                Player._player._aim = false;
            }
            if (Caminhao._caminhao)
            {
                Caminhao._caminhao._pulverizar = false;
            }
        }
        else
        {
            if (Player._player)
            {
                Player._player._pulverizar = true;
                Player._player._aim = true;
            }
            if (Caminhao._caminhao && Caminhao._caminhao._tanque > 0)
            {
                Caminhao._caminhao._pulverizar = true;
            }
            else
            {
                Caminhao._caminhao._pulverizar = false;
                SoundEffect._soundEffect._pulverizador.Stop();
            }

        }
    }
    //Menu

    public void Iniciar()
    {
        GameManager._gameManager._gamePlay = true;
        UImanager._uimanager._layoutMenu.SetActive(false);
        UImanager._uimanager._layoutGamePlay.SetActive(true);
        //

        UImanager._uimanager.UpdatePontos();

        //yodoone
        YodoOne.yodoOne.DismissBanner();
        StartCoroutine(YodoOne.yodoOne.ShowBanner());
    }
    public void Pausar()
    {
        if (Time.timeScale != 0)
        {            
            Time.timeScale = 0;
            UImanager._uimanager._pausa.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            UImanager._uimanager._pausa.SetActive(false);
        }
    }
    public void Reiniciar()
    {
        SceneManager.LoadScene("Game");
        GameManager._gameManager._gamePlay = true;
        UImanager._uimanager._layoutMenu.SetActive(false);
        UImanager._uimanager._layoutGamePlay.SetActive(true);
        GameManager._gameManager.SaveData();
        //yodo1 MAS
        YodoOne.yodoOne.DismissBanner();
        YodoOne.yodoOne.ShowInterstitial();
    }
    public void Politica()
    {
        if (UImanager._uimanager._Politica.activeSelf)
        {
            UImanager._uimanager._Politica.SetActive(false);
        }
        else
        {
            UImanager._uimanager._Politica.SetActive(true);
        }
    }
    public void SairVeiculo()
    {
        Caminhao._caminhao._motorista.gameObject.SetActive(true);
        Caminhao._caminhao._motorista = null;
        Player._player.transform.SetParent(null);
        Caminhao._caminhao._somMotor.Stop();
        GameManager._gameManager._dirigindo = false;
        CameraPosition._cameraPosition._target = Player._player.transform;
        CameraRadar._cameraRadar._target = Player._player.transform;
        UImanager._uimanager._sairVeiculo.SetActive(false);
        print("Saiu");
    }
    public void Sair()
    {
        SceneManager.LoadScene("Game");
        GameManager._gameManager.SaveData();
        GameManager._gameManager.Start();
        //yodo1 MAS
        YodoOne.yodoOne.DismissBanner();
        YodoOne.yodoOne.ShowInterstitial();
    }
    public void FecharJogo()
    {
        Application.Quit();
    }
}
