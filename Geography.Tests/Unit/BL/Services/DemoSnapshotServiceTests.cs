using AutoMapper;
using Geography.Api.Mapper;
using Geography.BL.ModelsDTO;
using Geography.BL.Services;
using Geography.DAL.Enums;
using Geography.DAL.Models;
using Geography.DAL.Repositories.Abstract;
using KellermanSoftware.CompareNetObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Geography.Tests.Unit.BL.Services
{
    public class DemoSnapshotServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDemoSnapshotRepository> _demoSnapshotRepositoryMock;
        private readonly DemoSnapshotService _demoSnapshotService;
        private readonly List<DemoSnapshot> _demoSnapshotList;
        private readonly List<DemoSnapshotDTO> _demoSnapshotDtoList;
        private readonly int _id;
        
        public DemoSnapshotServiceTests()
        {
            _demoSnapshotRepositoryMock = new Mock<IDemoSnapshotRepository>();
            _mapper = new Mapper(MapperConfig.GetConfiguration());
            _demoSnapshotService = new DemoSnapshotService(_demoSnapshotRepositoryMock.Object, _mapper);
            _demoSnapshotList = new List<DemoSnapshot>()
            {
                new DemoSnapshot()
                {
                    Id = 1,
                    Cloudiness = 20,
                    Coil = 4,
                    DateSnapshot = new DateTime(2022,01,10),
                    Sputnik = Sputnic.Kanopus,
                    Geography = DbGeography.FromText("POLYGON((20 20, 30 20, 30 30, 20 30, 20 20))")
                },
                new DemoSnapshot()
                {
                    Id = 2,
                    Coil = 1,
                    DateSnapshot = new DateTime(2021,05,17),
                    Sputnik = Sputnic.BKA,
                    Geography = DbGeography.FromText("POLYGON((-30 -30, -20 -30, -20 -20, -30 -20, -30 -30))")
                }
            };
            _demoSnapshotDtoList = new List<DemoSnapshotDTO>()
            {
                new DemoSnapshotDTO()
                {
                    Id = 1,
                    Cloudiness = 20,
                    Coil = 4,
                    DateSnapshot = new DateTime(2022,01,10),
                    Sputnik = "Kanopus",
                    Geography = new GeographyDTO()
                    {
                        Type = "Polygon",
                        Points = new List<double[]>
                        {
                            new double[] {20, 20},
                            new double[] {30, 20},
                            new double[] {30, 30},
                            new double[] {20, 30},
                            new double[] {20, 20}
                        }
                    }
                },
                new DemoSnapshotDTO()
                {
                    Id = 2,
                    Coil = 1,
                    DateSnapshot = new DateTime(2021,05,17),
                    Sputnik = "BKA",
                    Geography = new GeographyDTO()
                    {
                        Type = "Polygon",
                        Points = new List<double[]>
                        {
                            new double[] {-30, -30},
                            new double[] {-20, -30},
                            new double[] {-20, -20},
                            new double[] {-30, -20},
                            new double[] {-30, -30}
                        }
                    }
                }
            };
            _id = 1;
        }

        [Fact]
        public async Task GetAllAsync_ReturnedDemoSnapshotDTOList_Success()
        {
            // Arrange
            _demoSnapshotRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(_demoSnapshotList);

            //Act
            var result = await _demoSnapshotService.GetAllAsync();

            //Assert
            var expected = _demoSnapshotDtoList;
            Assert.NotNull(result);
            Assert.True(new CompareLogic().Compare(expected, result).AreEqual);
        }

        [Fact]
        public async Task GetIntersectionsAsync_ReturnedDemoSnapshotDTOList_Success()
        {
            // Arrange
            var filter = "POLYGON ((-25 -25, 0 -10, 25 25, 0 10, -25 -25))";
            
            _demoSnapshotRepositoryMock
                .Setup(repo => repo.GetIntersectionsAsync(It.Is<DbGeography>(x => x.WellKnownValue.WellKnownText == filter)))
                .ReturnsAsync(_demoSnapshotList);

            //Act
            var result = await _demoSnapshotService.GetIntersectionsAsync(filter);

            //Assert
            var expected = _demoSnapshotDtoList;
            Assert.NotNull(result);
            Assert.True(new CompareLogic().Compare(expected, result).AreEqual);
        }

        [Fact]
        public async Task RemoveAsync_InvokeRemoveAsyncFromRepository_Success()
        {
            //Act
            await _demoSnapshotService.RemoveAsync(_id);

            //Assert
            _demoSnapshotRepositoryMock.Verify(repo => repo.RemoveAsync(_id));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ExistsAsync_ReturnExistValue_Success(bool isExist)
        {
            //Act
            _demoSnapshotRepositoryMock
                .Setup(repo => repo.ExistsAsync(_id))
                .ReturnsAsync(isExist);

            var result = await _demoSnapshotService.ExistsAsync(_id);

            //Assert
            _demoSnapshotRepositoryMock.Verify(repo => repo.ExistsAsync(_id));
            Assert.Equal(result, isExist);
        }

        [Fact]
        public async Task ExistsAsync_InvokeUpdateAsyncFromRepository_Success()
        {
            // Arrange
            var demoSnapshotDto = _demoSnapshotDtoList.First();

            //Act
            await _demoSnapshotService.UpdateAsync(demoSnapshotDto);

            //Assert
            var demoSnapshot = _demoSnapshotList.First();
            _demoSnapshotRepositoryMock.Verify(repo => repo.UpdateAsync(
                It.Is<DemoSnapshot>(x => 
                    x.Id == demoSnapshot.Id &&
                    x.Coil == demoSnapshot.Coil &&
                    x.DateSnapshot.ToString()==demoSnapshot.DateSnapshot.ToString() &&
                    x.Sputnik == demoSnapshot.Sputnik &&
                    x.Cloudiness == demoSnapshot.Cloudiness &&
                    x.Geography.WellKnownValue.WellKnownText == demoSnapshot.Geography.WellKnownValue.WellKnownText)));
        }

        [Fact]
        public void GetSputnicArr_ReturnedStringList_Success()
        {
            //Act
            var result = _demoSnapshotService.GetSputnicArr();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.All(result, x => It.IsAny<string>());
        }

        [Theory]
        [InlineData(true, "POLYGON((1 1, 2 1, 2 2, 1 2, 1 1))")]
        [InlineData(true, "POLYGON ((1 1, 2 1, 2 2, 1 2, 1 1))")]
        [InlineData(false, "Polygon ((1 1, 2 1, 2 2, 1 2, 1 1))")]
        [InlineData(false, "POLYGON ((1 1, 2 1, 2 2, 1 2))")]
        [InlineData(false, "POLYGON ((1 1, 2 1, 2 2, 1 2, 3 1))")]
        [InlineData(false, "POLYGON ((1 1, 2 1, 2 2, 1 2, 1 3))")]
        [InlineData(false, "POLYGON ((1 1  2 1  2 2  1 2  1 3))")]
        [InlineData(false, "POLYGON (1 1, 2 1, 2 2, 1 2, 1 3)")]
        [InlineData(false, "POLYGON((1 1, 181 1, 2 2, 1 2, 1 1))")]
        [InlineData(false, "POLYGON((1 1, -181 1, 2 2, 1 2, 1 1))")]
        [InlineData(false, "POLYGON((1 1, 3 91, 2 2, 1 2, 1 1))")]
        [InlineData(false, "POLYGON((1 1, 3 -91, 2 2, 1 2, 1 1))")]
        public void GetSputnicArr_ValidationFilter_Success(bool isValid, string filter)
        {
            //Act
            var result = _demoSnapshotService.Validation(filter);

            //Assert
            Assert.Equal(result, isValid);
        }
    }
}
