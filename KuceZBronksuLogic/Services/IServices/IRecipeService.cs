﻿using KuceZBronksuBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuceZBronksuBLL.Services.IServices
{
	public  interface IRecipeService
	{
		public Task<RecipeViewModel> GetRecipe(int Id);
		public Task<List<RecipeViewModel>> GetAllRecipies();
		public SearchViewModel CreateSearchModelWithMealTypes();
		public Task<List<RecipeViewModel>> Search(SearchViewModel model);
		public EditAndCreateViewModel GetUniqueValuesOfRecipeLists();
		public void AddRecipeFromCreateView(EditAndCreateViewModel pageModel);
		public Task<EditAndCreateViewModel> CreateEditViewModelForEdit(int id);
		public void UpdateEditedRecipe(EditAndCreateViewModel editAndCreateViewModel);
		public Task<List<RecipeViewModel>> GetThreeMostViewedRecipes();
		public void DeleteRecipe(int id);
		public Task<List<RecipeViewModel>> RecipeWaitingToBeAdd();
		public Task ChangeApprovedOfRecipe(int id);
	}
}
