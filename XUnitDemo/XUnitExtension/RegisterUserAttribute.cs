using AutoFixture;
using AutoFixture.Xunit2;

namespace XUnitDemo.XUnitExtension
{
    public class RegisterUserAttribute : AutoDataAttribute
    {
        public RegisterUserAttribute() : base(() =>
        {
            var fixture = new Fixture();

            fixture.Customize<RegisterUserModel>(q => 
                q.With(q => q.Email, "test@email.com")
                .With(q => q.Password, "P@ssw0rd"));

            return fixture;
        })
        {
        }
    }
}
