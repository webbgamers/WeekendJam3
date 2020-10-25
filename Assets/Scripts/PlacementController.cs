using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacementController : MonoBehaviour
{
    public int money = 10;
    string currentPlacement = "";
    public GameObject mapTiles;
    Tilemap mapTilemap;
    public GameObject gameGridObject;
    Grid gameGrid;
    public GameObject scarecrow;
    public GameObject skeleton;

    // Start is called before the first frame update
    void Start()
    {
        mapTilemap = mapTiles.GetComponent<Tilemap>();
        gameGrid = gameGridObject.GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && currentPlacement != "" && money > 4) {
            Vector3 moustPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = mapTilemap.WorldToCell(moustPoint);
            Color tileColor = mapTilemap.GetColor(cellPosition);
            float H, S, V;
            Color.RGBToHSV(tileColor, out H, out S, out V);
            if (V == 0.99f) {
                print("invalid placement");
            }
            else {
                print("valid placement");
                if (currentPlacement == "scarecrow") {
                    GameObject newProjectile = (GameObject)Instantiate(scarecrow, gameGrid.GetCellCenterWorld(cellPosition), Quaternion.identity);
                    mapTilemap.SetColor(cellPosition, Color.HSVToRGB(1f, 1f, 0.99f));
                    currentPlacement = "";
                }
                else if (currentPlacement == "skeleton") {
                    GameObject newProjectile = (GameObject)Instantiate(skeleton, gameGrid.GetCellCenterWorld(cellPosition), Quaternion.identity);
                    mapTilemap.SetColor(cellPosition, Color.HSVToRGB(1f, 1f, 0.99f));
                    currentPlacement = "";
                }
                money -= 5;
            }
        }
    }

    public void ScarecrowSelect() {
        print("placement is now scarecrow");
        currentPlacement = "scarecrow";
    }

    public void SkeletonSelect() {
        print("placement is now skeleton");
        currentPlacement = "skeleton";
    }
}
