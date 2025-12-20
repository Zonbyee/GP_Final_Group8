using System.Collections.Generic;
using UnityEngine;

public static class MealTable
{
    public static Dictionary<string, int> MealMap = new Dictionary<string, int>()
    {
        {"beefburger", 0}, {"porkburger", 1}, {"salmonburger", 2}, {"shrimpburger", 3}, {"lobsterburger", 4}, {"steakburger", 5},
        {"beefsandwich", 6}, {"porksandwich", 7}, {"salmonsandwich", 8}, {"shrimpsandwich", 9}, {"lobstersandwich", 10}, {"steaksandwich", 11},
        {"applesalad", 12}, {"kiwisalad", 13}, {"tomatosalad", 14}, {"pineapplesalad", 15},
        {"meatjuice", 16}, {"seafoodjuice", 17},

        {"margheritapizza", 18}, {"hawaiipizza", 19}, {"seafoodpizza", 20},
        {"rawsealandpizza", 21},

        {"grilllobimp", 22}, {"gumbo", 23}, {"doublesaucesteak", 24},
        {"chaos", 25}
    };

    public static Dictionary<int, string> OrderText = new Dictionary<int, string>()
    {
        // Burgers
        {0, "I'll have a beef burger"},
        {1, "Give me a pork burger"},
        {2, "One salmon burger, please"},
        {3, "I want a shrimp burger"},
        {4, "A lobster burger for me"},
        {5, "I'd like a steak burger"},
        
        // Sandwiches
        {6, "Beef sandwich, thanks"},
        {7, "Can I get a pork sandwich?"},
        {8, "Salmon sandwich sounds good"},
        {9, "Shrimp sandwich, please"},
        {10, "I'll take the lobster sandwich"},
        {11, "One steak sandwich"},
        
        // Salads
        {12, "An apple salad would be nice"},
        {13, "Kiwi salad for me"},
        {14, "I want a tomato salad"},
        {15, "Pineapple salad, please"},
        
        // Juices
        {16, "Can I have some juice?"},
        {17, "Can I have some juice?"},

        // Pizzas
        {18, "Classic margherita pizza"},
        {19, "I'll try the Hawaii pizza"},
        {20, "Seafood pizza sounds delicious"},
        {21, "What's this chef special?"},

        // Special dishes
        {22, "Grilled lobimp? What's that?"},
        {23, "I'll have the gumbo"},
        {24, "Double sauce steak, please"},
        {25, "Surprise me!"}
    };

    // 🔹 每一道料理的售價（自己改成你要的數字）
    public static Dictionary<int, int> MealPrice = new Dictionary<int, int>()
    {
        {0, 350},   // beefburger
        {1, 300},   // porkburger
        {2, 330},   // salmonburger
        {3, 330},   // shrimpburger
        {4, 600},  // lobsterburger
        {5, 380},  // steakburger

        {6, 330},   // beefsandwich
        {7, 280},   // porksandwich
        {8, 300},   // salmonsandwich
        {9, 300},   // shrimpsandwich
        {10, 600}, // lobstersandwich
        {11, 350}, // steaksandwich

        {12, 200},  // applesalad
        {13, 300},  // kiwisalad
        {14, 200},  // tomatosalad
        {15, 200},  // pineapplesalad

        {16, 600},  // meatjuice
        {17, 600},  // seafoodjuice

        {18, 300},  // margheritapizza
        {19, 330}, // hawaiipizza
        {20, 350}, // seafoodpizza
        {21, 600}, // rawsealandpizza

        {22, 600}, // grilllobimp
        {23, 300},  // gumbo
        {24, 320}, // doublesaucesteak

        {25, 999}  // chaos (想搞笑就超貴🤣)
    };

    public static int GetPrice(int mealId)
    {
        return MealPrice.TryGetValue(mealId, out int price) ? price : 0;
    }

    // 依照「料理名稱字串」拿價格
    public static int GetPrice(string mealName)
    {
        if (MealMap.TryGetValue(mealName, out int id))
            return GetPrice(id);
        return 0;
    }

    private static Dictionary<string, GameObject> foodprefebs = new Dictionary<string, GameObject>();
    public static GameObject GetFood(string path)
    {
        if (foodprefebs.TryGetValue(path, out GameObject pf)) return pf;

        pf = Resources.Load<GameObject>(path);
        if (pf != null) foodprefebs[path] = pf;
        return pf;
    }
}