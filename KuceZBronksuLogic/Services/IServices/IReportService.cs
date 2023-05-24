﻿using KuceZBronksuBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuceZBronksuBLL.Services.IServices
{
	public interface IReportService
	{
		Task ReportRecipeVisitAsync(RecipeViewModel visitedRecipe, int userId);
		Task ReportUserLoginAsync(int userId);
		Task<int> GetUserIdAsync(string email);
	}
}
