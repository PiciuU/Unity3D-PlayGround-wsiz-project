using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ColliderGenerator : MonoBehaviour
{
    private float colThickness = 1f;
    private Vector2 screenSize;

    [Serializable] public struct COLLIDERS {
        public GameObject TopBoundary;
        public GameObject BottomBoundary;
        public GameObject LeftBoundary;
        public GameObject RightBoundary;
        public GameObject SpecialItemsSpawnZone;
    }

    public COLLIDERS colliders;

    void Start()
    {
        //Generate world space point information for position and scale calculations
        Vector3 cameraPos = Camera.main.transform.position;

        //Grab the world-space position values of the start and end positions of the screen, then calculate the distance between them and store it as half, since we only need half that value for distance away from the camera to the edge
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f; 
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        //Scale the object to the width and height of the screen, using the world-space values calculated earlier
        colliders.TopBoundary.transform.localScale = new Vector3(screenSize.x * 2, colThickness, colThickness);
        colliders.BottomBoundary.transform.localScale = new Vector3(screenSize.x * 2, colThickness, colThickness);
        colliders.LeftBoundary.transform.localScale = new Vector3(colThickness, screenSize.y * 2, colThickness);
        colliders.RightBoundary.transform.localScale = new Vector3(colThickness, screenSize.y * 2, colThickness);
        if (colliders.SpecialItemsSpawnZone != null) colliders.SpecialItemsSpawnZone.transform.localScale = new Vector3(screenSize.x * 2, screenSize.y * 1.6f, colThickness);
        
        //Change positions to align perfectly with outter-edge of screen, adding the world-space values of the screen we generated earlier, and adding/subtracting them with the current camera position, as well as add/subtracting half out objects size so it's not just half way off-screen
        colliders.TopBoundary.transform.position = new Vector2(cameraPos.x, cameraPos.y + screenSize.y + (colliders.TopBoundary.transform.localScale.y * 0.5f));
        colliders.BottomBoundary.transform.position = new Vector2(cameraPos.x, cameraPos.y - screenSize.y - (colliders.BottomBoundary.transform.localScale.y * 0.5f));
        colliders.LeftBoundary.transform.position = new Vector2(cameraPos.x - screenSize.x - (colliders.LeftBoundary.transform.localScale.x * 0.5f), cameraPos.y);
        colliders.RightBoundary.transform.position = new Vector2(cameraPos.x + screenSize.x + (colliders.RightBoundary.transform.localScale.x * 0.5f), cameraPos.y);
    }
}