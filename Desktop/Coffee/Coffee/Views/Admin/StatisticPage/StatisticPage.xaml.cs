using ChartKit.SkiaSharpView;
using ChartKit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChartKit.Defaults;
using Coffee.DTOs;
using Coffee.Services;
using System.Globalization;

namespace Coffee.Views.Admin.StatisticPage
{
    /// <summary>
    /// Interaction logic for StatisticPage.xaml
    /// </summary>
    public partial class StatisticPage : Page
    {
        #region variable
        public IEnumerable<ISeries> Series;
        List<DateTimePoint> dateTimePoints = new List<DateTimePoint>();
        private ObservableCollection<BillDTO> _BillListchart;
        public ObservableCollection<BillDTO> BillListchart;
        #endregion
        public StatisticPage()
        {
            InitializeComponent();
            Loadthongtinbill();
            
        }
        public async void Loadthongtinbill()
        {
            (string label, List<BillDTO> billlist) = await BillService.Ins.getListBill();

            if (billlist != null)
            {
                BillListchart = new ObservableCollection<BillDTO>(billlist);
            }
            var groupedBills = BillListchart.GroupBy(
        b => DateTime.ParseExact(b.NgayTao, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).Date,
        b => Convert.ToInt32(b.TongTien),
        (date, totals) => new { Date = date, Total = totals.Sum() });

            foreach (var group in groupedBills)
            {
                dateTimePoints.Add(new DateTimePoint(group.Date, group.Total));
            }
            Series = new ObservableCollection<ISeries>
           {
               new LineSeries<DateTimePoint>
               {
                   Values = dateTimePoints,
                   Fill=null
               }
           };
            lineChart.Series = Series;
        }
    }
}
