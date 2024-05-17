using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Coffee.Views.MessageBox;

namespace Coffee.ViewModel.AdminVM.Table
{
    public partial class MainTableViewModel : BaseViewModel
    {
        #region variable
        private string _CurrentPhone;

        public string CurrentPhone
        {
            get { return _CurrentPhone; }
            set { _CurrentPhone = value; }
        }

        private string _DateBill;

        public string DateBill
        {
            get { return _DateBill; }
            set { _DateBill = value; }
        }

        #endregion

        #region ICommend
        public ICommand closeBillWindowIC { get; set; }
        public ICommand printBillIC { get; set; }

        #endregion

        #region function
        private void printBill(Window w)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FlowDocument doc = this.CreateFlowDocument();
                // Thiết lập kích thước trang in
                doc.PageHeight = 1056; // Chiều cao trang A4
                doc.PageWidth = 816; // Chiều rộng trang A4

                doc.ColumnWidth = printDialog.PrintableAreaWidth;

                IDocumentPaginatorSource idpSource = doc;
                printDialog.PrintDocument(idpSource.DocumentPaginator, "Bill");

                w.Close();

                MessageBoxCF ms = new MessageBoxCF("In bill thành công", MessageType.Accept, MessageButtons.OK);
                ms.ShowDialog();
            }
        }

        private FlowDocument CreateFlowDocument()
        {
            FlowDocument doc = new FlowDocument();
            doc.PagePadding = new Thickness(50);

            Paragraph header = new Paragraph(new Run("Hóa Đơn"))
            {
                FontSize = 30,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center
            };

            doc.Blocks.Add(header);

            Section infoSection = new Section();

            // Hàng đầu tiên
            Paragraph info1 = new Paragraph();
            info1.Inlines.Add(new Run($"Tên nhân viên: {EmployeeName}    "));
            info1.Inlines.Add(new Run($"Tên khách hàng: {CustomeName}\n"));
            info1.TextAlignment = TextAlignment.Left;
            infoSection.Blocks.Add(info1);

            // Hàng thứ hai
            Paragraph info2 = new Paragraph();
            info2.Inlines.Add(new Run($"Ngày hóa đơn: {DateBill:dd/MM/yyyy}    "));
            info2.Inlines.Add(new Run($"Tổng tiền: {TotalBill:N0} VND\n"));
            info2.TextAlignment = TextAlignment.Left;
            infoSection.Blocks.Add(info2);

            // Thêm Section vào FlowDocument
            doc.Blocks.Add(infoSection);

            System.Windows.Documents.Table table = new System.Windows.Documents.Table();
            table.CellSpacing = 0;
            table.BorderBrush = Brushes.Black;
            table.BorderThickness = new Thickness(1);

            int numberOfColumns = 5;

            double[] columnWidths = { 3, 1.5, 1.5, 2, 2}; // Tỷ lệ chiều rộng cột

            for (int i = 0; i < numberOfColumns; i++)
            {
                TableColumn column = new TableColumn();
                column.Width = new GridLength(columnWidths[i], GridUnitType.Star);
                table.Columns.Add(column);
            }

            TableRowGroup tableRowGroup = new TableRowGroup();
            table.RowGroups.Add(tableRowGroup);

            TableRow headerRow = new TableRow();
            tableRowGroup.Rows.Add(headerRow);

            string[] headers = { "Tên sản phẩm", "Kích thước", "Số lượng", "Giá tiền", "Thành tiền" };

            foreach (string headerText in headers)
            {
                TableCell cell = new TableCell(new Paragraph(new Run(headerText)))
                {
                    FontWeight = FontWeights.Bold,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    Padding = new Thickness(5)
                };
                headerRow.Cells.Add(cell);
            }

            foreach (var detailBill in DetailBillList)
            {
                TableRow row = new TableRow();
                tableRowGroup.Rows.Add(row);

                TableCell tenSPHoaDonCell = new TableCell(new Paragraph(new Run(detailBill.TenSanPham)))
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    Padding = new Thickness(5)
                };

                TableCell tenKTHoaDonCell = new TableCell(new Paragraph(new Run(detailBill.SelectedProductSize.TenKichThuoc)))
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    Padding = new Thickness(5)
                };

                TableCell soLuongCell = new TableCell(new Paragraph(new Run(detailBill.SoLuong.ToString())))
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    Padding = new Thickness(5)
                };

                TableCell giaTienCell = new TableCell(new Paragraph(new Run(detailBill.SelectedProductSize.Gia.ToString("N0"))))
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    Padding = new Thickness(5)
                };

                TableCell thanhTienCell = new TableCell(new Paragraph(new Run(detailBill.ThanhTien.ToString("N0"))))
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    Padding = new Thickness(5)
                };

                row.Cells.Add(tenSPHoaDonCell);
                row.Cells.Add(tenKTHoaDonCell);
                row.Cells.Add(soLuongCell);
                row.Cells.Add(giaTienCell);
                row.Cells.Add(thanhTienCell);
            }

            doc.Blocks.Add(table);

            return doc;
        }
        #endregion
    }
}
