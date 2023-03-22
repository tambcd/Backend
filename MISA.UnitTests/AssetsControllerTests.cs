using Microsoft.AspNetCore.Mvc;
using MISA.WebApi.Controllers;
using MISA.Common.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using MISA.BLL.Interface;
using MISA.DL.Interface;

namespace MISA.WebApi.UnitTests
{
  
    internal class AssetsControllerTests
    {
        [Test]
        public  void GetAssetById_ExitsAsset_ReturnsSucces()
        {
            //Arrange 
            var assetsId = new Guid("075c791c-9db6-4370-9825-152968fb0e24");
            var expResult = new ObjectResult(
                 new fixed_asset
                 {
                     fixed_asset_id = assetsId,
                     fixed_asset_code = "TS001",
                     fixed_asset_name = "IPhone"
                 });   
                
            expResult.StatusCode = 200;
            var fakeBL = Substitute.For<IAssetBLL>();
            var fakeDL = Substitute.For<IAssetRepository>();
            fakeDL.GetById(assetsId).Returns(new fixed_asset
            {
                fixed_asset_id = new Guid("075c791c-9db6-4370-9825-152968fb0e24"),
                fixed_asset_code = "TS001",
                fixed_asset_name = "IPhone"
            });
            var fakeAssetDL = new FakeAssetDL();
            var fakeAssetBl = new FakeAssetBl();
            var AssetsController = new AssetsController(fakeDL, fakeBL);
            // Act

            var actualResult = (ObjectResult)AssetsController.GetById(assetsId);

            //Assert
            Assert.AreEqual(expResult.StatusCode, actualResult.StatusCode);
            Assert.AreEqual(((fixed_asset)expResult.Value).fixed_asset_id, ((fixed_asset)actualResult.Value).fixed_asset_id);
            Assert.AreEqual(((fixed_asset)expResult.Value).fixed_asset_code, ((fixed_asset)actualResult.Value).fixed_asset_code);
            Assert.AreEqual(((fixed_asset)expResult.Value).fixed_asset_name, ((fixed_asset)actualResult.Value).fixed_asset_name);
        }

        /// <summary>
        /// insert thành công 
        /// </summary>
        [Test]
        public void InsertAsset_ReturnSucces()
        {
            //Arrange 
            var asset = new fixed_asset
            {
                fixed_asset_id = new Guid("075c791c-9db6-4370-9825-152968fb0e24"),
                fixed_asset_code = "TS001",
                fixed_asset_name = "IPhone"
            };
            var expResult = new ObjectResult(
                 new fixed_asset
                 {
                     fixed_asset_id = new Guid("075c791c-9db6-4370-9825-152968fb0e24"),
                     fixed_asset_code = "TS001",
                     fixed_asset_name = "IPhone"
                 });

            expResult.StatusCode = 201;
            var fakeBL = Substitute.For<IAssetBLL>();
            var fakeDL = Substitute.For<IAssetRepository>();
            fakeDL.Insert(asset).Returns(1);
            fakeBL.InsertSevices(asset).Returns(1);
          /*  var fakeAssetDL = new FakeAssetDL();
            var fakeAssetBl = new FakeAssetBl();*/
            var AssetsController = new AssetsController(fakeDL, fakeBL);
            // Act

            var actualResult = (ObjectResult)AssetsController.Post(asset);

            //Assert
            Assert.AreEqual(expResult.StatusCode, actualResult.StatusCode);
            Assert.AreEqual(((fixed_asset)expResult.Value).fixed_asset_id, ((fixed_asset)actualResult.Value).fixed_asset_id);
            Assert.AreEqual(((fixed_asset)expResult.Value).fixed_asset_code, ((fixed_asset)actualResult.Value).fixed_asset_code);
            Assert.AreEqual(((fixed_asset)expResult.Value).fixed_asset_name, ((fixed_asset)actualResult.Value).fixed_asset_name);
        }


        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void InsertAsset_ReturnErro()
        {
            //Arrange 
            var asset = new fixed_asset
            {
                fixed_asset_id = new Guid("075c791c-9db6-4370-9825-152968fb0e24"),
                fixed_asset_code = "TS001",
                fixed_asset_name = "IPhone"
            };
            var expResult = new BadRequestResult();

            var fakeBL = Substitute.For<IAssetBLL>();
            var fakeDL = Substitute.For<IAssetRepository>();
            fakeDL.Insert(asset).Returns(1);
            fakeBL.InsertSevices(asset).Returns(0);
            var AssetsController = new AssetsController(fakeDL, fakeBL);
            // Act

            var actualResult = (BadRequestResult)AssetsController.Post(asset);

            //Assert
            Assert.AreEqual(expResult.StatusCode, actualResult.StatusCode);
            
        }
    }
}
