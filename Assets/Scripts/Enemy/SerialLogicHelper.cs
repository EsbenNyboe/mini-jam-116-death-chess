using UnityEngine;

namespace SfxSystem
{
    public static class SerialLogicHelper
    {
        public enum SerialLogic
        {
            Random,
            SeqRandomNoRepeat,
            SeqLinear
        }

        public static int RollNewIndex(int arrayLength, SerialLogic serialLogic, int sequenceIndex)
        {
            switch (serialLogic)
            {
                case SerialLogic.Random:
                    return Random.Range(0, arrayLength);
                case SerialLogic.SeqRandomNoRepeat:
                    return SequenceRandomNoRepeat(sequenceIndex, arrayLength);
                case SerialLogic.SeqLinear:
                    return SequenceLinear(sequenceIndex, arrayLength);
                default:
                    return sequenceIndex;
            }
        }

        private static int SequenceLinear(int sequenceIndex, int sequenceLength)
        {
            sequenceIndex++;
            if (sequenceIndex >= sequenceLength)
            {
                sequenceIndex = 0;
            }

            return sequenceIndex;
        }

        private static int SequenceRandomNoRepeat(int sequenceIndex, int sequenceLength)
        {
            int newIndex = sequenceIndex;
            int rollIndex = 0;
            int rollBudget = 10;
            while (rollIndex < rollBudget)
            {
                newIndex = Random.Range(0, sequenceLength);
                rollIndex++;
                if (newIndex != sequenceIndex)
                {
                    rollIndex = rollBudget;
                }
            }

            if (newIndex == sequenceIndex)
            {
                int upOrDown = Random.Range(0, 2);
                if (upOrDown == 1)
                {
                    newIndex += 1;
                    if (newIndex >= sequenceLength)
                        newIndex = 0;
                }
                else
                {
                    newIndex -= 1;
                    if (newIndex < 0)
                        newIndex = sequenceLength - 1;
                }
            }

            return newIndex;
        }
    }
}