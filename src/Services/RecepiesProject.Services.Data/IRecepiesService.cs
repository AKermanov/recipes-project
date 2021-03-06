namespace RecepiesProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RecepiesProject.Web.ViewModels.Recipes;

    public interface IRecepiesService
    {
        Task CreateRecepieAsync(CreateRecipeInputModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        IEnumerable<T> GetRandom<T>(int count);

        int GetCount();

        T GetById<T>(int id);

        Task UpdateRecepieAsync(int id, EditRecipeInputModel input);

        IEnumerable<T> GetByIngredients<T>(IEnumerable<int> ingredientIds);

        Task DeleteRecepieAsync(int id);
    }
}
