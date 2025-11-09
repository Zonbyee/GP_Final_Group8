using UnityEngine;
using System.Collections.Generic;

public static class data
{
    public class ingreds_data
    {
        public string name;
        public int quantity;

        public ingreds_data(string nn)
        {
            name = nn;
            quantity = 1;
        }
    }
    public static int val = 10;
    public static float bgmvol = 1f;
    public static List<ingreds_data> inbag = new List<ingreds_data>();

    public static void reset()
    {
        val = 10;
    }
}
