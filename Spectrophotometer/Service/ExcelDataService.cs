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
    public LoadResult<List<MonomerMixtures>> LoadMixtures()
    {
        try
        {
            var result = new List<MonomerMixtures>();
            using XLWorkbook wb = new XLWorkbook(_pathFile);
            
            foreach(var ws in wb.Worksheets)
            {
                string title = ws.Name;
                string nameFirstMonomer = ws.Cell("D19").GetString();
                string nameSecondMonomer = ws.Cell("D20").GetString();
                double lambdaMin = ws.Cell("A25").GetDouble();
                double lambdaMax = ws.Cell("B25").GetDouble();
                double lambdaA = ws.Cell("C25").GetDouble();
                double wFactor = ws.Cell("D25").GetDouble();

                result.Add(new MonomerMixtures(title, lambdaMin, lambdaMax, lambdaA, wFactor, nameFirstMonomer, nameSecondMonomer));
            }
            return LoadResult<List<MonomerMixtures>>.Seccess(result);
        }
        catch(Exception ex)
        {
            return LoadResult<List<MonomerMixtures>>.Failure($"Ошибка считвания данных из Excel-файла\n" +
                $"Ошибка может быть в ячейках: D19, D20, A25, B25, C25, D25" +
                $"\n{ex.Message}");
        }
    }

    public LoadResult<List<UnitMonomerMixture>> LoadUnitMixture(string title)
    {
        try
        {
            var result = new List<UnitMonomerMixture>();
            using XLWorkbook wb = new XLWorkbook(_pathFile);
            int columnFirstMonomer = 1;
            int columnSecondMonomer = 2;
            int columnSignalFactor = 12;

            var ws = wb.Worksheet(title);
            var row = ws.FirstRowUsed().RowBelow();
            
            while (!row.FirstCell().IsEmpty())
            {
                double volumeFirst = row.Cell(columnFirstMonomer).GetDouble();
                double volumeSecond = row.Cell(columnSecondMonomer).GetDouble();
                double signalFactor = row.Cell(columnSignalFactor).GetDouble();

                result.Add(new UnitMonomerMixture(volumeFirst, volumeSecond, signalFactor));
            }

            return LoadResult<List<UnitMonomerMixture>>.Seccess(result);
        }
        catch(Exception ex)
        {
            return LoadResult<List<UnitMonomerMixture>>.Failure($"Ошибка считвания данных из Excel-файла\n{ex.Message}");
        }
    }
}
