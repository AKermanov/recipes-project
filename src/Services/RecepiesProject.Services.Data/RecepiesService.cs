namespace RecepiesProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using RecepiesProject.Data.Common.Repositories;
    using RecepiesProject.Data.Models;
    using RecepiesProject.Web.ViewModels.Recipes;

    public class RecepiesService : IRecepiesService
    {
        private readonly string[] allowedExtentions = { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public RecepiesService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Ingredient> ingredientRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientRepository = ingredientRepository;
        }

        public async Task CreateRecepieAsync(CreateRecipeInputModel input, string userId, string imagePath)
        {
            var recipe = new Recipe
            {
                CategoryId = input.CategoryId,
                CookingTime = TimeSpan.FromMinutes(input.CookingTime),
                Instructions = input.Instructions,
                Name = input.Name,
                PortionsCount = input.PortionsCount,
                PreparationTime = TimeSpan.FromMinutes(input.PreparationTime),
                AddedByUserId = userId,
            };

            foreach (var inputIngredient in input.Ingredients)
            {
                var ingredient = this.ingredientRepository.All().FirstOrDefault(x => x.Name == inputIngredient.IngredientName);
                if (ingredient == null)
                {
                    ingredient = new Ingredient { Name = inputIngredient.IngredientName };
                }

                recipe.Ingredients.Add(new RecipeIngredient
                {
                    Ingredient = ingredient,
                    Quantity = inputIngredient.Quantity,
                });

                // /wwwroot/images/recipes/jhdsi-343g3h453-=g34g.jpg
                Directory.CreateDirectory($"{imagePath}/recipes/");
                foreach (var image in input.Images)
                {
                    var extension = Path.GetExtension(image.FileName).TrimStart('.');
                    if (!this.allowedExtentions.Any(x => extension.EndsWith(x)))
                    {
                        throw new Exception($"Invalid image extention {extension}!");
                    }

                    var dbImage = new Image
                    {
                        AddedByUserId = userId,
                        Extension = extension,
                    };
                    recipe.Images.Add(dbImage);

                    var physicalPath = $"{imagePath}/recipes/{dbImage.Id}.{extension}";
                    using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                    await image.CopyToAsync(fileStream);
                }

                await this.recipesRepository.AddAsync(recipe);
                await this.recipesRepository.SaveChangesAsync();
            }
        }

        public Task DeleteRecepieAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            throw new System.NotImplementedException();
        }

        public T GetById<T>(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetByIngredients<T>(IEnumerable<int> ingredientIds)
        {
            throw new System.NotImplementedException();
        }

        public int GetCount()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateRecepieAsync(int id, EditRecipeInputModel input)
        {
            throw new System.NotImplementedException();
        }
    }
}
