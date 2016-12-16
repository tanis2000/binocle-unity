using UnityEngine;
using System.Collections.Generic;

namespace Binocle.Sprites
{
    [System.Serializable]
    public class SpriteAnimation
    {
        public string name;
        public int id;
        public int fps;
        public List<Sprite> frames = new List<Sprite>();

        // sequence code format:
        // startFrame-endFrame:time(chance)
        // time: also be set to "forever" - this will loop the sequence indefinitely
        // chance: float value from 0-1, chance that the sequence will play (if not played, it will be skipped)
        // time and chance can both be ignored, this will mean the sequence plays through once

        // sequence code examples:
        // TV: 0-1:3, 2-3:3, 4-5:4, 6-7:4, 8:3, 9:3
        // Idle animation with random fidgets: 0-59, 60-69, 10-59, 0-59(.25), 70-129(.75)
        // Jump animation with looping finish: 0-33, 20-33:forever
        public string sequenceCode;
        public string cue;

        public List<SpriteAnimationTrigger> triggers = new List<SpriteAnimationTrigger>();

        public void AddFrame(Sprite sprite)
        {
            frames.Add(sprite);
        }


    }



}

