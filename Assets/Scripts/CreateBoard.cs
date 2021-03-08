using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateBoard : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject housePrefab;
    public GameObject treePrefab;
    public Text score;
    public GameObject[] tiles = new GameObject[64];
    private long dirtBitBoard = 0;
    private long desertBitBoard = 0;
    private long grainBitBoard = 0;
    private long pastureBitBoard = 0;
    private long waterBitBoard = 0;
    private long woodsBitBoard = 0;
    private long rockBitBoard = 0;
    private long treeBitBoard = 0;
    private long playerBitBoard = 0;
    private static int playerScore = 0;

    void Start()
    {
       BuildGameBoard();
       PrintAllCellCounts();
       InvokeRepeating("PlantTree", 1, 1);
    }

    private void PlantTree()
    {
        int randomRow = UnityEngine.Random.Range(0, 8);
        int randomColumn = UnityEngine.Random.Range(0, 8);

        if(GetCellState(dirtBitBoard & ~playerBitBoard, randomRow, randomColumn))
        {
            GameObject tree = Instantiate(treePrefab);
            tree.transform.parent = tiles[randomRow * 8 + randomColumn].transform;
            tree.transform.localPosition = Vector3.zero;
            treeBitBoard = SetCellState(treeBitBoard, randomRow, randomColumn);
        }
    }

    private void BuildGameBoard()
    {
        for (int row = 0; row < 8; row++)
        {
            for(int column = 0; column < 8; column++)
            {
                int randomTile = UnityEngine.Random.Range(0, tilePrefabs.Length);
                Vector3 position = new Vector3(column, 0, row);
                GameObject tile = Instantiate(tilePrefabs[randomTile], position, Quaternion.identity);
                tile.name = tile.tag + "_" + row + "_" + column;
                tiles[row * 8 + column] = tile;
                UpdateBitBoard(tile, row, column);
            }
        }
    }

    private void PrintAllCellCounts()
    {
        Debug.Log("Dirt Cells =" + CellCount(dirtBitBoard));
        Debug.Log("Desert Cells =" + CellCount(desertBitBoard));
        Debug.Log("Grain Cells =" + CellCount(grainBitBoard));
        Debug.Log("Pasture Cells =" + CellCount(pastureBitBoard));
        Debug.Log("Rock Cells =" + CellCount(rockBitBoard));
        Debug.Log("Water Cells =" + CellCount(waterBitBoard));
        Debug.Log("Woods Cells =" + CellCount(woodsBitBoard));
    }

    private void UpdateBitBoard(GameObject tile, int row, int column)
    {
        switch (tile.tag) 
        {
            case Tag.Desert:
                desertBitBoard = SetCellState(desertBitBoard, row, column);
                PrintBitBoard(Tag.Desert, desertBitBoard);
                break;
            case Tag.Dirt:
                dirtBitBoard = SetCellState(dirtBitBoard, row, column);
                PrintBitBoard(Tag.Dirt, dirtBitBoard);
                break;
            case Tag.Grain:
                grainBitBoard = SetCellState(grainBitBoard, row, column);
                PrintBitBoard(Tag.Grain, grainBitBoard);
                break;
            case Tag.Pasture:
                pastureBitBoard = SetCellState(pastureBitBoard, row, column);
                PrintBitBoard(Tag.Pasture, pastureBitBoard);
                break;
            case Tag.Rock:
                rockBitBoard = SetCellState(rockBitBoard, row, column);
                PrintBitBoard(Tag.Rock, rockBitBoard);
                break;
            case Tag.Water:
                waterBitBoard = SetCellState(waterBitBoard, row, column);
                PrintBitBoard(Tag.Water, waterBitBoard);
                break;
            case Tag.Woods:
                woodsBitBoard = SetCellState(woodsBitBoard, row, column);
                PrintBitBoard(Tag.Woods, woodsBitBoard);
                break;
            default: 
                break;
        }
    }

    private void PrintBitBoard(string name, long bitBoard)
    {
        Debug.Log(name + ": " + Convert.ToString(bitBoard, 2).PadLeft(64, '0'));
    }

    private long SetCellState(long bitBoard, int row, int column)
    {
        long newBit = 1L << (row * 8 + column);
        return bitBoard |= newBit;
    }

    private bool GetCellState(long bitBoard, int row, int column)
    {
        long mask = 1L << (row * 8 + column);
        return ((bitBoard & mask) != 0);
    }

    private int CellCount(long bitBoard)
    {
        int count = 0;
        long bitBoardLocal = bitBoard;

        while(bitBoardLocal != 0)
        {
            bitBoardLocal &= bitBoardLocal - 1;
            count ++;
        }

        return count;
    }

    private void CalculateScore(int row, int column)
    {
        if(GetCellState(dirtBitBoard & playerBitBoard, row, column))
        {
            playerScore = playerScore + 10;  
        }
        else 
        {
            playerScore = playerScore + 2;
        }

        score.text = "Score: " + playerScore;  
    }

    private bool CanPlaceHouse(int row, int column, bool isHit)
    {
        //Can only place on empty dirt tile (no tree) or desert tiles and must be where player clicked
        //We bitwise AND dirt with negated tree and OR that with desert
        return GetCellState((dirtBitBoard & ~treeBitBoard) | desertBitBoard, row, column) && isHit;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool isHit = Physics.Raycast(ray, out hit);
            int row = (int)hit.collider.gameObject.transform.position.z;
            int column = (int)hit.collider.gameObject.transform.position.x;
            if(CanPlaceHouse(row, column, isHit))
            {
                GameObject house = Instantiate(housePrefab);
                house.transform.parent = hit.collider.gameObject.transform;
                house.transform.localPosition = Vector3.zero;
                playerBitBoard = SetCellState(playerBitBoard, (int)hit.collider.gameObject.transform.position.z, (int)hit.collider.gameObject.transform.position.x);
                CalculateScore(row, column);
            }
        }
    }
}
