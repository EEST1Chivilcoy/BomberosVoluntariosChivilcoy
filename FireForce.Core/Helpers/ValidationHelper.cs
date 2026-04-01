using System.ComponentModel.DataAnnotations;

namespace Vista.Helpers
{
    public static class ValidationHelper
    {
        public static void Validar(object viewModel)
        {
            var validationContext = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool esValido = Validator.TryValidateObject(viewModel, validationContext, validationResults, validateAllProperties: true);

            if (!esValido)
            {
                string errores = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"Existen errores de validación: {Environment.NewLine}{errores}");
            }
        }
    }
}
