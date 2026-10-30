using NUnit.Framework;

namespace ArmyRunner.Tests
{
    public sealed class GateMathTests
    {
        [TestCase(10, "+5", 15)]
        [TestCase(10, "-3", 7)]
        [TestCase(10, "x2", 20)]
        [TestCase(10, "÷2", 5)]
        [TestCase(2, "-9", 0)]
        [TestCase(100000, "x9", 99999)]
        public void AppliesValidGateOperations(int start, string gate, int expected)
        {
            Assert.That(GateMath.TryApply(start, gate, out int result), Is.True);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void RejectsDivisionByZero() => Assert.That(GateMath.TryApply(10, "/0", out _), Is.False);

        [Test]
        public void RejectsUnknownOperation() => Assert.That(GateMath.TryApply(10, "%2", out _), Is.False);

        [Test]
        public void CombatResolverUsesRemainingCrowd()
        {
            Assert.That(CombatResolver.Resolve(12, 10), Is.EqualTo(CombatResult.Victory));
            Assert.That(CombatResolver.Resolve(8, 10), Is.EqualTo(CombatResult.Defeat));
            Assert.That(CombatResolver.Resolve(10, 10), Is.EqualTo(CombatResult.Draw));
        }
    }
}
