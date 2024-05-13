
using System;
using System.Collections.Generic;

namespace RecipeApp
{
    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double Calories { get; set; }
        public string FoodGroup { get; set; }

        public Ingredient(string name, double quantity, string unit, double calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }

    class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Instructions { get; set; }

        public Recipe(string name, List<Ingredient> ingredients, string instructions)
        {
            Name = name;
            Ingredients = ingredients;
            Instructions = instructions;
        }

        public double GetTotalCalories()
        {
            double totalCalories = 0;
            foreach (var ingredient in Ingredients)
            {
                totalCalories += ingredient.Calories * ingredient.Quantity;
            }
            return totalCalories;
        }
    }

    class Program
    {
        static List<Recipe> recipes = new List<Recipe>();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateRecipe();
                        break;
                    case "2":
                        ViewRecipes();
                        break;
                    case "3":
                        ViewRecipeFromList();
                        break;
                    case "4":
                        CalculateTotalCalories();
                        break;
                    case "5":
                        ScaleRecipe();
                        break;
                    case "6":
                        ScaleToOriginalValues();
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("Welcome to Recipe App!");
            Console.WriteLine("1. Create Recipe");
            Console.WriteLine("2. View All Recipes");
            Console.WriteLine("3. View Recipe Details");
            Console.WriteLine("4. Calculate Total Calories");
            Console.WriteLine("5. Scale a Recipe");
            Console.WriteLine("6. Scale Quantities to Original Values");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");
        }

        static void CreateRecipe()
        {
            Console.Write("Enter recipe name: ");
            string name = Console.ReadLine();

            List<Ingredient> ingredients = new List<Ingredient>();
            Console.WriteLine("Enter ingredients (one per line, type 'done' when finished):");
            string ingredientName = "";
            double ingredientQuantity = 0.0;
            string ingredientUnit = "";
            double ingredientCalories = 0.0;
            string ingredientFoodGroup = "";
            Console.Write("Ingredient name: ");
            ingredientName = Console.ReadLine();
            while (ingredientName.ToLower() != "done")
            {
                Console.Write("Quantity: ");
                ingredientQuantity = Convert.ToDouble(Console.ReadLine());
                Console.Write("Unit of measurement: ");
                ingredientUnit = Console.ReadLine();
                Console.Write("Calories: ");
                ingredientCalories = Convert.ToDouble(Console.ReadLine());
                Console.Write("Food Group: ");
                ingredientFoodGroup = Console.ReadLine();

                ingredients.Add(new Ingredient(ingredientName, ingredientQuantity, ingredientUnit, ingredientCalories, ingredientFoodGroup));

                Console.Write("Ingredient name: ");
                ingredientName = Console.ReadLine();
            }

            double totalCalories = CalculateTotalCalories(ingredients);
            if (totalCalories > 300)
            {
                Console.WriteLine("Warning: Total calorie count is above 300!");
            }

            Console.WriteLine("Enter instructions:");
            string instructions = Console.ReadLine();

            Recipe newRecipe = new Recipe(name, ingredients, instructions);
            recipes.Add(newRecipe);
            Console.WriteLine("Recipe added successfully!");
        }

        static double CalculateTotalCalories(List<Ingredient> ingredients)
        {
            double totalCalories = 0;
            foreach (var ingredient in ingredients)
            {
                totalCalories += ingredient.Calories * ingredient.Quantity;
            }
            return totalCalories;
        }

        static void ViewRecipes()
        {
            Console.WriteLine("All Recipes:");
            foreach (var recipe in recipes)
            {
                Console.WriteLine($"- {recipe.Name}");
            }
        }

        static void ViewRecipeFromList()
        {
            Console.WriteLine("Choose a recipe to view details:");
            ViewRecipes(); // Display the list of recipes

            Console.Write("Enter the name of the recipe: ");
            string recipeName = Console.ReadLine();
            Recipe recipeToDisplay = recipes.Find(r => r.Name.ToLower() == recipeName.ToLower());
            if (recipeToDisplay == null)
            {
                Console.WriteLine("Recipe not found.");
                return;
            }

            Console.WriteLine($"Name: {recipeToDisplay.Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in recipeToDisplay.Ingredients)
            {
                Console.WriteLine($"- {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
                Console.WriteLine($"  Calories per unit: {
// Continued from the previous code

                Console.WriteLine($"  Calories per unit: {ingredient.Calories}");
                Console.WriteLine($"  Food Group: {ingredient.FoodGroup}");
            }
            Console.WriteLine($"Total Calories: {recipeToDisplay.GetTotalCalories()}");
            Console.WriteLine($"Instructions:\n{recipeToDisplay.Instructions}");
        }

        static void CalculateTotalCalories()
        {
            double totalCalories = 0;
            foreach (var recipe in recipes)
            {
                totalCalories += recipe.GetTotalCalories();
            }
            Console.WriteLine($"Total Calories of All Recipes: {totalCalories}");
        }

        static void ScaleRecipe()
        {
Console.WriteLine("Enter the name of the recipe to scale:");
            string recipeName = Console.ReadLine();
            Recipe recipeToScale = recipes.Find(r => r.Name.ToLower() == recipeName.ToLower());
            if (recipeToScale == null)
            {
                Console.WriteLine("Recipe not found.");
                return;
            }

            Console.WriteLine("Enter scaling factor (0.5 for half, 2 for double, 3 for triple):");
            double scalingFactor = Convert.ToDouble(Console.ReadLine());

            List<(string, double, string)> scaledIngredients = new List<(string, double, string)>();
            foreach (var ingredient in recipeToScale.Ingredients)
            {
                double scaledQuantity = ingredient.Item2 * scalingFactor;
                scaledIngredients.Add((ingredient.Item1, scaledQuantity, ingredient.Item3));
            }

            Console.WriteLine("Scaled Recipe:");
            Console.WriteLine($"Name: {recipeToScale.Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in scaledIngredients)
            {
                Console.WriteLine($"- {ingredient.Item2} {ingredient.Item3} of {ingredient.Item1}");
            }
            Console.WriteLine("Instructions:");
            Console.WriteLine(recipeToScale.Instructions);
        }

        static void ScaleToOriginalValues()
        {
Console.WriteLine("Enter the name of the recipe to scale to original values:");
            string recipeName = Console.ReadLine();
            Recipe recipeToScale = recipes.Find(r => r.Name.ToLower() == recipeName.ToLower());
            if (recipeToScale == null)
            {
                Console.WriteLine("Recipe not found.");
                return;
            }

            Console.WriteLine("Recipe Ingredients scaled back to original values:");
            Console.WriteLine($"Name: {recipeToScale.Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in recipeToScale.OriginalIngredients)
            {
                Console.WriteLine($"- {ingredient.Item2} {ingredient.Item3} of {ingredient.Item1}");
            }
            Console.WriteLine("Instructions:");
            Console.WriteLine(recipeToScale.Instructions);
        }
    }
}


