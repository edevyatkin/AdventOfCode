using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020 {
    public class Day21 {
        public static void Main(string[] args) {
            var data = File.ReadAllText("Day21_input.txt");
            var splitted = data.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var foods = new List<(HashSet<string>, List<string>)>();
            foreach (var foodStr in splitted) {
                var splittedFoodStr = foodStr.Split(" (");
                var ingridients = splittedFoodStr[0].Split(' ').ToHashSet();
                var allergents = splittedFoodStr[1][9..^1].Split(", ").ToList();
                foods.Add((ingridients, allergents));
                // Console.WriteLine(splittedFoodStr[0]);
                // Console.WriteLine(splittedFoodStr[1][9..^1]);
            }

            var remainingFoods = new List<(HashSet<string> Ingredients, List<string> Allergens)>(foods);
            var allergens = new Dictionary<string, HashSet<string>>();
            while (true) {
                Console.WriteLine("SEARCHING FOODS WITH ONE ALLERGEN");
                //foods = foods.OrderBy(f => f.Item2.Count).ToList();
                var foodsWithOneAllergen = remainingFoods
                    .Where(f => f.Allergens.Count == 1).ToList();
                foreach (var food in foodsWithOneAllergen) {
                    var allergen = food.Allergens.First();
                    if (!allergens.ContainsKey(allergen)) {
                        allergens[allergen] = new HashSet<string>(food.Ingredients);
                    }
                    else {
                        allergens[allergen].IntersectWith(food.Ingredients);
                    }

                    remainingFoods.Remove(food);
                }

                foreach (var pair in allergens) {
                    Console.WriteLine($"Allergent \"{pair.Key}\" can be in\n [{string.Join(' ', pair.Value)}]\n");
                }

                // return;

                Console.WriteLine("FILTER ALLERGEN INGREDIENTS USING OTHER FOODS");
                foreach (var allergen in allergens) {
                    foreach (var food in 
                        remainingFoods.Where(f => f.Allergens.Contains(allergen.Key))) {
                        Console.WriteLine($"Found ingredient! It is {allergen.Key}");
                        allergens[allergen.Key].IntersectWith(food.Ingredients);
                        Console.WriteLine(
                            $"Filter approved. Now allergen \"{allergen.Key}\" can be in\n [{string.Join(' ', allergen.Value)}]\n");
                    }
                }

                //return;
                
                Console.WriteLine("FILTER ALLERGEN AND INGREDIENTS !!! FROM !!! OTHER FOODS");
                bool removed = false;
                foreach (var allergen in 
                        allergens.Where(bf => bf.Value.Count == 1)) {
                    foreach (var food in 
                        remainingFoods.Where(of => of.Allergens.Contains(allergen.Key))) {
                        food.Ingredients.Remove(allergen.Value.First());
                        food.Allergens.Remove(allergen.Key);
                        removed = true;
                        Console.WriteLine($"Removing [{allergen.Value.First()}] and \"{allergen.Key}\"!");
                    }
                }
                
                Console.WriteLine("FILTER ALLERGENS BY ITSELF");
                var properAllergens = allergens.Where(bf => bf.Value.Count == 1).ToList();
                foreach (var allergen in properAllergens) {
                    foreach (var allergenToFilter in allergens.Where(a => a.Value.Count > 1)) {
                        if (!allergenToFilter.Value.Remove(allergen.Value.First())) continue;
                        Console.WriteLine($"Removing [{allergen.Value.First()}] from allergen {allergenToFilter.Key}!");
                        removed = true;
                    }
                }

                //return;
                
                foreach (var pair in allergens) {
                    Console.WriteLine($"Removing completed. Allergent \"{pair.Key}\" can be in\n [{string.Join(' ', pair.Value)}]\n");
                }

                if (!removed)
                    break;
            }

            var allAllergens = allergens.SelectMany(a => a.Value).ToHashSet();
            foods.ForEach(f => f.Item1.ExceptWith(allAllergens));
            Console.WriteLine($"Day 21 part 1: {foods.Select(f => f.Item1.Count).Sum()}");

            Console.WriteLine($"Day 21 part 2: {string.Join(',',allergens.OrderBy(a => a.Key).Select(a => a.Value.First()))}");
        }
    }
}