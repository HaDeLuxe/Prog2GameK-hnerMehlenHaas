using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Frames per Second Counter
/// Copied from "craftworkgames" comment -> https://stackoverflow.com/questions/20676185/xna-monogame-getting-the-frames-per-second4
/// </summary>
namespace Reggie {
    public class FrameCounter {
        public FrameCounter() {
        }

        public long totalFrames { get; private set; }
        public float totalSeconds { get; private set; }
        public float averageFramesPerSecond { get; private set; }
        public float currentFramesPerSecond { get; private set; }

        public const int MAXIMUM_SAMPLES = 100;

        private Queue<float> sampleBuffer = new Queue<float>();

        public  bool Update(float deltaTime) {
            currentFramesPerSecond = 1.0f / deltaTime;

            sampleBuffer.Enqueue(currentFramesPerSecond);

            if (sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                sampleBuffer.Dequeue();
                averageFramesPerSecond = sampleBuffer.Average(i => i);
            }
            else
            {
                averageFramesPerSecond = currentFramesPerSecond;
            }

            totalFrames++;
            totalSeconds += deltaTime;
            return true;
        }
    }
}
