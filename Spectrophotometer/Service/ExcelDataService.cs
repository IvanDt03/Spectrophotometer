using Spectrophotometer.Models;
using System.Collections.Generic;
using System;
using ClosedXML.Excel;

namespace Spectrophotometer.Service;

public class ExcelDataService : IDataService
{
    private string _pathFile;

    public ExcelDataService(string pathFile)
    {
        _pathFile = pathFile;
    }
    public LoadResult<List<MixtureMonomers>> LoadMixtures()
    {
        try
        {
            var result = new List<MixtureMonomers>();
            using XLWorkbook wb = new XLWorkbook(_pathFile);
            
            foreach(var ws in wb.Worksheets)
            {
                string nameFirstMonomer = ws.Cell("D19").GetString();
                string nameSecondMonomer = ws.Cell("D20").GetString();
                string title = ws.Name;
                double lambdaMin = ws.Cell("A25").GetDouble();
                double lambdaMax = ws.Cell("B25").GetDouble();
                double lambdaA = ws.Cell("C25").GetDouble();
                double wFactor = ws.Cell("D25").GetDouble();

                result.Add(new MixtureMonomers(title, nameFirstMonomer, nameSecondMonomer, lambdaMin, lambdaMax, lambdaA, wFactor));
            }
            return LoadResult<List<MixtureMonomers>>.Seccess(result);
        }
        catch(Exception ex)
        {
            return LoadResult<List<MixtureMonomers>>.Failure($"Ошибка считвания данных из Excel-файла\n" +
                $"Ошибка может быть в ячейках: D19, D20, A25, B25, C25, D25" +
                $"\n{ex.Message}");
        }
    }

    public LoadResult<List<RatioMonomers>> LoadUnitMixture(MixtureMonomers mixture)
    {
        try
        {
            var result = new List<RatioMonomers>();
            using XLWorkbook wb = new XLWorkbook(_pathFile);
            int columnFirstMonomer = 1;
            int columnSecondMonomer = 2;
            int columnSignalFactor = 12;

            var ws = wb.Worksheet(mixture.Title);
            var row = ws.FirstRowUsed().RowBelow();
            
            while (!row.FirstCell().IsEmpty())
            {
                double volumeFirst = row.Cell(columnFirstMonomer).GetDouble();
                double volumeSecond = row.Cell(columnSecondMonomer).GetDouble();
                double signalFactor = row.Cell(columnSignalFactor).GetDouble();

                result.Add(new RatioMonomers(mixture, volumeFirst, volumeSecond, signalFactor));
                row = row.RowBelow();
            }

            return LoadResult<List<RatioMonomers>>.Seccess(result);
        }
        catch(Exception ex)
        {
            return LoadResult<List<RatioMonomers>>.Failure($"Ошибка считвания данных из Excel-файла\n{ex.Message}");
        }
    }
}
