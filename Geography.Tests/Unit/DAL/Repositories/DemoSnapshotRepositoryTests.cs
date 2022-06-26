using Dapper;
using Geography.DAL.Enums;
using Geography.DAL.Models;
using Geography.DAL.Repositories;
using Geography.DAL.Repositories.Abstract;
using Moq;
using Moq.Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Geography.Tests.Unit.DAL.Repositories
{
    public class DemoSnapshotRepositoryTests
    {
        private readonly Mock<ISqlServerConnectionProvider> _sqlServerConnectionProviderMock;
        private readonly Mock<DbConnection> _dbConnectionMock;
        private readonly List<DemoSnapshot> _demoSnapshotList;
        private readonly DemoSnapshotRepository _demoSnapshotRepository;

        public DemoSnapshotRepositoryTests()
        {
            _dbConnectionMock = new Mock<DbConnection>();
            _sqlServerConnectionProviderMock = new Mock<ISqlServerConnectionProvider>();
            _demoSnapshotRepository = new DemoSnapshotRepository(_sqlServerConnectionProviderMock.Object);            
            
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
            
            _sqlServerConnectionProviderMock
                .Setup(provider => provider.GetDbConnection())
                .Returns(_dbConnectionMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnedDemoSnapshotList_Success()
        {
            // Arrange
            var query = "SELECT * FROM DemoSnapshots";
            _dbConnectionMock.SetupDapperAsync(c => c.QueryAsync<DemoSnapshot>(query, null, null, null, null))
                      .ReturnsAsync(_demoSnapshotList);
            
            //Act
            var result = await _demoSnapshotRepository.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(_demoSnapshotList.Count, result.Count());
        }
    }
}
