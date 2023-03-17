using Microsoft.VisualStudio.TestTools.UnitTesting;
using SF2022User_NN_Lib.dll;
using System;
using System.ComponentModel;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1_IsNotNull_ActualMethod() // тестовый метод, проверяющий, что стринг массив, вызываемый из AvailablePeriods не нулевой
        {
            TimeSpan[] st = new TimeSpan[5];
            int[] d = new int[] { 60, 30, 10, 10, 40 };
            st[0] = new TimeSpan(10, 0, 0);
            st[1] = new TimeSpan(11, 0, 0);
            st[2] = new TimeSpan(15, 0, 0);
            st[3] = new TimeSpan(15, 30, 0);
            st[4] = new TimeSpan(16, 50, 0);
            TimeSpan begin = new TimeSpan(8, 0, 0), end = new TimeSpan(18, 0, 0);
            int cons = 100;

            string[] actual = Calculations.AvailablePeriods(st, d, begin, end, cons);

            Assert.IsNotNull(actual);
        }
        [TestMethod]
        public void TestMethod2_IsInstanceOfType_String() // Тестовый метод, проверяющий, что выходжной тип массив стрин
        {
            TimeSpan[] st = new TimeSpan[5];
            int[] d = new int[] { 60, 30, 10, 10, 40 };
            st[0] = new TimeSpan(10, 0, 0);
            st[1] = new TimeSpan(11, 0, 0);
            st[2] = new TimeSpan(15, 0, 0);
            st[3] = new TimeSpan(15, 30, 0);
            st[4] = new TimeSpan(16, 50, 0);
            TimeSpan begin = new TimeSpan(8, 0, 0), end = new TimeSpan(18, 0, 0);
            int cons = 100;
            string[] except = { "собака" };
            string[] actual = Calculations.AvailablePeriods(st, d, begin, end, cons);

            Assert.IsInstanceOfType(actual, typeof(string[]));

        }
        [TestMethod]
        public void TestMethod3_IsNotInstanceOfType_Uint() // Тестовый метод, проверяющий, что выходжной тип не uint
        {
            TimeSpan[] st = new TimeSpan[5];
            int[] d = new int[] { 60, 30, 10, 10, 40 };
            st[0] = new TimeSpan(10, 0, 0);
            st[1] = new TimeSpan(11, 0, 0);
            st[2] = new TimeSpan(15, 0, 0);
            st[3] = new TimeSpan(15, 30, 0);
            st[4] = new TimeSpan(16, 50, 0);
            TimeSpan begin = new TimeSpan(8, 0, 0), end = new TimeSpan(18, 0, 0);
            int cons = 100;
            string[] except = { "собака" };
            string[] actual = Calculations.AvailablePeriods(st, d, begin, end, cons);

            Assert.IsNotInstanceOfType(actual, typeof(uint));
        }
        [TestMethod]
        public void TestMethod4_IsNotInstanceOfType_Bool() // Тестовый метод, проверяющий, что выходжной тип не бул
        {
            TimeSpan[] st = new TimeSpan[5];
            int[] d = new int[] { 60, 30, 10, 10, 40 };
            st[0] = new TimeSpan(10, 0, 0);
            st[1] = new TimeSpan(11, 0, 0);
            st[2] = new TimeSpan(15, 0, 0);
            st[3] = new TimeSpan(15, 30, 0);
            st[4] = new TimeSpan(16, 50, 0);
            TimeSpan begin = new TimeSpan(8, 0, 0), end = new TimeSpan(18, 0, 0);
            int cons = 100;

            string[] actual = Calculations.AvailablePeriods(st, d, begin, end, cons);

            Assert.IsNotInstanceOfType(actual, typeof(bool));
        }
        [TestMethod]
        public void TestMethod5_IsTrue_ExceptionSimple() // Тестовый метод, проверяющий наличие исключения при простой реализации метода
        {
            TimeSpan[] st = new TimeSpan[5];
            int[] d = new int[] { 60, 30, 10, 10, 40 };
            st[0] = new TimeSpan(10, 0, 0);
            st[1] = new TimeSpan(11, 0, 0);
            st[2] = new TimeSpan(15, 0, 0);
            st[3] = new TimeSpan(15, 30, 0);
            st[4] = new TimeSpan(16, 50, 0);
            TimeSpan begin = new TimeSpan(8, 0, 0), end = new TimeSpan(18, 0, 0);
            int cons = 100;

            string[] actual = Calculations.AvailablePeriods(st, d, begin, end, cons);

            Assert.ThrowsException<AssertFailedException>(() => Assert.ThrowsException<SystemException>(() => Calculations.AvailablePeriods(st, d, begin, end, cons)));
        }

        [TestMethod]
        public void TestMethod6_IsTrue_Exception_BigInt() // Тестовый метод, проверяющий наличия исключения при большом int cons
        {
            TimeSpan[] st = new TimeSpan[5];
            int[] d = new int[] { 60, 30, 10, 10, 40 };
            st[0] = new TimeSpan(10, 0, 0);
            st[1] = new TimeSpan(11, 0, 0);
            st[2] = new TimeSpan(15, 0, 0);
            st[3] = new TimeSpan(15, 30, 0);
            st[4] = new TimeSpan(16, 50, 0);
            TimeSpan begin = new TimeSpan(8, 0, 0), end = new TimeSpan(18, 0, 0);
            int cons = 312312319;

            string[] actual = Calculations.AvailablePeriods(st, d, begin, end, cons);

            Assert.ThrowsException<AssertFailedException>(() => Assert.ThrowsException<SystemException>(() => Calculations.AvailablePeriods(st, d, begin, end, cons)));
        }
        [TestMethod]
        public void TestMethod7_IsTrue_AreNotEqual_BeginEND() // тестовый метод, проверяющий, что begin и end не равны
        {
            TimeSpan[] st = new TimeSpan[5];
            int[] d = new int[] { 60, 30, 10, 10, 40 };
            st[0] = new TimeSpan(10, 0, 0);
            st[1] = new TimeSpan(11, 0, 0);
            st[2] = new TimeSpan(15, 0, 0);
            st[3] = new TimeSpan(15, 30, 0);
            st[4] = new TimeSpan(16, 50, 0);
            TimeSpan begin = new TimeSpan(8, 0, 0), end = new TimeSpan(18, 0, 0);
            int cons = 312312319;

            string[] actual = Calculations.AvailablePeriods(st, d, begin, end, cons);

            Assert.AreNotEqual(begin, end);
        }
        [TestMethod]
        public void TestMethod8_IsTrue_ExceptionSimilar() //  тестовый метод, проверяющий на наличие эксепшна при выставлении граничных значений (все одинаковые значения)
        {
            TimeSpan[] st = new TimeSpan[5];
            int[] d = new int[] { 60, 60, 60, 60, 60 };
            st[0] = new TimeSpan(60, 60, 60);
            st[1] = new TimeSpan(60, 60, 60);
            st[2] = new TimeSpan(60, 60, 60);
            st[3] = new TimeSpan(60, 60, 60);
            st[4] = new TimeSpan(60, 60, 60);
            TimeSpan begin = new TimeSpan(60, 60, 60), end = new TimeSpan(60, 60, 60);
            int cons = 60;
            string[] actual = Calculations.AvailablePeriods(st, d, begin, end, cons);
            Assert.ThrowsException<AssertFailedException>(() => Assert.ThrowsException<SystemException>(() => Calculations.AvailablePeriods(st, d, begin, end, cons)));
        }
        [TestMethod]
        public void TestMethod9_IsTrue_ExceptionZero() // тестовый метод, проверяющий на наличие исключения при попытке к обращению элемента массива с индексом, который находится вне границ
        {
            TimeSpan[] st = new TimeSpan[31231];
            int[] d = new int[] { 0, 0, 0, 0, 0, 0 };
            st[0] = new TimeSpan(0, 0, 0);
            st[1] = new TimeSpan(0, 0, 0);
            st[2] = new TimeSpan(0, 0, 0);
            st[3] = new TimeSpan(0, 0, 0);
            st[4] = new TimeSpan(0, 0, 0);
            TimeSpan begin = new TimeSpan(0, 0, 0), end = new TimeSpan(0, 0, 0);
            int cons = 0;
            string[] actual = Calculations.AvailablePeriods(st, d, begin, end, cons);
            Assert.ThrowsException<AssertFailedException>(() => Assert.ThrowsException<IndexOutOfRangeException>(() => Calculations.AvailablePeriods(st, d, begin, end, cons)));
        }
        [TestMethod]
        public void TestMethod10_IsTrue_NotEqual() // тестовый метод на проверку неравенства двух объектов 
        {
            TimeSpan[] st = new TimeSpan[31231];
            int[] d = new int[] { 0, 0, 0, 0, 0, 0 };
            st[0] = new TimeSpan(0, 0, 0);
            st[1] = new TimeSpan(0, 0, 0);
            st[2] = new TimeSpan(0, 0, 0);
            st[3] = new TimeSpan(0, 0, 0);
            st[4] = new TimeSpan(0, 0, 0);
            TimeSpan begin = new TimeSpan(0, 0, 0), end = new TimeSpan(0, 0, 0);
            int cons = 0;
            string[] actual = Calculations.AvailablePeriods(st, d, begin, end, cons);
            Assert.AreNotEqual(0, actual);
        }

    }

}
