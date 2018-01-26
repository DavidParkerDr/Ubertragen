using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transmission
{
    

    public class SoundManager
    {
        private Dictionary<string, SoundEffect> mSoundEffects = null;

        public SoundManager()
        {
            mSoundEffects = new Dictionary<string, SoundEffect>();

        }

        public void Add(string pName)
        {
            SoundEffect soundEffect = Transmission.Instance().CM().Load<SoundEffect>(pName);
            mSoundEffects.Add(pName, soundEffect);
        }

        public SoundEffectInstance GetSoundEffectInstance(string pName)
        {
            return mSoundEffects[pName].CreateInstance();
        }
        public SoundEffectInstance GetLoopableSoundEffectInstance(string pName)
        {
            SoundEffectInstance instance =  mSoundEffects[pName].CreateInstance();
            instance.IsLooped = true;
            return instance;
        }
    }
}
