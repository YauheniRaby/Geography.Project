using AutoMapper;
using Geography.BL.ModelsDTO;
using Geography.BL.Services.Abstract;
using Geography.DAL.Enums;
using Geography.DAL.Models;
using Geography.DAL.Repositories.Abstract;
using System.Data.Entity.Spatial;
using System.Text;
using System.Text.RegularExpressions;

namespace Geography.BL.Services
{
    public class DemoSnapshotService : IDemoSnapshotService
    {
        private readonly IDemoSnapshotRepository _demoSnapshotRepository;
        private readonly IMapper _mapper;

        public DemoSnapshotService (IDemoSnapshotRepository demoSnapshotRepository, IMapper mapper)
        {
            _demoSnapshotRepository = demoSnapshotRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DemoSnapshotDTO>> GetAllAsync()
        {
            var result = await _demoSnapshotRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DemoSnapshotDTO>>(result);
        }

        public async Task<IEnumerable<DemoSnapshotDTO>> GetIntersectionsAsync(string filter)
        {
            var geography = DbGeography.FromText(filter);
            var result = await _demoSnapshotRepository.GetIntersectionsAsync(geography);
            return _mapper.Map<IEnumerable<DemoSnapshotDTO>>(result);
        }

        public Task RemoveAsync(int id)
        {
            return _demoSnapshotRepository.RemoveAsync(id);
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _demoSnapshotRepository.ExistsAsync(id);
        }

        public Task UpdateAsync(DemoSnapshotDTO demoSnapshotDTO)
        {
            var demoSnapshot = _mapper.Map<DemoSnapshot>(demoSnapshotDTO);
            return _demoSnapshotRepository.UpdateAsync(demoSnapshot);
        }

        public bool Validation(string filter)
        {
            var regex = new Regex(Constants.polygonTemplate);
            if (!regex.IsMatch(filter))
                return false;

            var numbers = new List<double>();
            var number = new StringBuilder();
            foreach (var e in filter)
            {
                if (Char.IsDigit(e) || e == '.')
                {
                    number.Append(e);
                    continue;
                }
                if ((e == ' ' || e == ')') && number.Length != 0)
                {
                    var result = number.ToString().Replace('.', ',');
                    numbers.Add(Convert.ToDouble(result));
                    number.Clear();
                }
            }

            for (int i = 0; i < numbers.Count; i++)
                if ((i % 2 == 0 && numbers[i] > 180 || numbers[i] < -180) || (i % 2 != 0 && numbers[i] > 90 || numbers[i] < -90))
                    return false;

            return numbers[0] == numbers[numbers.Count - 2]
                    && numbers[1] == numbers[numbers.Count - 1];
        }

        public string[] GetSputnicArr()
        {
            return Enum.GetNames(typeof(Sputnic));
        }
    }
}
