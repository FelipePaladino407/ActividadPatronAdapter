//-------------------------------------------------------------------------
// <copyright file="Recipe.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;   // Importante para la consigna. Get público y set privado, en la propiedad de solo lectura bool Coocked

namespace Full_GRASP_And_SOLID
{
    public class Recipe : IRecipeContent // Modificado por DIP
    {

        private bool cooked = false;
        public bool Cooked
        {
            get => this.cooked;   // No lo coloco un "set", propio de que la propiedad cooked es de solo lectura.
        }

        public void Cook()
        {
            RecipeAdapter bota = new RecipeAdapter(this);
            CountdownTimer countdown = new CountdownTimer();
            countdown.Register(GetCookTime(), bota);
        }

        public int GetCookTime()
        {
            int totalCookTime = 0;
            foreach (BaseStep step in this.steps)
            {
                totalCookTime += step.Time;
            }
            return totalCookTime;
        }

        internal void MarkAsCooked()   
        {
            this.cooked = true;
        }
        
        
        // Cambiado por OCP
        private IList<BaseStep> steps = new List<BaseStep>();

        public Product FinalProduct { get; set; }

        // Agregado por Creator
        public void AddStep(Product input, double quantity, Equipment equipment, int time)
        {
            Step step = new Step(input, quantity, equipment, time);
            this.steps.Add(step);
        }

        // Agregado por OCP y Creator
        public void AddStep(string description, int time)
        {
            WaitStep step = new WaitStep(description, time);
            this.steps.Add(step);
        }

        public void RemoveStep(BaseStep step)
        {
            this.steps.Remove(step);
        }

        // Agregado por SRP
        public string GetTextToPrint()
        {
            string result = $"Receta de {this.FinalProduct.Description}:\n";
            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetTextToPrint() + "\n";
            }

            // Agregado por Expert
            result = result + $"Costo de producción: {this.GetProductionCost()}";

            return result;
        }

        // Agregado por Expert
        public double GetProductionCost()
        {
            double result = 0;

            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetStepCost();
            }

            return result;
        }
    }
}