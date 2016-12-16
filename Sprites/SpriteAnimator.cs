using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Binocle.Sprites
{
    public class SpriteAnimator : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public List<SpriteAnimation> animations = new List<SpriteAnimation>();

        public bool playing { get; private set; }

        public SpriteAnimation currentAnimation { get; private set; }

        public int currentFrame { get; private set; }

        [HideInInspector]
        public bool loop;
        public float speedMultiplier = 1f;

        public string playAnimationOnStart = "";

        bool looped;

        void Awake()
        {
            if (!spriteRenderer)
                spriteRenderer = GetComponent<SpriteRenderer>();

            if (playAnimationOnStart != "")
                Play(playAnimationOnStart);
        }

        void OnDisable()
        {
            playing = false;
            currentAnimation = null;
        }

        public void AddAnimation(SpriteAnimation sa)
        {
            animations.Add(sa);
        }

        public void Play(string name, bool loop = true, int startFrame = 0)
        {
            SpriteAnimation animation = GetAnimation(name);
            if (animation != null)
            {
                if (animation != currentAnimation)
                {
                    ForcePlay(name, loop, startFrame);
                }
            }
            else
            {
                Debug.LogWarning("could not find animation: " + name);
            }
        }

        public void Play(int id, bool loop = true, int startFrame = 0)
        {
            SpriteAnimation animation = GetAnimation(id);
            if (animation != null)
            {
                if (animation != currentAnimation)
                {
                    ForcePlay(animation.name, loop, startFrame);
                }
            }
            else
            {
                Debug.LogWarning("could not find animation: " + id);
            }
        }

        public void ForcePlay(string name, bool loop = true, int startFrame = 0)
        {
            SpriteAnimation animation = GetAnimation(name);
            if (animation != null)
            {
                this.loop = loop;
                currentAnimation = animation;
                playing = true;
                currentFrame = startFrame;
                spriteRenderer.sprite = animation.frames[currentFrame];
                StopAllCoroutines();
                StartCoroutine(PlayAnimation(currentAnimation));
            }
            else
            {
                Debug.LogWarning("Could not find animation: " + name);
            }
        }

        public void SlipPlay(string name, int wantFrame, params string[] otherNames)
        {
            for (int i = 0; i < otherNames.Length; i++)
            {
                if (currentAnimation != null && currentAnimation.name == otherNames[i])
                {
                    Play(name, true, currentFrame);
                    break;
                }
            }
            Play(name, true, wantFrame);
        }

        public bool IsPlaying(string name)
        {
            return (currentAnimation != null && currentAnimation.name == name);
        }

        public bool IsPlaying(int id)
        {
            return (currentAnimation != null && currentAnimation.id == id);
        }

        public SpriteAnimation GetAnimation(string name)
        {
            foreach (SpriteAnimation animation in animations)
            {
                if (animation.name == name)
                {
                    return animation;
                }
            }
            return null;
        }

        public SpriteAnimation GetAnimation(int id)
        {
            foreach (SpriteAnimation animation in animations)
            {
                if (animation.id == id)
                {
                    return animation;
                }
            }
            return null;
        }

        IEnumerator CueAnimation(string animationName, float minTime, float maxTime)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            ForcePlay(animationName, false);
        }

        IEnumerator PlayAnimation(SpriteAnimation animation)
        {
            playing = true;

            speedMultiplier = 1f;
            //Debug.Log("Playing animation: " + animation.name);

            float timer = 0f;
            float delay = 1f / (float)animation.fps;
            string cueOnComplete = "";

            if (animation.cue != null && animation.cue != "")
            {
                if (animation.cue.IndexOf(':') != -1)
                {
                    string[] dataBits = animation.cue.Trim().Split(':');

                    string animationName = dataBits[1];
                    dataBits = dataBits[0].Split('-');

                    float minTime = float.Parse(dataBits[0], System.Globalization.CultureInfo.InvariantCulture);
                    float maxTime = minTime;

                    if (dataBits.Length > 1)
                        maxTime = float.Parse(dataBits[1], System.Globalization.CultureInfo.InvariantCulture);

                    StartCoroutine(CueAnimation(animationName, minTime, maxTime));

                    loop = true;
                }
                else
                {
                    cueOnComplete = animation.cue.Trim();
                }
            }

            if (animation.sequenceCode != null && animation.sequenceCode != "")
            {
                while (true)
                {
                    string[] split = animation.sequenceCode.Split(',');
                    for (int i = 0; i < split.Length; i++)
                    {
                        string data = split[i].Substring(0, split[i].Length);
                        float duration = 0f;
                        float chance = 1f;
                        string[] dataBits;

                        if (data.IndexOf('(') != -1)
                        {
                            int startIndex = data.IndexOf('(');
                            int endIndex = data.IndexOf(')');
                            string chanceString = data.Substring(startIndex + 1, endIndex - (startIndex + 1));
                            chance = float.Parse(chanceString, System.Globalization.CultureInfo.InvariantCulture);
                            data = data.Substring(0, startIndex);
                        }

                        if (Random.value > chance)
                            continue;

                        bool readFrames = true;

                        if (data.IndexOf(':') != -1)
                        {
                            dataBits = data.Trim().Split(':');
                            if (dataBits[0] == "fps")
                            {
                                readFrames = false;
                            }
                            else if (dataBits[0] == "goto")
                            {
                                readFrames = false;
                            }
                            else if (dataBits[0] == "label")
                            {
                                readFrames = false;
                            }
                            else
                            {
                                if (dataBits.Length > 1)
                                {
                                    if (dataBits[1] == "forever")
                                        duration = -1f;
                                    else
                                        duration = float.Parse(dataBits[1], System.Globalization.CultureInfo.InvariantCulture);
                                }

                                dataBits = dataBits[0].Split('-');
                            }
                        }
                        else
                        {
                            dataBits = data.Trim().Split('-');
                        }

                        if (readFrames)
                        {
                            int startFrame = -1;
                            int endFrame = -1;

                            startFrame = int.Parse(dataBits[0], System.Globalization.CultureInfo.InvariantCulture);
                            endFrame = startFrame;

                            if (dataBits.Length > 1)
                                endFrame = int.Parse(dataBits[1], System.Globalization.CultureInfo.InvariantCulture);

                            currentFrame = startFrame;

                            //Debug.Log ("startFrame: " + startFrame + " endFrame: " + endFrame + " duration: " + duration);

                            if (duration <= 0f)
                            {
                                while (duration < 0f || currentFrame < endFrame)
                                {
                                    while (timer < delay)
                                    {
                                        timer += Time.deltaTime * speedMultiplier;
                                        yield return null;
                                    }

                                    while (timer >= delay)
                                    {
                                        timer -= delay;
                                        NextFrame(animation);
                                    }

                                    spriteRenderer.sprite = animation.frames[currentFrame];
                                }
                            }
                            else
                            {
                                while (duration > 0f)
                                {
                                    while (timer < delay)
                                    {
                                        duration -= Time.deltaTime * speedMultiplier;
                                        timer += Time.deltaTime * speedMultiplier;
                                        yield return null;
                                    }
                                    while (timer >= delay)
                                    {
                                        timer -= delay;
                                        currentFrame++;
                                        if (currentFrame > endFrame)
                                            currentFrame = startFrame;
                                    }

                                    spriteRenderer.sprite = animation.frames[currentFrame];
                                }
                            }
                        }
                    }
                    //Debug.LogWarning("cueOnComplete: " + cueOnComplete);
                    if (cueOnComplete != "")
                        ForcePlay(cueOnComplete, loop);
                }
            }
            else
            {
                while (loop || currentFrame < animation.frames.Count - 1)
                {
                    while (timer < delay)
                    {
                        timer += Time.deltaTime * speedMultiplier;
                        yield return null;
                    }

                    while (timer >= delay)
                    {
                        timer -= delay;
                        NextFrame(animation);
                    }

                    spriteRenderer.sprite = animation.frames[currentFrame];
                }
                if (cueOnComplete != "")
                    ForcePlay(cueOnComplete, loop);
            }

            currentAnimation = null;
            playing = false;
        }

        void NextFrame(SpriteAnimation animation)
        {
            looped = false;
            currentFrame++;
            foreach (SpriteAnimationTrigger animationTrigger in animation.triggers)
            {
                if (animationTrigger.frame == currentFrame)
                {
                    gameObject.SendMessageUpwards(animationTrigger.name);
                }
            }

            if (currentFrame >= animation.frames.Count)
            {
                if (loop)
                    currentFrame = 0;
                else
                    currentFrame = animation.frames.Count - 1;
            }
        }

        public int GetFacing()
        {
            return (int)Mathf.Sign(spriteRenderer.transform.localScale.x);
        }

        public void FlipTo(float dir)
        {
            if (dir < 0f)
                spriteRenderer.transform.localScale = new Vector3(-1f, 1f, 1f);
            else
                spriteRenderer.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public void FlipTo(Vector3 position)
        {
            float diff = position.x - transform.position.x;
            if (diff < 0f)
                spriteRenderer.transform.localScale = new Vector3(-1f, 1f, 1f);
            else
                spriteRenderer.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}