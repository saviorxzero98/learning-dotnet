namespace FileSignatureValidation.Test
{
    public class FileSignatureValidatorTest
    {
        [Fact]
        public void TestIsPngFile()
        {
            // Arrange
            var fileName = "file_example_PNG_500kB.png";
            var fileData = File.ReadAllBytes($"Files\\{fileName}");

            // Act
            var isValidFileExtension = FileSignatureValidator.IsValidFileExtension(fileName, fileData);

            // Assert
            Assert.True(isValidFileExtension);
        }

        [Fact]
        public void TestIsJpgFile()
        {
            // Arrange
            var fileName = "file_example_JPG_100kB.jpg";
            var fileData = File.ReadAllBytes($"Files\\{fileName}");

            // Act
            var isValidFileExtension = FileSignatureValidator.IsValidFileExtension(fileName, fileData);

            // Assert
            Assert.True(isValidFileExtension);
        }

        [Fact]
        public void TestIsWebPFile()
        {
            // Arrange
            var fileName = "file_example_WEBP_50kB.webp";
            var fileData = File.ReadAllBytes($"Files\\{fileName}");

            // Act
            var isValidFileExtension = FileSignatureValidator.IsValidFileExtension(fileName, fileData);

            // Assert
            Assert.True(isValidFileExtension);
        }

        [Fact]
        public void TestIsSvgFile()
        {
            // Arrange
            var fileName = "file_example_SVG_20kB.svg";
            var fileData = File.ReadAllBytes($"Files\\{fileName}");

            // Act
            var isValidFileExtension = FileSignatureValidator.IsValidFileExtension(fileName, fileData);

            // Assert
            Assert.True(isValidFileExtension);
        }
    }
}
