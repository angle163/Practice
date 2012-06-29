using System;
using System.Reflection;
using Practice.Extension;

namespace Practice.Web.Sample
{
    public class AttributeDemo
    {
        /// <summary>
        /// A test method of Attribute Demo.
        /// </summary>
        /// <returns>The demo generates string of result.</returns>
        public static string TestMethod()
        {
            AnimalTypeTestClass testClass = new AnimalTypeTestClass();
            var type = testClass.GetType();
            string text = string.Empty;
            // Iterate through all the methods of the class.
            foreach (MethodInfo mInfo in type.GetMethods())
            {
                // Iterate through all the Attributes of each method.
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    // Check for the AnimalType attribute.
                    if (attr.GetType() == typeof(AnimalTypeAttribute))
                    {
                        text += "Method {0} has a pet {1} attribute.\n"
                            .FormatWith(mInfo.Name, ((AnimalTypeAttribute)attr).Pet);
                    }
                }
            }
            /*
             * Output:
             * Method DogMethod has a pet Dog attribute.
             * Method CatMethod has a pet Cat attribute.
             * Method BirdMethod has a pet Bird attribute.
             */
            return text;
        }
    }

    /// <summary>
    /// A test class whene each method has its own pet.
    /// </summary>
    public class AnimalTypeTestClass
    {
        [AnimalType(Animal.Dog)]
        public void DogMethod()
        {
        }

        [AnimalType(Animal.Cat)]
        public void CatMethod() { }

        [AnimalType(Animal.Bird)]
        public void BirdMethod() { }
    }

    /// <summary>
    /// A custom attribute to allow a target to have a pet.
    /// </summary>
    public sealed class AnimalTypeAttribute : Attribute
    {
        /// <summary>
        /// Keep a variable internally.
        /// </summary>
        private Animal _pet;

        /// <summary>
        /// The constructor is called when the attribute is set.
        /// </summary>
        /// <param name="pet"></param>
        public AnimalTypeAttribute(Animal pet)
        {
            _pet = pet;
        }

        /// <summary>
        /// Get pet type.
        /// </summary>
        public Animal Pet
        {
            get { return _pet; }
        }
    }

    /// <summary>
    /// An enumeration of animals. Start at 1 (0 = uninitialized).
    /// </summary>
    public enum Animal
    {
        //Pets.
        Dog = 1,
        Cat = 2,
        Bird = 3,
    }
}