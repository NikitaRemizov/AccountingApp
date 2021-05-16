using AccountingApp.BLL.Utils;
using Xunit;

namespace AccountingApp.Tests.Utils
{
    public class PasswordTests
    {
        const string PasswordString = "string";

        [Fact]
        public void Password_PasswordsWithSameStringAndSalt_PasswordsStoredHashesAreSame()
        {
            var password1 = new Password(PasswordString);
            var password2 = new Password(PasswordString, password1.Hash);
            Assert.Equal(password1.Hash, password2.Hash);
        }

        [Fact]
        public void Password_TwoSamePasswordStringsForTwoPasswords_DifferentStoredHashes()
        {
            var password1 = new Password(PasswordString);
            var password2 = new Password(PasswordString);
            Assert.NotEqual(password1.Hash, password2.Hash);
        }
    }
}
