using System;

namespace Prog2GameKühnerMehlenDavid {
    public class Anim
{

    private float timeUntilNextFrame;
    private int frameIndex;

	public Anim()
	{
        timeUntilNextFrame = 0f;
        frameIndex = 1;
	}

    public void animation(ref int x, ref int y) {
        float animationFrameTime = 1f / 25f;

        float gameFrameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        timeUntilNextFrame -= gameFrameTime;
        if(timeUntilNextFrame <= 0)
        {
            frameIndex++;
            if (frameIndex > 25) frameIndex = 1;
            timeUntilNextFrame += animationFrameTime;
        }
        if(frameIndex > 1 && frameIndex <= 5)
        {
            y = 1;
            x = frameIndex;
        }
        else if(frameIndex > 5 && frameIndex <= 10)
        {
            y = 2;
            x = frameIndex - 5;
        }
        else if(frameIndex > 10 && frameIndex <= 15)
        {
            y = 3;
            x = frameIndex - 10;
        }
        else if(frameIndex > 15 && frameIndex <= 20)
        {
            y = 4;
            x = frameIndex - 15;
        }
        else if(frameIndex > 20 && frameIndex <= 25)
        {
            y = 5;
            y = frameIndex - 20;
        }
        
    }
    }
}
