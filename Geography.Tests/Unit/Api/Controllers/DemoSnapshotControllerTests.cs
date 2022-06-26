using Geography.Api.Controllers;
using Geography.BL.ModelsDTO;
using Geography.BL.Services.Abstract;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Geography.Tests.Unit.Api.Controllers
{
    public class DemoSnapshotControllerTests
    {
        private readonly Mock<IDemoSnapshotService> _demoSnapshotServiceMock;
        private readonly DemoSnapshotController _demoSnapshotController;
        private readonly List<DemoSnapshotDTO> _demoSnapshotDtoList;

        public DemoSnapshotControllerTests()
        {
            _demoSnapshotServiceMock = new Mock<IDemoSnapshotService>();
            _demoSnapshotController = new DemoSnapshotController(_demoSnapshotServiceMock.Object);
            _demoSnapshotDtoList = new List<DemoSnapshotDTO>()
            {
                new DemoSnapshotDTO()
                {
                    Cloudiness = 10,
                    Coil = 5,
                    DateSnapshot = new DateTime(2022,10,11),
                    Id = 1,
                    Sputnik = "Kanopus",
                    Geography = new GeographyDTO()
                    {
                        Type = "Polygon",
                        Points = new List<double[]>
                        {
                            new double[]{10,10},
                            new double[]{20,10},
                            new double[]{20,20},
                            new double[]{10,20},
                            new double[]{10,10}
                        }
                    }
                },
                new DemoSnapshotDTO()
                {
                    Coil = 2,
                    DateSnapshot = new DateTime(2021,7,15),
                    Id = 2,
                    Sputnik = "BKA",
                    Geography = new GeographyDTO()
                    {
                        Type = "Polygon",
                        Points = new List<double[]>
                        {
                            new double[]{50,50},
                            new double[]{70,50},
                            new double[]{70,70},
                            new double[]{50,70},
                            new double[]{50,50}
                        }
                    }
                }
            };

        }

        [Fact]
        public async Task GetAllAsync_ReturnDemoSnapshotDtoList()
        {
            // Arrange
            _demoSnapshotServiceMock
                .Setup(service => service.GetAllAsync())
                .ReturnsAsync(_demoSnapshotDtoList);

            //Act
            var response = await _demoSnapshotController.GetAllAsync();

            //Assert
            _demoSnapshotServiceMock.Verify(service => service.GetAllAsync());
            Assert.IsType<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.True(new CompareLogic().Compare(_demoSnapshotDtoList, result.Value).AreEqual);
        }

        [Theory]
        [InlineData(true, StatusCodes.Status200OK)]
        [InlineData(false, StatusCodes.Status400BadRequest)]
        public async Task GetByFilterAsync_EnterFilter_HandlingValidResult(bool isValid, int statusCode)
        {
            // Arrange
            var filter = "TestFilter";

            _demoSnapshotServiceMock
                .Setup(service => service.GetIntersectionsAsync(filter))
                .ReturnsAsync(_demoSnapshotDtoList);

            _demoSnapshotServiceMock
                .Setup(service => service.Validation(filter))
                .Returns(isValid);

            //Act
            var response = await _demoSnapshotController.GetByFilterAsync(filter);

            //Assert
            _demoSnapshotServiceMock.Verify(service => service.Validation(filter));
            
            if (isValid)
            {
                Assert.IsType<OkObjectResult>(response.Result);
                var result = response.Result as OkObjectResult;
                Assert.NotNull(result);
                _demoSnapshotServiceMock.Verify(service => service.GetIntersectionsAsync(filter));
                Assert.Equal(statusCode, result.StatusCode);
                Assert.True(new CompareLogic().Compare(_demoSnapshotDtoList, result.Value).AreEqual);
            }
            else
            {
                Assert.IsType<BadRequestResult>(response.Result);
                var result = response.Result as BadRequestResult;
                Assert.NotNull(result);
                _demoSnapshotServiceMock.Verify(service => service.GetIntersectionsAsync(It.IsAny<string>()), Times.Never);
                Assert.Equal(statusCode, result.StatusCode);
            }               
        }

        [Fact]
        public void GetSputnicArr_ReturnSputnicArr()
        {
            // Arrange
            var sputnicArr = new string[] { "testValue1", "testValue2" };

            _demoSnapshotServiceMock
                .Setup(service => service.GetSputnicArr())
                .Returns(sputnicArr);

            //Act
            var response = _demoSnapshotController.GetSputnicArr();

            //Assert
            _demoSnapshotServiceMock.Verify(service => service.GetSputnicArr());
            Assert.IsType<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.True(new CompareLogic().Compare(sputnicArr, result.Value).AreEqual);
        }

        [Theory]
        [InlineData(true, StatusCodes.Status204NoContent)]
        [InlineData(false, StatusCodes.Status404NotFound)]
        public async Task RemoveAsync_EnterId_HandlingExistResult(bool isExist, int statusCode)
        {
            // Arrange
            var id = 1;

            _demoSnapshotServiceMock
                .Setup(service => service.ExistsAsync(id))
                .ReturnsAsync(isExist);

            //Act
            var response = await _demoSnapshotController.RemoveAsync(id);

            //Assert
            _demoSnapshotServiceMock.Verify(service => service.ExistsAsync(id));
            Assert.NotNull(response);
            Assert.Equal(statusCode, response.StatusCode);

            if (isExist)
                _demoSnapshotServiceMock.Verify(service => service.RemoveAsync(id));
                
            else
                _demoSnapshotServiceMock.Verify(service => service.RemoveAsync(It.IsAny<int>()), Times.Never);
        }
        
        [Theory]
        [InlineData(true, StatusCodes.Status204NoContent)]
        [InlineData(false, StatusCodes.Status404NotFound)]
        public async Task UpdateAsync_EnterDemoSnapshotDTO_HandlingExistResult(bool isExist, int statusCode)
        {
            // Arrange
            var demoSnapshotDto = _demoSnapshotDtoList.First();

            _demoSnapshotServiceMock
                .Setup(service => service.ExistsAsync(demoSnapshotDto.Id))
                .ReturnsAsync(isExist);

            //Act
            var response = await _demoSnapshotController.UpdateAsync(demoSnapshotDto);

            //Assert
            _demoSnapshotServiceMock.Verify(service => service.ExistsAsync(demoSnapshotDto.Id));
            Assert.NotNull(response);
            Assert.Equal(statusCode, response.StatusCode);

            if (isExist)
                _demoSnapshotServiceMock.Verify(service => service.UpdateAsync(demoSnapshotDto));

            else
                _demoSnapshotServiceMock.Verify(service => service.UpdateAsync(It.IsAny<DemoSnapshotDTO>()), Times.Never);
        }
    }
}
