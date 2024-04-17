using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntity : MonoBehaviour
{
    [SerializeField]
    private PlayerController _controller;

    [SerializeField]
    private GameObject _mesh;


    [ContextMenu("Respawn")]
    public void Hit()
    {
        // reload scene
        _controller.enabled = false;
        _mesh.SetActive(false);
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void Launch(float jumpHeight)
    {
        _controller.Launch(jumpHeight);
    }
}
