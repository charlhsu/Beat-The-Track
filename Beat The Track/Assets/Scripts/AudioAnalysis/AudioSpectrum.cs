using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mini "engine" for analyzing m_audioSpectrum data
/// Feel free to get fancy in here for more accurate visualizations!
/// </summary>
public class AudioSpectrum : MonoBehaviour
{

    private void Update()
    {
        // get the data
        audioSource.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        // assign m_audioSpectrum value
        // this "engine" focuses on the simplicity of other classes only..
        // ..needing to retrieve one value (spectrumValue)
        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            spectrumValue = m_audioSpectrum[1] * 100;

            
        }
       /* for (int i = 1; i < m_audioSpectrum.Length - 1; i++)
        {
            Debug.DrawLine(new Vector3(i - 1, m_audioSpectrum[i] + 10, 0), new Vector3(i, m_audioSpectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(m_audioSpectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(m_audioSpectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), m_audioSpectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), m_audioSpectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(m_audioSpectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(m_audioSpectrum[i]), 3), Color.blue);
        }*/
    }

    private void Start()
    {
        /// initialize buffer
        m_audioSpectrum = new float[128];
    }

    // This value served to AudioSyncer for beat extraction
    public static float spectrumValue { get; private set; }

    // Unity fills this up for us
    private float[] m_audioSpectrum;
    public AudioSource audioSource;

}