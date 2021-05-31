namespace RecepiesProject.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecepiesProject.Services.Data;
    using RecepiesProject.Web.ViewModels.Recipes;

    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public RecipesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateRecipeInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        // add create Recipe using http post
    }
}
