using System.Collections.Generic;

namespace MarioPizza;

public class Menu : IMenu
{
    public ICollection<IPizza> AllPizzas { get; set; }

    public ICollection<string> FindPizza(IList<string> mustHaveIngredients, IList<string> wontHaveIngredients)
    {
        //New list of strings containing pizzas matching the requried ingredients.
        var matchingPizzas = new List<string>();


        string validPizzaNames = "Pizza's you would like: ";

        foreach (IPizza pizza in AllPizzas)
        {

            bool validPizza = true;

            //Checks if any of the Pizzas contains the mustHaveIngredients in ingredients list.
            foreach (string mustHaveIngredient in mustHaveIngredients)
            {
                if (!pizza.Ingredients.Contains(mustHaveIngredient))
                {
                    validPizza = false;
                    break;
                }
            }

            foreach (string wontHaveIngredient in wontHaveIngredients)
            {
                if (pizza.Ingredients.Contains(wontHaveIngredient))
                {
                    validPizza = false;
                    break;
                }
            }



            if (validPizza)
            {
                matchingPizzas.Add(pizza.Name);
                validPizzaNames = validPizzaNames + pizza.Name + ", ";
            }




        }

        if (matchingPizzas.Count > 0)
        {
            validPizzaNames = validPizzaNames.Substring(0, validPizzaNames.Length - 2) + "!";
        }
        else
        {
            validPizzaNames = "...Found no pizzas for you, Mr. picky!";
        }


        Console.WriteLine(validPizzaNames);
        //Returns the pizza matching the customer demands.
        return matchingPizzas;



    }
}
