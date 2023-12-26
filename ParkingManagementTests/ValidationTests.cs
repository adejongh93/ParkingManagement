using ParkingManagement.Services.Services.Validations;

namespace ParkingManagementTests
{
    public class ValidationTests
    {
        [Theory]
        [InlineData("1234-asdf", false)]
        [InlineData("asdf-1234", false)]
        [InlineData("ASDFG-1234", false)]
        [InlineData("ASDF-12345", false)]
        [InlineData("tqtrtwetre-45g45", false)]
        [InlineData("th45h5hwh", false)]
        [InlineData("sfngh45hg", false)]
        [InlineData("AAAAAAAA-555555555", false)]

        [InlineData("ASDF-1234", true)]
        [InlineData("WERT-6666", true)]
        [InlineData("GHJK-3533", true)]
        [InlineData("CVBN-4444", true)]
        [InlineData("FGHJ-3244", true)]
        [InlineData("TREW-4645", true)]
        [InlineData("ERTY-8787", true)]
        [InlineData("WERT-2322", true)]
        public void IsValidLicensePlate_ShouldValidateCorrectly(string licensePlate, bool expectedResult)
        {
            // Arrange
            var validationService = new ValidationsService();

            // Act
            var isVAlid = validationService.IsValidLicensePlate(licensePlate);

            // Assert
            Assert.Equal(expectedResult, isVAlid);
        }
    }
}
