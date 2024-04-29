using ChartKit.SkiaSharpView;
using ChartKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChartKit.SkiaSharpView.Painting.Effects;
using ChartKit.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using ChartKit.Defaults;
using System.Globalization;
using Coffee.DTOs;
using System.Windows.Input;
using Coffee.Services;
using Coffee.Views.Admin.Table;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json.Linq;
using System.Windows.Markup;

namespace Coffee.ViewModel.AdminVM.Statistic
{
    public partial class StatisticViewModel
    {
        //Biểu đồ cột x
        public Axis[] XAxes { get; set; } =
    {
        new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("dd/MM/yyyy"))
    };
        //Biểu đồ cột y
        public Axis[] YAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                     Labeler = value => string.Format(new CultureInfo("vi-VN"), "{0:C0}", value)
                }
            };
    }
}
