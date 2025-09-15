using System;
using UnityEngine;

namespace Messages.Utils
{
    public static class WavUtil
    {
        public static byte[] AudioClipToWAV(AudioClip clip)
        {
            int sampleCount = clip.samples * clip.channels;
            float[] samples = new float[sampleCount];
            clip.GetData(samples, 0);

            // Convertir float a PCM16
            byte[] audioBytes = new byte[sampleCount * 2]; // 2 bytes por sample
            int rescaleFactor = 32767;

            for (int i = 0; i < sampleCount; i++)
            {
                float clamped = Mathf.Clamp(samples[i], -1f, 1f);
                short intSample = (short)(clamped * rescaleFactor);
                audioBytes[i * 2] = (byte)(intSample & 0xff);
                audioBytes[i * 2 + 1] = (byte)((intSample >> 8) & 0xff);
            }

            return AddWAVHeader(audioBytes, clip.frequency, clip.channels);
        }

        private static byte[] AddWAVHeader(byte[] audioData, int sampleRate, int channels)
        {
            
            int byteRate = sampleRate * channels * 2; // PCM16
            int blockAlign = channels * 2;
            int subChunk2Size = audioData.Length;
            int chunkSize = 36 + subChunk2Size;

            byte[] header = new byte[44];

            // RIFF
            Array.Copy(System.Text.Encoding.ASCII.GetBytes("RIFF"), 0, header, 0, 4);
            Array.Copy(BitConverter.GetBytes(chunkSize), 0, header, 4, 4);
            Array.Copy(System.Text.Encoding.ASCII.GetBytes("WAVE"), 0, header, 8, 4);

            // fmt subchunk
            Array.Copy(System.Text.Encoding.ASCII.GetBytes("fmt "), 0, header, 12, 4);
            Array.Copy(BitConverter.GetBytes(16), 0, header, 16, 4);        // Subchunk1Size
            Array.Copy(BitConverter.GetBytes((short)1), 0, header, 20, 2); // AudioFormat PCM
            Array.Copy(BitConverter.GetBytes((short)channels), 0, header, 22, 2);
            Array.Copy(BitConverter.GetBytes(sampleRate), 0, header, 24, 4);
            Array.Copy(BitConverter.GetBytes(byteRate), 0, header, 28, 4);
            Array.Copy(BitConverter.GetBytes((short)blockAlign), 0, header, 32, 2);
            Array.Copy(BitConverter.GetBytes((short)16), 0, header, 34, 2); // bits per sample

            // data subchunk
            Array.Copy(System.Text.Encoding.ASCII.GetBytes("data"), 0, header, 36, 4);
            Array.Copy(BitConverter.GetBytes(subChunk2Size), 0, header, 40, 4);

            // Combinar header + audio
            byte[] wavFile = new byte[44 + audioData.Length];
            Array.Copy(header, 0, wavFile, 0, 44);
            Array.Copy(audioData, 0, wavFile, 44, audioData.Length);

            return wavFile;
        }
    }
}
