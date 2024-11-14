using System;

namespace Full_GRASP_And_SOLID
{
    public class RecipeAdapter : TimerClient
    {
        private Recipe recipe;
        private CountdownTimer countdownTimer;

        public RecipeAdapter(Recipe recipe)
        {
            this.recipe = recipe;
            this.countdownTimer = new CountdownTimer();
        }

        public void StartCooking()
        {
            int cookTimeInMilliseconds = recipe.GetCookTime() * 1000;
            countdownTimer.Register(cookTimeInMilliseconds, this);
        }

        public void TimeOut()
        {
            recipe.MarkAsCooked(); // Método adicional para cambiar "Cooked" en "Recipe".
        }
    }
}