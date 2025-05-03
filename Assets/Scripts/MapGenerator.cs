using UnityEngine;
using System.Collections;
using System;

public class MapGenerator : MonoBehaviour {

    public int width;
    public int height;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map;

    public GameObject wallObj;
    public GameObject floorObj;

    public GameObject mapObj;  

    void Start() {
        GenerateMap();
        DrawMap(map);
    }

    void Update() {
        
    }

    void GenerateMap() {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++) {
            SmoothMap();
        }

        //int borderSize = 5;
        //int[,] borderedMap = new int[width + borderSize * 2, height + borderSize * 2];

        //for (int x = 0; x < borderedMap.GetLength(0); x++) {
        //    for (int y = 0; y < borderedMap.GetLength(1); y++) {
        //        if (x >= borderSize && x < width + borderSize && y >= borderSize && y < height + borderSize) {
        //            borderedMap[x, y] = map[x - borderSize, y - borderSize];
        //        }
        //    }
        //}


    }

    void DrawMap(int[,] thisMap) {
        if (thisMap!= null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 pos = new Vector3(-width / 2 + x + .5f,  -height / 2 + y + .5f, 0);
                    if (thisMap[x,y] == 0) {
                        GameObject newFloor = Instantiate(floorObj, pos, Quaternion.identity) as GameObject;
                        newFloor.transform.parent = mapObj.transform.GetChild(1);
                    }
                    else{
                        GameObject newWall = Instantiate(wallObj, pos, Quaternion.identity) as GameObject;
                        newWall.transform.parent = mapObj.transform.GetChild(0);
                    }

                }
            }
        }

    }




    void AStar(int[,] thisMap, IntVector2 startPos, IntVector2 endPos) {



    }


[System.Serializable]
    public struct IntVector2 {

	    public int x, z;
	
	    public IntVector2 (int x, int z) {
		    this.x = x;
		    this.z = z;
	    }

        public static IntVector2 operator +(IntVector2 a, IntVector2 b) {
		    a.x += b.x;
		    a.z += b.z;
		    return a;
	    }
    }



    void RandomFillMap() {
        if (useRandomSeed) {
            seed = Time.time.ToString();
        }

        System.Random psuedoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1) {
                    map[x, y] = 1;
                }
                else {
                    map[x, y] = (psuedoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }
    }

    void SmoothMap() {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurrondingWallCount(x, y);

                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;

            }
        }
    }

    int GetSurrondingWallCount(int gridX, int gridY) {
        int wallCount = 0;

        for (int neighbourX = + - 1; neighbourX <= gridX + 1; neighbourX ++) {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height) {
                    if (neighbourX != gridX || neighbourX != gridY) {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else {
                    wallCount++;
                }

            }
        }

        return wallCount;
    }

	
}
