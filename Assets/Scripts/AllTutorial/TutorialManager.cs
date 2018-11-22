using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    public List<Tutorial> Tutorials = new List<Tutorial>();

    public Text expText;

    private static TutorialManager instance;
    public static TutorialManager Instace
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<TutorialManager>();

            if (instance == null)
                Debug.Log("There is no TutorialManager");

            return instance;
        }
    }

    private Tutorial currentTutorial;


	// Use this for initialization
	void Start () {
        //StartCoroutine(timer());
        SetNextTutorial(0);
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTutorial)
		{
			currentTutorial.CheckIfHappening();
		}
	}

    public void CompletedTutorial()
    {
		StartCoroutine(timer());
		SetNextTutorial(currentTutorial.Order + 1);
		if (currentTutorial.Order == Tutorials.Count)
            CompletedAllTutorials();

    }
    public void SetNextTutorial(int currentOrder)
    {
        currentTutorial = GetTutorialByOrder(currentOrder);

        if (!currentTutorial)
        {
            return;
        }

        expText.text = currentTutorial.Explanation;
        StartCoroutine(timer());
    }

    public void CompletedAllTutorials()
    {
        expText.text = "Congratulations! You have completed all the tutorials! Press Esc to go back to the menu.";
        SceneManager.LoadScene("Menu");
    }
    public Tutorial GetTutorialByOrder(int Order)
    {
        for (int i = 0; i < Tutorials.Count; i++)
        {
            if (Tutorials[i].Order == Order)
                return Tutorials[i];
        }

        return null;
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(10.0f);
    }
}
