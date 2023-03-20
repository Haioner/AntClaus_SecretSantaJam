using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pressAnyKeyText;
    [SerializeField] private Animator canvasAnim;
    private int countStory = 0;

    void Update()
    {
        Continue();
    }

    private void Continue()
    {
        if (Input.anyKeyDown && pressAnyKeyText.gameObject.activeInHierarchy)
        {
            switch (countStory)
            {
                case 2:
                    GameObject.Find("Music_Menu").GetComponent<AudioManager>().StopAudio();
                    FindObjectOfType<Transition>().PlayOutTransition("Game");
                    break;

                case 1:
                    canvasAnim.Play("StoryPart2");
                    countStory = 2;
                    break;

                case 0:
                    pressAnyKeyText.gameObject.SetActive(false);
                    canvasAnim.Play("StoryAnim");
                    countStory = 1;
                    break;

                default:
                    Debug.Log("Padrão");
                    break;
            }
        }
    }
}
