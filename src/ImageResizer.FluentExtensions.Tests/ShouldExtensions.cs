using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
    #region Aliases to NUnit.Framework classes to improve our BDD syntax
    public class Be : Is { public Be() { } }
    public class Have : Has { public Have() { } }
    public class Contain : Contains { public Contain() { } }
    #endregion

    /// <summary>
    /// Simple extension methods allowing us to use NUnit constraints as: "foo".Should(Be.StringContaining("o"));
    /// </summary>
    /// <remarks>
    /// ShouldExtensions.Should and ShouldExtensions.ShouldNot are the only methods that are really required 
    /// to give us Should() syntax with NUnit.  We also add a number of Should*() helper methods, however, 
    /// so you can say things like list.ShouldContain("rover") instead of list.Should(Contain.Item("rover"))
    /// </remarks>
    public static partial class ShouldExtensions
    {
        public static void Should(this object o, IResolveConstraint constraint)
        {
            Assert.That(o, constraint);
        }
        public static void ShouldNot(this object o, Constraint constraint)
        {
            Assert.That(o, new NotOperator().ApplyPrefix(constraint));
        }
    }

    // NUnit.Should setups up our .Should() syntax
    //
    // These are additional .Should*() helper methods to help us 
    // write specs that may be easier to read
    public static partial class ShouldExtensions
    {

        public static void ShouldEqual<T>(this T a, T b)
        {
            a.Should(Be.EqualTo(b));
        }

        public static void ShouldNotEqual<T>(this T a, T b)
        {
            a.ShouldNot(Be.EqualTo(b));
        }

        public static void ShouldNotBeNull<T>(this T item)
        {
            item.ShouldNot(Be.Null);
        }

        public static void ShouldBeNull<T>(this T item)
        {
            item.Should(Be.Null);
        }

        public static void ShouldContain<T>(this IEnumerable<T> list, T item)
        {
            list.Should(Contain.Item(item));
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> list, T item)
        {
            list.ShouldNot(Contain.Item(item));
        }

        public static void ShouldContain(this string full, string part)
        {
            full.Should(Be.StringContaining(part));
        }

        public static void ShouldNotContain(this string full, string part)
        {
            full.ShouldNot(Be.StringContaining(part));
        }

        public static void ShouldBeFalse(this bool b)
        {
            b.Should(Be.False);
        }

        public static void ShouldBeTrue(this bool b)
        {
            b.Should(Be.True);
        }

        public static void ShouldBe<TType>(this object o)
        {
            o.Should(Be.TypeOf<TType>());
        }
    }
}