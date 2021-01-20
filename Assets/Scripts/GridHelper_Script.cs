using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHelper_Script : MonoBehaviour
{
    
    public static int width = 10, height = 20+4;
    public static Transform [,] grid = new Transform[width,height];
    
    public static Vector2 RoundVector(Vector2 vector){
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }

    public static bool IsInsideBorders(Vector2 position){
        if(position.x >= 0 && position.y >=0 && position.x < width){
            return true;
        }else{
            return false;
        }
    }

    public static void DeleteRow(int y){
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x,y]);
            grid[x,y] = null;
        }
    }

    public static void DecreaseRow(int y){
        for (int x = 0; x < width; x++)
        {
            if(grid[x,y] != null){
                grid[x,y-1] = grid[x,y];
                grid[x,y] = null;
                //Repitamos el bloque una posicion mas abajo en la pantalla
                grid[x,y-1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void DecreaseRowAbove(int y){
        for (int i = y; i < height; i++)
        {
            DecreaseRow(i);
        }
    }

    public static bool IsRowFull(int y){
        for (int x = 0; x < width; x++)
        {
            if(grid[x,y] == null){
                return false;
            }
        }
        return true;
    }

    public static void DeleteAllFullRows(){
        for(int y = 0; y < height; y++){
            if(IsRowFull(y)){
                DeleteRow(y);
                DecreaseRowAbove(y+1);
                y--;
            }
        }
        CleaPieces();
    }

    private static void CleaPieces(){
        foreach(GameObject piece in GameObject.FindGameObjectsWithTag("Piece"))
        {
            if(piece.transform.childCount == 0){
                Destroy(piece);
            }
        }
    }
}
