using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner_Script : MonoBehaviour
{
    public GameObject[] levelPieces;
    public GameObject currentPiece, nextPiece;

    private void Start()
    {
        nextPiece = Instantiate(levelPieces[0], this.transform.position, Quaternion.identity);
        SpawnNextPiece();
    }

    public void SpawnNextPiece()
    {
        currentPiece = nextPiece;
        currentPiece.GetComponent<Piece_Script>().enabled = true;

        foreach (SpriteRenderer child in currentPiece.GetComponentsInChildren<SpriteRenderer>())
        {
            Color currentColor = child.color;
            currentColor.a = 1.0f;
            child.color = currentColor;
        }

        StartCoroutine("PrepareNextPiece");
    }

    IEnumerator PrepareNextPiece()
    {
        yield return new WaitForSecondsRealtime(3.0f);

        int i = Random.Range(0, levelPieces.Length);

        nextPiece = Instantiate(levelPieces[i], this.transform.position, Quaternion.identity);
        nextPiece.GetComponent<Piece_Script>().enabled = false;

        foreach (SpriteRenderer child in nextPiece.GetComponentsInChildren<SpriteRenderer>())
        {
            Color currentColor = child.color;
            currentColor.a = 0.3f;
            child.color = currentColor;
        }
    }
}
