using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace V6Soft.Common.Test
{
    public static class EnumerableAssert
    {
        public static void IsEmpty<T>(IEnumerable<T> collection)
        {
            Assert.AreEqual(0, collection.Count(), "Collection cannot be empty");
        }

        public static void IsNotEmpty<T>(IEnumerable<T> collection)
        {
            Assert.AreNotEqual(0, collection.Count(), "Collection should not be empty");
        }

        public static void Contains<T>(IEnumerable<T> collection, T cacheItem)
        {
            Assert.IsTrue(collection.Contains(cacheItem), "Collection should contain provided item");
        }

        public static void ContainsOnly<T>(IEnumerable<T> collection, params T[] items)
        {
            Assert.IsNotNull(items, "Incorrect parameter");
            Assert.AreNotEqual(0, items.Length, "Incorrect parameter");

            List<T> collectionCopy = collection.ToList();

            foreach (var item in items)
            {
                Assert.IsTrue(collectionCopy.Remove(item), "Collection should contain provided item {0}", item);
            }

            Assert.AreEqual(0, collectionCopy.Count, "Collection should contain only provided items");
        }

        public static void Contains<T>(IEnumerable<T> collection, Func<T, bool> predicate)
        {
            Assert.IsTrue(collection.Any(predicate), "Collection should meet the condition of predicate");
        }

        public static void ContainsOnly<T>(IEnumerable<T> collection, params Predicate<T>[] predicates)
        {
            Assert.IsNotNull(predicates, "Incorrect parameter");
            Assert.AreNotEqual(0, predicates.Length, "Incorrect parameter");

            List<T> collectionCopy = collection.ToList();

            foreach (var predicate in predicates)
            {
                Assert.AreEqual(1, collectionCopy.RemoveAll(predicate), "Only one element in the collection should meet the predicate");
            }

            Assert.AreEqual(0, collectionCopy.Count, "Collection should contain only items meeting the predicates");
        }

        /// <summary>
        /// Executes an equality assertion between two custom-typed values, using the asserting method
        /// specified by <paramref name="assertItemEqual"/>.
        /// A <paramref name="message" /> is show when the assertion fails.
        /// </summary>
        public static void AssertEqual<TExpected, TActual>(IEnumerable<TExpected> expected, IEnumerable<TActual> actual, Action<TExpected, TActual> assertItemEqual)
        {
            try
            {
                if (expected == null && actual == null) { return; }

                if (expected == null || actual == null)
                {
                    Assert.Fail("One of arguments is null.");
                    return;
                }

                Assert.AreEqual(expected.Count(), actual.Count());

                var expectedEnumerator = expected.GetEnumerator();
                var actualEnumerator = actual.GetEnumerator();

                while (expectedEnumerator.MoveNext())
                {
                    actualEnumerator.MoveNext();
                    assertItemEqual(expectedEnumerator.Current, actualEnumerator.Current);
                }
            }
            catch (UnitTestAssertException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail(ex.Message);
                throw ex;
            }
        }
    }
}
