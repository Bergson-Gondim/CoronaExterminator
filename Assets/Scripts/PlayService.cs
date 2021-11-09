using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class PlayService : MonoBehaviour
{
    public static PlayService _playservices;
    public GameObject _leaderBoardButton;
    //public GameObject _achievmentsButton;
    //public Text _loginText;

    private void Awake()
    {
        //definindo 
        if (_playservices == null)
        {
            _playservices = this;
        }
        else if (_playservices != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //esconder botão do play services
        _leaderBoardButton.SetActive(false);
        //_achievmentsButton.SetActive(false);
        //
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate(succes =>
            {
                if (succes)
                {
                    //_loginText.text = Social.localUser.userName;
                }
                else
                {
                    //_loginText.text = "Login";
                }
            });
        }
        else
        {
            //_loginText.text = "Login";
        }
    }
    private void Update()
    {
        //mostrar botão do play services
        if (Social.localUser.authenticated)
        {
            _leaderBoardButton.SetActive(true);
            //_achievmentsButton.SetActive(true);
        }
    }
    public static void UnlockAchivment(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }
    public static void IncrementAchievment(string id, int steps)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, steps, success => { });
    }

    public static void PostEvents(uint score, string events)
    {

        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.Events.IncrementEvent(events, score);
        }
    }

    public void LoginLogout()
    {
        if (Social.localUser.authenticated)
        {

            PlayGamesPlatform.Instance.SignOut();
            //_loginText.text = "Login";

        }
        else
        {
            Start();
        }
    }

    public static void ShowAchievments()
    {
        Social.ShowAchievementsUI();
    }
    public static void ShowLeaderboard(string leaderboard)
    {
        //Social.ShowLeaderboardUI();
        PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboard);
    }
    public static void PostScore(long score, string leaderboard)
    {
        Social.ReportScore(score, leaderboard, (success => { }));
    }
}
