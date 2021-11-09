using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial _tutorial;
    public GameObject[] _pages;
    public int _page;
    // Start is called before the first frame update
    public void Start()
    {
        _tutorial = this;
        _page = _pages.Length-1;
        for (int i = 0; i < _pages.Length; i++)
        {
            _pages[i].SetActive(true);
        }
    }
    public void HidePage()
    {
        _pages[_page].SetActive(false);
        _page--;
        
    }
}
