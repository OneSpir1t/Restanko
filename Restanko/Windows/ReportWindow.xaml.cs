using System.Windows;
using System.Windows.Controls;
using Restanko.Entities;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Windows.Media.Imaging;
using System.Linq;

namespace Restanko.Windows
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private User User { get; set; } 
        public ReportWindow(User user)
        {
            InitializeComponent();
            User = user;
            Repair repair = RestankoContext.restankoContext.Repairs.OrderBy(r => r.DateOfRepair).First();
            StartDate_DatePicker.DisplayDateStart = new DateTime(repair.DateOfRepair.Year, repair.DateOfRepair.Month, repair.DateOfRepair.Day);
            var now = DateTime.Now;
            StartDate_DatePicker.DisplayDateEnd = now.AddDays(-10);
            EndDate_DatePicker.DisplayDateStart = now.AddDays(-5);
            EndDate_DatePicker.DisplayDateEnd = now;
        }

        private void CreateReport_Button_Click(object sender, RoutedEventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
           
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Pdf Files (*.pdf) | *.pdf";
            sfd.FileName = "1";
            if(StartDate_DatePicker.SelectedDate != null && EndDate_DatePicker.SelectedDate != null)
            {
                var date1 = (DateTime)StartDate_DatePicker.SelectedDate;
                var date2 = (DateTime)EndDate_DatePicker.SelectedDate;
                int year1 = (int)date1.Year;
                int month1 = (int)date1.Month;
                int day1 = (int)date1.Day;
                int year2 = (int)date2.Year;
                int month2 = (int)date2.Month;
                int day2 = (int)date2.Day;
                DateOnly dateOnly1 = new DateOnly(year1, month1, day1);
                DateOnly dateOnly2 = new DateOnly(year2, month2, day2);
                string path = Environment.CurrentDirectory + "/Report/";
                List<Repair> Repairs = RestankoContext.restankoContext.Repairs.Where(r => (DateOnly)r.DateEndOfRepair <= dateOnly2 && (DateOnly)r.DateEndOfRepair >= dateOnly1).ToList();
                if (Repairs.Count != 0)
                {
                    if (sfd.ShowDialog() == true)
                    {
                        string[] files = Directory.GetFiles(path);
                        string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
                        var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                        var font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
                        Document doc = new Document(PageSize.LETTER, 40f, 40f, 60f, 60f);
                        PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                        doc.Open();
                        Paragraph p1 = new Paragraph($"Отчёт №{files.Length + 1}", font);
                        p1.Alignment = Element.ALIGN_CENTER;
                        doc.Add(p1);
                        Paragraph p3 = new Paragraph($"{date1.Day}.{date1.Month}.{date1.Year} - {date2.Day}.{date2.Month}.{date2.Year}");
                        p3.Alignment = Element.ALIGN_CENTER;
                        doc.Add(p3);
                        var now = DateTime.Now;
                        doc.Add(new Paragraph($"Отчёт от: {now.Day}.{now.Month}.{now.Year}", font));
                        doc.Add(new Paragraph($"Менеджер: {string.Join(" ", User.Surname, User.Name, User.Patryonomic)}", font));
                        PdfPTable table = new PdfPTable(3);
                        table.SpacingBefore = 10f;
                        table.SpacingAfter = 10f;
                        table.AddCell(new PdfPCell(new Phrase("№ Заказа", font)));
                        table.AddCell(new PdfPCell(new Phrase("Наименование работы", font)));
                        table.AddCell(new PdfPCell(new Phrase("Стоимость", font)));
                        int sum = 0;
                        for (int i = 0; i <= Repairs.Count - 1; i++)
                        {
                            // Создаем ячейки таблицы с заданным шрифтом
                            table.AddCell(new PdfPCell(new Phrase($"{Repairs[i].Id}", font)));
                            table.AddCell(new PdfPCell(new Phrase($"{Repairs[i].RepairType.Name}", font)));
                            table.AddCell(new PdfPCell(new Phrase($"{Repairs[i].RepairType.Cost}", font)));
                            sum += Repairs[i].RepairType.Cost;
                        }
                        table.AddCell(new PdfPCell(new Phrase("Всего заказов", font)));
                        table.AddCell(new PdfPCell(new Phrase("", font)));
                        table.AddCell(new PdfPCell(new Phrase("Итого:", font)));
                        table.AddCell(new PdfPCell(new Phrase($"{Repairs.Count}", font)));
                        table.AddCell(new PdfPCell(new Phrase("", font)));
                        table.AddCell(new PdfPCell(new Phrase($"{sum}", font)));
                        doc.Add(table);
                        Paragraph p4 = new Paragraph("Менеджер:____________", font);
                        p4.Alignment = Element.ALIGN_RIGHT;
                        doc.Add(p4);
                        doc.Close();
                        MessageBox.Show("Успешно сохранено", "Уведомление");
                        Close();
                        Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                        if (File.Exists(sfd.FileName))
                            File.Copy(sfd.FileName, path + Path.GetRandomFileName() + ".pdf");
                    }
                }
                else
                {
                    MessageBox.Show("За этот период не было завершенных работ");
                }                                    
            }    
        }
    }
}
