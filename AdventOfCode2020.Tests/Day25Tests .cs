using NUnit.Framework;

namespace AdventOfCode2020.Tests {
    public class Day25Tests {
        [Test]
        [TestCase(8, 5764801)]
        [TestCase(11, 17807724)]
        public void PublicKeyTest(int loopSize, int publicKey) {
            var key = Crypter.GetPublicKey(loopSize);
            Assert.AreEqual(publicKey, key);
        }
        
        [Test]
        [TestCase(5764801, 11, 14897079)]
        [TestCase(17807724, 8, 14897079)]
        public void EncryptionKeyTest(int publicKey, int loopSize, int encryptionKey) {
            var key = Crypter.GetEncryptionKey(publicKey, loopSize);
            Assert.AreEqual(encryptionKey, key);
        }
        
        [Test]
        [TestCase(5764801, 17807724, 14897079)]
        public void EncryptionKey2Test(int cardKey, int doorKey, int encKey) {
            var key = Crypter.GetEncryptionKeyFrom2Pks(cardKey, doorKey);
            Assert.AreEqual(key, key);
        }
    }
}