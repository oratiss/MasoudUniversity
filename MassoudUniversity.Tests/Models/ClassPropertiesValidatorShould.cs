using System;
using Xunit;
using MasoudUniversity.Models;

namespace MasoudUniversity.Tests.Models
{
    public class ClassPropertiesValidatorShould
    {
        [Fact]
        public void AcceptValidIdentifierAndLocation()
        {
            var sut = new ClassPropertiesValidator();

            Assert.True(sut.CheckValidityOfClassIdentifier("0005", "building b"));

        }

        [Theory]
        [InlineData("sara","building c")]
        [InlineData("5415","Corridor a")]
        [InlineData("jack","Theather Room")]
        public void AcceptValidIdentifierAndLocation2(string Id, string Location)
        {
            var sut = new ClassPropertiesValidator();

            Assert.False(sut.CheckValidityOfClassIdentifier(Id, Location));

        }

    }
}
