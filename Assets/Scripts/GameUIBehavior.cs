using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUIBehavior : MonoBehaviour
{
    private float _timer;
    private int _intTimer;
    private int _killCount;

    private bool _counting = true;

    private TMP_Text _killText;
    private TMP_Text _timeText;

    private RectTransform _hpRectTransform;

    private GameObject _gameOverPanel;
    

    public void EnemyKilled ()
    {
        _killCount++;
        _killText.SetText(string.Concat(": ",_killCount.ToString()));
        
    }

    public void PlayerKilled ()
    {
        _counting = false;
        _gameOverPanel.SetActive(true);
    }

    public void GoToMenu ()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayerHurt (float curHealth, float maxHealth)
    {
        float ratio = curHealth / maxHealth;
        if (ratio < 0) ratio = 0;

        //Debug.Log(ratio);

        _hpRectTransform.localScale = new Vector3 (ratio,1,1);
        _hpRectTransform.localPosition = new Vector3 (ratio*400/2 - 200,0,0);
    }
    
    void Start()
    {
        _timer = 0f;
        _intTimer = 0;
        _killCount = 0;
        _killText = GameObject.Find("CountLabel").GetComponent<TMP_Text>();
        _timeText = GameObject.Find("TimeLabel").GetComponent<TMP_Text>();
        _hpRectTransform = GameObject.Find("HealthLabel").GetComponent<RectTransform>();
        _gameOverPanel = GameObject.Find("GameOverPanel");
        _gameOverPanel.SetActive(false);
        _gameOverPanel.transform.localScale = new Vector3 (1,1,1);

        Debug.Log(_killText.text);
    }

    
    void Update()
    {
        _timer += Time.deltaTime;
        if (Mathf.FloorToInt(_timer) > _intTimer && _counting)
        {
            _intTimer = Mathf.FloorToInt(_timer);

            _timeText.SetText(string.Concat(": ", _intTimer.ToString(), " s"));
        }
    }
}
