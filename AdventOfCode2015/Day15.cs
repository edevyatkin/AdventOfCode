using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,15)]
public class Day15 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var ingredients = ParseIngredients(input);
        var spoons = new int[ingredients.Count];
        var result1 = MakeBestCookie(ingredients,0, spoons,100);
        var result2 = MakeBestCookie(ingredients,0, spoons,100, true);
        
        return new AocDayResult(result1, result2);
    }

    long MakeBestCookie(List<Ingredient> ingredients, int curIngredient, int[] spoons, int elapsedSpoons, bool isPartTwo = false)
    {
        var result = 0L;
        if (curIngredient == ingredients.Count - 1)
        {
            spoons[curIngredient] = elapsedSpoons;
            
            var capacityScores = (long)Math.Max(0, ingredients
                .Select((ing,ix) => ing.Capacity * spoons[ix])
                .Sum());
            var durabilityScores = (long)Math.Max(0, ingredients
                .Select((ing,ix) => ing.Durability * spoons[ix])
                .Sum());
            var flavorScores = (long)Math.Max(0, ingredients
                .Select((ing,ix) => ing.Flavor * spoons[ix])
                .Sum());
            var textureScores = (long)Math.Max(0, ingredients
                .Select((ing,ix) => ing.Texture * spoons[ix])
                .Sum());

            var total = capacityScores * durabilityScores * flavorScores * textureScores;
            
            if (!isPartTwo)
                return total;

            var caloriesScores = (long)Math.Max(0, ingredients
                .Select((ing,ix) => ing.Calories * spoons[ix])
                .Sum());
            
            return caloriesScores == 500 ? total : 0;
        }
        
        for (int count = 1; count <= elapsedSpoons; count++)
        {
            spoons[curIngredient] = count;
            result = Math.Max(result, MakeBestCookie(ingredients, curIngredient + 1, spoons, elapsedSpoons - count, isPartTwo));
        }

        return result;
    }

    private List<Ingredient> ParseIngredients(string[] input)
    {
        var lst = new List<Ingredient>();
        foreach (var s in input)
        {
            var sp = s.Split(": ");
            var sp2 = sp[1].Split(", ");
            var ingr = new Ingredient
            {
                Name = sp[0],
                Capacity = int.Parse(sp2[0].Split(' ')[1]),
                Durability = int.Parse(sp2[1].Split(' ')[1]),
                Flavor = int.Parse(sp2[2].Split(' ')[1]),
                Texture = int.Parse(sp2[3].Split(' ')[1]),
                Calories = int.Parse(sp2[4].Split(' ')[1])
            };
            lst.Add(ingr);
        }

        return lst;
    }

    class Ingredient
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Durability { get; set; }
        public int Flavor { get; set; }
        public int Texture { get; set; }
        public int Calories { get; set; }
    }
}
