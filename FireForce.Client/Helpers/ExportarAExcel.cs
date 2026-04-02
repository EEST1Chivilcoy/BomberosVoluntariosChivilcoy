using System.Reflection;
using ClosedXML.Excel;

namespace FireForce.Client.Helpers
{
    public class CrearExcel
    {
        public byte[] ExportarEnExcel<T>(IEnumerable<T> data) where T : class
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(typeof(T).Name);

                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                var tipo = typeof(T);
                var propiedades = tipo.GetProperties();

                // encabezados
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cell(1, i + 1).Value = properties[i].Name;
                }

                // celdas
                var row = 2;
                foreach (var item in data)
                {
                    for (int col = 0; col < properties.Length; col++)
                    {
                        var valor = propiedades[col].GetValue(item);
                        if (valor != null)
                        {
                            if (valor is IEnumerable<object> enumerable)
                            {
                                worksheet.Cell(row, col + 1).Value = string.Join(", ", enumerable);
                            }
                            else
                            {
                                worksheet.Cell(row, col + 1).Value = valor.ToString();
                            }
                        }
                        else
                        {
                            worksheet.Cell(row, col + 1).Value = "";
                        }
                    }
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}