using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.DL.Interface;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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
                    sheet.Cells[row, 6].Value = i.department_code ?? "";
                    sheet.Cells[row, 7].Value = i.department_name ?? "";
                    sheet.Cells[row, 8].Value = i.quantity.ToString();
                    sheet.Cells[row, 9].Value = FomatMoney(i.cost) ;
                    sheet.Cells[row, 10].Value = FomatMoney(i.depreciation_value);
                    sheet.Cells[row, 11].Value = FomatMoney(i.cost  - i.depreciation_value);

                    index++;
                    row++;
                }

                row = 4;
                // Style
                foreach (var i in fixed_asset)
                {

                    sheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    sheet.Column(8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    sheet.Column(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    sheet.Column(10).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    sheet.Column(11).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
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

        
       
        /// <summary>
        /// validate insert cho tài sản
        /// </summary>
        /// <param name="entity"> tài sản </param>
        /// <returns>   
        ///   true : hợp lệ 
        ///   false ; không hợp lệ 
        /// </returns>
        protected override bool ValidateCusrtom(fixed_asset entity)
        {
            
            // trường hợp thêm mới 
            if(entity.cost< entity.depreciation_value)            
            {
                isValidCustom = false;
                listMsgEr.Add(Common.CommonResource.GetResoureString("ValueCost"));
            }
            // nếu dữ liệu trống hoặc bằng null 
            if (!Iassetrepository.isSameCode(entity.fixed_asset_code, entity.fixed_asset_id) )
            {
                isValidCustom = false;
                listMsgEr.Add($"Mã tài sản  {Common.CommonResource.GetResoureString("SameCode")}");
            }
            return isValidCustom;
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
        /// <summary>
        /// Chuyển date sang string
        /// </summary>
        /// <param name="date">date</param>
        /// <returns>dd/mm/dd</returns>
        private string FomatMoney(double dataFormat)
        {          
                var result = "";
                var a = dataFormat.ToString().Length / 3;
                var b = dataFormat.ToString().Length % 3;
                var index = 1;
                while (a!=0)
                {
                    result +=
                    dataFormat.ToString()[dataFormat.ToString().Length - index].ToString() +
                    dataFormat.ToString()[dataFormat.ToString().Length - index - 1].ToString() +
                    dataFormat.ToString()[dataFormat.ToString().Length - index - 2].ToString() +
                      ".";
                    index += 3;
                    a--;
                }

                if (b == 0)
                {
                    result = reverse(result.Substring(0, result.Length - 1));
                }
                else
                {
                while (b!=0)
                    {
                        result += dataFormat.ToString()[b - 1];
                        b--;
                    }
                    result = reverse(result);
                }
                return result;
            
        }

        string reverse(string txt)
        {
            var strRev = "";

            for (var i = txt.Length - 1; i >= 0; i--)
            {
                strRev += txt[i];
            }

            return strRev;
        }
    }
}
