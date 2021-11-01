using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SquareMode : MonoBehaviour
{
    public List<Color> _sqareColors = new List<Color>();

    public List<Square> _leftSquares = new List<Square>();

    public List<Square> _rightSquares = new List<Square>();

    private List<Color> _availbleColors;

    private List<int> _availbleLeftIndex;

    private List<int> _availbleRightIndex;

    public Square CurrentDraggeSquare;

    public Square CurrentHoveredSquare;

    public bool isTaskCompleted = false;

    private static int maxSquares = 3;

    public static int gameScore = 0;

    private static float setUpTime = 30f;

    public Text textTimer;

    public Text textScore;

    private void Start()
    {
 
        _availbleColors = new List<Color>(_sqareColors);
        _availbleLeftIndex = new List<int>();
        _availbleRightIndex = new List<int>();
        textScore.text = gameScore.ToString();

        for (int i = 0; i < maxSquares; i++)
        {
            _availbleLeftIndex.Add(i);
        }

        for (int i = 0; i < maxSquares; i++)
        {
            _availbleRightIndex.Add(i);
        }

        while (_availbleColors.Count > 0 && _availbleLeftIndex.Count > 0 && _availbleRightIndex.Count > 0)
        {
            Color pickedColor = _availbleColors[Random.Range(0, _availbleColors.Count)];
            int pickedLeftIndex = Random.Range(0, _availbleLeftIndex.Count);
            int pickedRightIndex = Random.Range(0, _availbleRightIndex.Count);

            _leftSquares[_availbleLeftIndex[pickedLeftIndex]].SetColor(pickedColor);
            _rightSquares[_availbleRightIndex[pickedRightIndex]].SetColor(pickedColor);

            _availbleColors.Remove(pickedColor);
            _availbleLeftIndex.RemoveAt(pickedLeftIndex);
            _availbleRightIndex.RemoveAt(pickedRightIndex);

        }

        StartCoroutine(CheckTaskCompetion());
    }

    void Update()
    {
        if(setUpTime > 0)
        {
            setUpTime -= Time.deltaTime;
            textTimer.text = Mathf.Round(setUpTime).ToString();
        }
        else
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private IEnumerator CheckTaskCompetion()
    {
        while (!isTaskCompleted)
        {
            int successfullSquares = 0;
            for (int i = 0; i < maxSquares; i++)
            {
                if (_rightSquares[i].IsSuccess) { successfullSquares++; }

            }
            if (successfullSquares >= maxSquares)
            {
                if (maxSquares < 8)
                {
                    maxSquares++;
                }
                setUpTime = 30 - (gameScore / 10 +1);
                gameScore += 10;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+0);
            }

            yield return new WaitForSeconds(0.5f);

        }
    }

    private void OnDestroy()
    {
        Score.score = gameScore;
    }

}


