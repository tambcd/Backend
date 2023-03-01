using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.DL.Interface;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.BLL.Services
{
    public class AssetBLL : BaseBLL<fixed_asset>, IAssetBLL
    {
        IAssetRepository Iassetrepository;
        public AssetBLL(IAssetRepository _iassetrepository) : base(_iassetrepository)
        {
            Iassetrepository = _iassetrepository;
        }

        public Stream ExportAssets()
        {
            return GenerateExcelFileAsync((List<fixed_asset>)Iassetrepository.GetAll());

        }

        public Stream GenerateExcelFileAsync(List<fixed_asset> fixed_asset)
        {
            string excelName = $"DanhSanhTaiSan-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            MemoryStream stream = new MemoryStream();
            var headers = new List<string>()
            {
                "STT",
                "Mã tài sản",
                "Tên tài sản",
                "Mã loại tài sản",
                "Tên loại tài sản",
                "Mã bộ phận sử dụng",
                "Tên bộ phận sử dụng",
                "Số lượng",
                "Nguyên giá",
                "HM/KH lũy kế",
                "Giá trị còn lại",
               
            };
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("My Sheet");
                for (int i = 1; i <= headers.Count; i++)
                {
                    sheet.Column(i).AutoFit();
                    sheet.Cells[3, i].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[3, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    sheet.Cells[3, i].Style.Fill.BackgroundColor.SetColor(color: System.Drawing.Color.FromArgb(30988504));
                    sheet.Cells[3, i].Value = headers[i - 1];
                }

                int row = 4;
                int index = 1;
                foreach (var i in fixed_asset)
                {
                    sheet.Cells[row, 1].Value = index;
                    sheet.Cells[row, 2].Value = i.fixed_asset_code;
                    sheet.Cells[row, 3].Value = i.fixed_asset_name;
                    sheet.Cells[row, 4].Value = i.fixed_asset_category_code;
                    sheet.Cells[row, 5].Value = i.fixed_asset_category_name;
/*                    sheet.Cells[row, 5].Value = DateToString(i.DateOfBirth);
*/                  sheet.Cells[row, 6].Value = i.department_code ?? "";
                    sheet.Cells[row, 7].Value = i.department_name ?? "";
                    sheet.Cells[row, 8].Value = i.quantity.ToString();
                    sheet.Cells[row, 9].Value = i.cost.ToString() ;
                    sheet.Cells[row, 10].Value = i.depreciation_value.ToString();
                    sheet.Cells[row, 11].Value = (i.cost  - i.depreciation_value).ToString();
/*                    sheet.Cells[row, 11].Value = DateToString(i.IdenbtyDate);
*/                  
                    index++;
                    row++;
                }

                row = 4;
                // Style
                foreach (var i in fixed_asset)
                {
                    sheet.Cells[row, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    /*sheet.Cells[row, 12].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 13].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 14].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 15].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[row, 16].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
*/
                    row++;
                }
                for (var i = 1; i <= headers.Count; i++)
                {
                    sheet.Column(i).AutoFit();
                }
                package.Save();
            }


            return stream;
        }

        public string GetNewCodeSevice()
        {
            var newcode = Iassetrepository.GetNewCodeAssets();
            if (newcode != null)
            {


                string resultString = Regex.Match(newcode, @"\d+").Value;

                return "TS" + (Int32.Parse(resultString) + 1).ToString();


            }
            return "TS1";
        }

        /// <summary>
        /// Chuyển date sang string
        /// </summary>
        /// <param name="date">date</param>
        /// <returns>dd/mm/dd</returns>
        private string DateToString(DateTime? date)
        {
            if (date == null)
            {
                return "";
            }
            else
            {
                return "" + date.Value.Day.ToString() + "/" + date.Value.Month.ToString() + "/" + date.Value.Year.ToString();
            }
        }
    }
}
