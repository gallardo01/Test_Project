using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name
{
    public static int currentIndex;
    public static List<string> names = new List<string>
       {
            "John",
            "Emma",
            "Liam",
            "Olivia",
            "Noah",
            "Ava",
            "Lucas",
            "Mia",
            "Ben",
            "Sophia",
            "James",
            "Charlotte",
            "Elijah",
            "Emily",
            "Henry",
            "Grace",
            "David",
            "Lily",
            "Leo",
            "Anna",
            "Sam",
            "Chloe",
            "Adam",
            "Zoe",
            "Alex",
            "Ella",
            "Max",
            "Nora",
            "Leo",
            "Ivy",
            "Tom",
            "Maya",
            "Jack",
            "Ruby",
            "Owen",
            "Zara",
            "Luke",
            "Luna",
            "Jake",
            "Isla",
            "Andrew",
            "Aria",
            "Eva",
            "Lena",
            "Leo",
            "Zoe",
            "Ian",
            "Eva",
            "Eli",
            "Mila",
            "Zack",
            "Sara",
            "Ryan",
            "Amy",
            "Eric",
            "Nina",
            "Mark",
            "Rose",
            "Alex",
            "Elle",
            "Sean",
            "Kate",
            "Josh",
            "Bella",
            "Max",
            "Lena",
            "Cole",
            "Emma",
            "Will",
            "Leah",
            "Finn",
            "Lucy",
            "Nate",
            "Nina",
            "Jace",
            "Jade",
            "Ray",
            "Zoe",
            "Ty",
            "Ava",
            "Blake",
            "Ella",
            "Mason",
            "Lily",
            "Zeke",
            "Mila",
            "Kai",
            "Sofia",
            "Joel",
            "Zara",
            "Evan",
            "Luna",
            "Ash",
            "Isla",
            "Cole",
            "Mia",
            "Josh",
            "Zoe",
            "Alec",
            "Eva",
            "Caleb",
            "Nora",
            "Leo",
            "Ivy",
            "Milo",
            "Zara",
            "Sam",
            "Ruby",
            "Jude",
            "Zoe",
            "Max",
            "Isla",
            "Eli",
            "Nina",
            "Troy",
            "Mila",
            "Sean",
            "Rose",
            "Tom",
            "Elle",
            "Zach",
            "Kate",
            "Drew",
            "Bella",
            "Luke",
            "Lena",
            "Kane",
            "Leah",
            "Ezra",
            "Lucy",
            "Finn",
            "Nina",
            "Jace",
            "Jade",
            "Ray",
            "Zara",
            "Ty",
            "Ava",
            "Blake",
            "Ella",
            "Mason",
            "Lily",
            "Zeke",
            "Mila",
            "Kai",
            "Sofia",
            "Joel",
            "Zoe",
            "Evan",
            "Luna",
            "Ash",
            "Isla",
            "Cole",
            "Mia",
            "Josh",
            "Zoe",
            "Alec",
            "Eva"

        };
    public static string GetName()
    {
        string name = names[currentIndex];
        if (currentIndex == names.Count - 1) currentIndex = 0;
        else currentIndex += 1;
        return name;
    }

    public static void RandomIndex()
    {
        currentIndex = Random.Range(0, names.Count - 1);
    }
}
