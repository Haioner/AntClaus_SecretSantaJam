using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioManager musicMenu;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Awake()
    {
        musicMenu.PlayAudio(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
