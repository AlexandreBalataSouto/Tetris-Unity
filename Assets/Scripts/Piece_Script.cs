using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_Script : MonoBehaviour
{
    float lastFall = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if(!IsValidPiecePosition()){
            Debug.Log("GAME OVER");
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePieceHorizontally(-1);

        }else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovePieceHorizontally(1);

        }else if(Input.GetKeyDown(KeyCode.UpArrow)){
            this.transform.Rotate(0,0,-90);
            if(IsValidPiecePosition()){
                UpdateGrid();
            }else{
                this.transform.Rotate(0,0,90);
            }
        }else if(Input.GetKeyDown(KeyCode.DownArrow) || (Time.time - lastFall) > 1.0f){
            this.transform.position += new Vector3(0,-1,0);
            if(IsValidPiecePosition()){
                 UpdateGrid();
            }else{
                this.transform.position += new Vector3(0,1,0);
                GridHelper_Script.DeleteAllFullRows();
                FindObjectOfType<PieceSpawner_Script>().SpawnNextPiece();
                this.enabled = false;
            }
            lastFall = Time.time;
        }
    }

    void MovePieceHorizontally(int direction)
    {
        this.transform.position += new Vector3(direction, 0, 0);

        //Comprobacion
        if (IsValidPiecePosition())
        {
            UpdateGrid();
        }
        else
        {
            this.transform.position += new Vector3(-direction, 0, 0);
        }
    }

    bool IsValidPiecePosition()
    {
        foreach (Transform block in this.transform)
        {
            Vector2 pos = GridHelper_Script.RoundVector(block.position);

            if (!GridHelper_Script.IsInsideBorders(pos))
            {
                return false;
            }

            Transform possibleObject = GridHelper_Script.grid[(int)pos.x, (int)pos.y];
            if (possibleObject != null && possibleObject.parent != this.transform)
            {
                return false;
            }
        }

        return true;
    }

    void UpdateGrid()
    {
        for (int y = 0; y < GridHelper_Script.height; y++)
        {
            for (int x = 0; x < GridHelper_Script.width; x++)
            {
                if (GridHelper_Script.grid[x, y] != null)
                {
                    if (GridHelper_Script.grid[x, y].parent == this.transform)
                    {
                        GridHelper_Script.grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform block in this.transform)
        {
            Vector2 pos = GridHelper_Script.RoundVector(block.position);
            GridHelper_Script.grid[(int)pos.x, (int)pos.y] = block;
        }
    }
}
