using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace Phoenix.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true)]
    public class PhoenixTestClassAttribute : PropertyAttribute, ITestAction
    {
        public ActionTargets Targets => ActionTargets.Suite | ActionTargets.Test;

        public void AfterTest(ITest test)
        {
            throw new NotImplementedException();
        }

        public void BeforeTest(ITest test)
        {
            if (test is not TestAssembly)
            {
                var testClassType = test.Fixture.GetType();

                //// Check if the class has the 'FriendlyNameAttribute'
                //FriendlyNameAttribute classFriendlyNameAttribute = (FriendlyNameAttribute)testClassType
                //    .GetCustomAttributes(false)
                //    .FirstOrDefault(attr => attr.GetType().Name == "FriendlyNameAttribute");
                //if (classFriendlyNameAttribute != null)
                //{
                //    FriendlyNameDictionary.TestNames["suite" + test.ClassName.ToString().ToLower().Replace(".", "")] = classFriendlyNameAttribute._testName;
                //}

                // Get all methods with the Test attribute
                var testMethods = testClassType.GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(TestAttribute), false).Length > 0);

                foreach (var method in testMethods)
                {
                    string GUID = method.DeclaringType.FullName.ToLower().Replace(".", "") + method.Name.ToLower();
                    //if (!FriendlyNameDictionary.TestNames.ContainsKey(GUID))
                    //{
                    //    // Check if the method has an attribute named 'TestName'
                    //    FriendlyNameAttribute testNameAttribute = (FriendlyNameAttribute)method.GetCustomAttributes(false)
                    //        .FirstOrDefault(attr => attr.GetType().Name == "FriendlyNameAttribute");

                    //    if (testNameAttribute != null)
                    //    {
                    //        // Assuming the TestName attribute has a property called 'Name'
                    //        var testName = testNameAttribute._testName;

                    //        FriendlyNameDictionary.TestNames[GUID] = testName;
                    //    }
                    //}
                }
            }
        }
    }
}
