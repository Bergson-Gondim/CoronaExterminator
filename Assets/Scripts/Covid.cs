using UnityEngine;
using UnityEngine.AI;
public class Covid : MonoBehaviour
{
    public NavMeshAgent _navMeshAgent;
    public Transform _target;
    
    public int _speed;
    public bool _multiplicado;
    public int _indexList;

    public Vector3 _gotoPosition;
    public Vector2 _positionRange;

    [Header("Vida")]
    public float _vida;

    // Start is called before the first frame update
    void Start()
    {
        GameManager._gameManager._instanciasVirus.Add(this.gameObject);
        _indexList = GameManager._gameManager._instanciasVirus.Count - 1;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        //_target = GameObject.FindGameObjectWithTag("Player").transform;
        _gotoPosition = new Vector3(Random.Range(-_positionRange.x,transform.position.x),transform.position.y,Random.Range(-_positionRange.x,_positionRange.y));

        CameraRadar._cameraRadar._radarDots.Add(GetComponentInChildren<SpriteRenderer>().transform);

        GameManager._gameManager._quantidadeVirus++;
        UImanager._uimanager.UpdateBiohazard(GameManager._gameManager._instanciasVirus.Count, GameManager._gameManager._limitVirus);

    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager._gameManager._gamePlay)
        {
            
            _navMeshAgent.destination = _gotoPosition;
            //_navMeshAgent.destination = _target.position;
            _navMeshAgent.speed = _speed*Time.deltaTime;

            if (_vida < 0)
            {

                
                Destroy(this.gameObject);
                GameManager._gameManager.ReordenarLista(_indexList);
                GameManager._gameManager._quantidadeVirus = GameManager._gameManager._instanciasVirus.Count;
                GameManager._gameManager._pontos++;                
                UImanager._uimanager.UpdatePontos();
                UImanager._uimanager.UpdateBiohazard(GameManager._gameManager._instanciasVirus.Count, GameManager._gameManager._limitVirus);
                SoundEffect._soundEffect._sound.PlayOneShot(SoundEffect._soundEffect._audioClip);
                
            }
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "desinfetante")
        {
            _vida -= 1;
        }
    }
}
