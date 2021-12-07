using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020 {
    public class Day25 {
        public static void Main(string[] args) {
            var keys = File.ReadAllLines("Day25_input.txt");
            var cardPk = int.Parse(keys[0]);
            var doorPk = int.Parse(keys[1]);
            Console.WriteLine(Crypter.GetEncryptionKeyFrom2Pks(cardPk, doorPk));
        }
    }
    public class Crypter {
        public static int GetPublicKey(int loopSize) {
            var k = 1;
            while (loopSize > 0) {
                k *= 7;
                k %= 20201227;
                loopSize--;
            }
            
            // k = (prevk*7) % 20201227 = ((a * 20201227 + prevprevK) * 7) % 20201227
            // (a * 20201227 + prevprevK) % 7 = 0

            return k;
        }

        public static int GetEncryptionKey(in int publicKey, int loopSize) {
            var k = 1L;
            while (loopSize > 0) {
                k *= publicKey;
                k %= 20201227;
                loopSize--;
            }

            return (int)k;
        }
        
        public static int GetEncryptionKeyFrom2Pks(int card, int door) {
            long key = 1;
            long encKey = 1;
            while (key != card) {
                key = (key * 7) % 20201227;
                encKey = (encKey * door) % 20201227;
            }
            return (int)encKey;
        }

    }
}