namespace RecepiesProject.Web.ViewModels.Recipes
{
    using AutoMapper;
    using RecepiesProject.Data.Models;
    using RecepiesProject.Services.Mapping;

    public class EditRecipeInputModel : BaseRecipeInputModel, IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, EditRecipeInputModel>()
                .ForMember(x => x.CookingTime, otp => otp.MapFrom(x => (int)x.CookingTime.TotalMinutes))
                .ForMember(x => x.PreparationTime, otp => otp.MapFrom(x => (int)x.PreparationTime.TotalMinutes));
        }
    }
}
