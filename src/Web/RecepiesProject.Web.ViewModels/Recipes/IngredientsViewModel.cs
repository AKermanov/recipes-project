namespace RecepiesProject.Web.ViewModels.Recipes
{
    using RecepiesProject.Data.Models;
    using RecepiesProject.Services.Mapping;

    public class IngredientsViewModel : IMapFrom<RecipeIngredient>
    {
        public string IngredientName { get; set; }

        public string Quantity { get; set; }
    }
}
