using Spectrophotometer.Models;
using System.Collections.Generic;

namespace Spectrophotometer.Service;

public interface IDataService
{
    LoadResult<List<MonomerMixtures>> LoadMixtures();
    LoadResult<List<UnitMonomerMixture>> LoadUnitMixture(string title);
}
