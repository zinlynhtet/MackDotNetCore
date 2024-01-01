using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.WebSockets;
using System.Collections.Generic;
using MackDotNetCore.ThemeMvcApp.Models;

namespace MackDotNetCore.ThemeMvcApp.Controllers
{
    public class HighChartsController : Controller
    {
        public IActionResult LogarithmicAxisChart()
        {
            HighChartLogarithmicAxisChartResponseModel model = new HighChartLogarithmicAxisChartResponseModel
            {
                ChartTitle = "Logarithmic Axis Demo",
                DataPoints = new List<HighChartLogarithmicAxisChartModel>
                {
                    new HighChartLogarithmicAxisChartModel { X = 1, Y = 1 },
                    new HighChartLogarithmicAxisChartModel { X = 2, Y = 2 },
                    new HighChartLogarithmicAxisChartModel { X = 3, Y = 4 },
                    new HighChartLogarithmicAxisChartModel { X = 4, Y = 8 },
                    new HighChartLogarithmicAxisChartModel { X = 5, Y = 16 },
                    new HighChartLogarithmicAxisChartModel { X = 6, Y = 32 },
                    new HighChartLogarithmicAxisChartModel { X = 7, Y = 64 },
                    new HighChartLogarithmicAxisChartModel { X = 8, Y = 128 },
                    new HighChartLogarithmicAxisChartModel { X = 9, Y = 256 },
                    new HighChartLogarithmicAxisChartModel { X = 10, Y = 512 },
                },
            };

            return View(model);
        }
    }
}

