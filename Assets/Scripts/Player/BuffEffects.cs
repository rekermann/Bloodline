using CardMetaData;

namespace Player
{
    public class BuffEffects
    {
        public int count;
        public string buffEffect;
        public string effectedCardType;
        public string timer;

        public BuffEffects(int count, string buffEffect, string effectedCardType, string timer)
        {
            this.count = count;
            this.buffEffect = buffEffect;
            this.effectedCardType = effectedCardType;
            this.timer = timer;
        }
    }
}