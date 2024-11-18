using System;

namespace Full_GRASP_And_SOLID
{
    public class RecipeAdapter : TimerClient
    {
        private Recipe recipe;

        public RecipeAdapter(Recipe recipe)
        {
            this.recipe = recipe;
        }
        public void TimeOut()
        {
            recipe.MarkAsCooked(); // Método adicional para cambiar "Cooked" en "Recipe".
        }
    }
}