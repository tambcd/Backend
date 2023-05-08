using Microsoft.AspNetCore.Http;
using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.Common.Enum;
using MISA.Common.Exceptions;
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
        /// <summary>
        /// thự hiện xuất khẩu theo lọc 
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="txtSearch">từ khóa tìm kiếm </param>
        /// <param name="DepartmentId">mã phòng ban</param>
        /// <param name="AssetCategoryId">mã loại sản phẩm </param>
        /// <returns></returns>
        public Stream ExportAssets(string? txtSearch, Guid? DepartmentId, Guid? AssetCategoryId)
        {
            return GenerateExcelFileAsync((List<fixed_asset>)Iassetrepository.Getpage(txtSearch, DepartmentId, AssetCategoryId));

        }

        /// <summary>
        /// hàm tạo và  định dạng file excel 
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="fixed_asset">danh sách cần export </param>
        /// <returns></returns>
        public Stream GenerateExcelFileAsync(List<fixed_asset> fixed_asset)
        {
            // sinh tên không trùng theo ngày
            string excelName = $"DanhSanhTaiSan-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            MemoryStream stream = new MemoryStream();
            var headers = new List<string>()
            {
                Common.CommonResource.GetResoureString("STT"),
                Common.CommonResource.GetResoureString("AssetCode"),
                Common.CommonResource.GetResoureString("AssetName"),
                Common.CommonResource.GetResoureString("CategoryCode"),
                Common.CommonResource.GetResoureString("CategoryName"),
                Common.CommonResource.GetResoureString("DepartmentCode"),
                Common.CommonResource.GetResoureString("DepartmentName"),
                Common.CommonResource.GetResoureString("Number"),
                Common.CommonResource.GetResoureString("Cost"),
                Common.CommonResource.GetResoureString("DepreciationValue"),
                Common.CommonResource.GetResoureString("ResidualValue"),


            };
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("DanhSanhTaiSan");
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
                    sheet.Cells[row, 9].Value = FomatMoney(i.cost);
                    sheet.Cells[row, 10].Value = FomatMoney(i.depreciation_value);
                    sheet.Cells[row, 11].Value = FomatMoney(i.cost - i.depreciation_value);

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
                    sheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    sheet.Column(8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    sheet.Column(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    sheet.Column(10).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    sheet.Column(11).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
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
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="entity"> tài sản </param>
        /// <returns>   
        ///   true : hợp lệ 
        ///   false ; không hợp lệ 
        /// </returns>
        protected override bool ValidateCusrtom(fixed_asset entity)
        {

            // nguyên giá lớn hơn giá trị hao mòn 
            if (entity.cost < entity.depreciation_value)
            {
                isValidCustom = false;
                listMsgEr.Add(Common.CommonResource.GetResoureString("ValueCost"));
            }
            // trung mã 
            if (Iassetrepository.IsSameCode(entity.fixed_asset_code, entity.fixed_asset_id))
            {
                isValidCustom = false;
                listMsgEr.Add($" {Common.CommonResource.GetResoureString("AssetCode")} {entity.fixed_asset_code} {Common.CommonResource.GetResoureString("SameCode")}");
            }
            return isValidCustom;
        }
        protected override bool ValidateCusrtomDelete(List<Guid> guids)
        {
            if (Iassetrepository.AssetLicense(guids) == 0)
            {
                return true;
            }
            else
            {
                listMsgEr.Add($"{Iassetrepository.AssetLicense(guids).ToString()}");
                return false;
            }
        }

        /// <summary>
        /// hàm định dạng tiền 
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="dataFormat"> tiền cần format </param>
        /// <returns>dd/mm/dd</returns>
        private string FomatMoney(double dataFormat)
        {
            var result = "";
            var a = dataFormat.ToString().Length / 3;
            var b = dataFormat.ToString().Length % 3;
            var index = 1;
            while (a != 0)
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
                result = Reverse(result.Substring(0, result.Length - 1));
            }
            else
            {
                while (b != 0)
                {
                    result += dataFormat.ToString()[b - 1];
                    b--;
                }
                result = Reverse(result);
            }
            return result;

        }
        /// <summary>
        /// hàm đảo ngược chuỗi 
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="txt">chuỗi đầu vào </param>
        /// <returns></returns>
        string Reverse(string txt)
        {
            var strRev = "";

            for (var i = txt.Length - 1; i >= 0; i--)
            {
                strRev += txt[i];
            }

            return strRev;
        }

        /// <summary>
        /// thực hiện nhập khẩu 
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="formFile">file thông tin tài sản</param>
        /// <returns></returns>
        public int ImportAssets(IFormFile formFile)
        {
            try
            {
                // kiểu tra tệp rỗng 
                if (formFile == null || formFile.Length <= 0)
                {
                    listMsgEr.Add(Common.CommonResource.GetResoureString("FileExist"));
                }
                // kiểu tra tệp đúng dịnh dạng ko 

                if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    listMsgEr.Add(Common.CommonResource.GetResoureString("FileNotFormat"));
                }
                var assets = new List<fixed_asset>();
                using (var stream = new MemoryStream())
                {
                    formFile.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 4; row <= rowCount; row++)
                        {

                            var asset = new fixed_asset()
                            {

                                fixed_asset_code = ConvertObjectToString(worksheet.Cells[row, 2].Value),
                                fixed_asset_name = ConvertObjectToString(worksheet.Cells[row, 3].Value),
                                department_id = ConvertObjectToGuid(worksheet.Cells[row, 4].Value),
                                department_code = ConvertObjectToString(worksheet.Cells[row, 5].Value),
                                department_name = ConvertObjectToString(worksheet.Cells[row, 6].Value),
                                fixed_asset_category_id = ConvertObjectToGuid(worksheet.Cells[row, 7].Value),
                                fixed_asset_category_code = ConvertObjectToString(worksheet.Cells[row, 8]),
                                fixed_asset_category_name = ConvertObjectToString(worksheet.Cells[row, 9].Value),
                                purchase_date = ConvertObjectToDate(worksheet.Cells[row, 10].Value),
                                cost = ConvertObjectToNumber(worksheet.Cells[row, 11].Value),
                                quantity = ConvertObjectToNumberInt(worksheet.Cells[row, 12].Value),
                                depreciation_value = ConvertObjectToNumber(worksheet.Cells[row, 13].Value),
                                depreciation_rate = ConvertObjectToNumber(worksheet.Cells[row, 14].Value),
                                tracked_year = ConvertObjectToNumberInt(worksheet.Cells[row, 15].Value),
                                life_time = ConvertObjectToNumberInt(worksheet.Cells[row, 16].Value),
                                production_year = ConvertObjectToDate(worksheet.Cells[row, 17].Value),


                            };
                            base.listMsgEr.Clear();
                            base.Validate(asset, (int)MisaEnum.Insert);
                            ValidateCusrtom(asset);
                            asset.ListerroImport.AddRange(base.listMsgEr);
                            assets.Add(asset);


                        }
                    }
                }
                return Iassetrepository.Import(assets);

            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        /// <summary>
        /// object to int 
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>int || 0 khi null hoặc vượt quá int </returns>
        private int ConvertObjectToNumberInt(object? value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                int Number;
                if (Int32.TryParse(value.ToString(), out Number))
                {
                    return Number;
                }
                else return 0;
            }
        }
        /// <summary>
        /// object to double
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>double || 0 khi null hoặc vượt quá double</returns>
        private double ConvertObjectToNumber(object? value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                int Number;
                if (Int32.TryParse(value.ToString(), out Number))
                {
                    return Number;
                }
                else return 0;
            }
        }

        /// <summary>
        /// chuyển đối tượng sang ngày tháng 
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="value">đối tượng </param>
        /// <returns>date || date now khí null</returns>
        private DateTime ConvertObjectToDate(object? value)
        {
            if (value == null)
            {
                return DateTime.Now;
            }
            else
            {
                DateTime date;
                if (DateTime.TryParse(value.ToString(), out date))
                {
                    return date;
                }
                return DateTime.Now;
            }
        }

        /// <summary>
        /// chuyển đổi kiểu đối tượng sang dữ liệu Guid
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private Guid ConvertObjectToGuid(object value)
        {
            return new Guid(value.ToString());
        }

        /// <summary>
        ///    chuyển đổi kiểu đối tượng sang  dữ liệu dưới dạng chuỗi 
        ///    @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="value">đối tượng cần ép kiểu </param>
        /// <returns>dữ liệu dưới dạng chuỗi</returns>
        private string? ConvertObjectToString(object? value)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                return value.ToString();
            }
        }
        /// <summary>
        /// validate sửa nguyên giá taì sản 
        /// (TVTam - 26/04/03 )
        /// </summary>
        /// <param name="assetUpdateCost"></param>
        /// <returns></returns>
        public bool ValidateCustom(AssetUpdateCost assetUpdateCost)
        {
            var isValid = true;
            var properties = assetUpdateCost.GetType().GetProperties();
            // kiểm tra dữ liệu dựa vào Attribute tự định nghĩa 

            foreach (var property in properties)
            {
                var propName = property.Name;
                var value = property.GetValue(assetUpdateCost);
                var arrProNameDisplay = property.GetCustomAttributes(typeof(PropNameDisplay), false).FirstOrDefault();

                // nếu dữ liệu trống hoặc bằng null 
                if (property.IsDefined(typeof(MISARequired), false) && (value == null || value.ToString() == String.Empty))
                {
                    isValid = false;
                    propName = (arrProNameDisplay as PropNameDisplay).PropName;
                    listMsgEr.Add($"{propName} {Common.CommonResource.GetResoureString("EmptyCheck")}");
                }
            }

            return isValid;
        }
        /// <summary>
        /// Cập nhập nguyên giá 
        /// (TVTam - 26/04/03 )
        /// </summary>
        /// <param name="assetUpdateCost">đối tượng nguyên giá </param>
        /// <returns></returns>
        public int UpdateCost(AssetUpdateCost assetUpdateCost)
        {
            if (ValidateCustom(assetUpdateCost))
            {
                return Iassetrepository.UpdateCost(assetUpdateCost.idAsset, assetUpdateCost.idLicense, assetUpdateCost.cost, assetUpdateCost.new_cost);
            }
            else
            {
                throw new MISAException(Common.CommonResource.GetResoureString("InvalidInput"), listMsgEr);
            }
        }
    }

}
