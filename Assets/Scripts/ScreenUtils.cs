using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenUtils
{
    public static Vector2 GetScreenCenter()
    {
        float screenCenterX = Screen.width * .5f;
        float screenCenterY = Screen.height * .5f;


        if (Screen.orientation == ScreenOrientation.Landscape)
        {
            return new Vector2(screenCenterX, screenCenterY);
        }
        else
        {
            return new Vector2(screenCenterY, screenCenterX);
        }


    }

}
