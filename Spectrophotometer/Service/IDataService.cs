using Spectrophotometer.Models;
using System.Collections.Generic;

namespace Spectrophotometer.Service;

public interface IDataService
{
    LoadResult<List<MixtureMonomers>> LoadMixtures();
    LoadResult<List<RatioMonomers>> LoadUnitMixture(MixtureMonomers mixture);
}
